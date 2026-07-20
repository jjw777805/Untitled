
using MyPlayer;
using MyUtils;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

namespace MyEnemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class LittleEnemy:Enemy
    {
        public EnemyData enemyData;
        public Vector3 bornPos;
        bool isKnowPlayer = false;
        Vector3 playerPos;
        Rigidbody2D rb;
        bool IsInsight()
        {
            Vector3 selfPos = transform.position;
            Vector3 deltaPos = playerPos - selfPos;
            float dis=Vector3.Magnitude(deltaPos);
            if (isKnowPlayer)
            {
                if(dis<enemyData.catchDis)return true;
                else isKnowPlayer = false;
                return true;
            }
            else
            {
                if (dis < enemyData.catchDis)
                {
                    if (transform.right.x * deltaPos.x > 0.01f)
                    {
                        isKnowPlayer = true;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
        }

        void MoveHorizontal(Vector3 Target , float speed)
        {
            Vector3 deltaPos = Target - transform.position;
            if(deltaPos.x * transform.right.x < 0.01f)
            {
                Vector3 newEuler = transform.eulerAngles;
                newEuler.y = (newEuler.y+180f)%360;
                transform.rotation = Quaternion.Euler(newEuler);
            }
            rb.velocity = new Vector2(speed * transform.right.x , 0);
        }
        void Catch()
        {
            if(Vector3.Distance(playerPos,transform.position)<enemyData.atkDis)return;
            MoveHorizontal(playerPos,enemyData.catchSpeed);
        }

        void Patrol()
        {
            float left = bornPos.x - enemyData.patrolDis/2;
            float right = bornPos.x + enemyData.patrolDis/2;
            float nowPos = transform.position.x;
            if( left <= nowPos  && nowPos <= right)
            {
                rb.velocity = new Vector2(enemyData.patrolSpeed * transform.right.x , 0);
            } 
            else MoveHorizontal(bornPos,enemyData.patrolSpeed);
        }

        float hp;
        TimeCount stunCoolDown = new TimeCount();
        bool isStun=false;
        public void Hurt(float damage)
        {
            hp -= damage;
            isStun = true;
            anim.SetTrigger(StunHash);
            stunCoolDown.Reset();
            isKnowPlayer = true;
            if(hp<0)gameObject.SetActive(false);
        }
        bool canAttack = true;
        bool Attack()
        {
            if(!canAttack)return false;
            var hit = Physics2D.Raycast(
                    transform.position,
                    transform.right,
                    enemyData.atkDis,
                    LayerMask.GetMask("Player")  
                );
            if (hit.collider != null)
            {
                canAttack = false;
                atkCoolDown.Reset();
                anim.SetTrigger(AttackHash);
                return true;
            }
            return false;
        }

        TimeCount atkCoolDown;
        Animator anim;

        public void Reset()
        {
            gameObject.transform.position = bornPos;
            hp = enemyData.hp;
            transform.rotation = Quaternion.Euler(0,0,0);
            gameObject.SetActive(true);
        }
        private readonly int StunHash = Animator.StringToHash("Stun");
        private readonly int AttackHash = Animator.StringToHash("Attack");
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            hp = enemyData.hp;
            atkCoolDown = new TimeCount(enemyData.atkSeq);
            atkCoolDown.On_End.AddListener(()=>{canAttack=true;});
            stunCoolDown = new TimeCount(enemyData.stunTime);
            stunCoolDown.On_End.AddListener(()=>{isStun=false;});
        }
        void Update()
        {
            
            if(Time.deltaTime ==0)return;
            atkCoolDown.Update(Time.deltaTime);

            if(isStun)return;
            playerPos = Player.instance.transform.position;
            bool isInsight = IsInsight();
            if(!isInsight)Patrol();
            else
            {
                if(!Attack())Catch();
            }
        }
    }
}