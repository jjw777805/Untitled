
using System.Reflection.Emit;
using MyPlayer;
using MyUtils;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

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
                else playerKnownCT.Reset();
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
        public override void Hurt(float damage)
        {
            hp -= damage;
            isStun = true;
            anim.SetTrigger(StunHash);
            stunCoolDown.Reset();
            isKnowPlayer = true;
            if(hp<=0)gameObject.SetActive(false);
        }
        bool canAttack = true;
        public bool isAtk = false;
        bool Attack()
        {
            if(!canAttack || isAtk)return false;
            var hit = Physics2D.Raycast(
                    transform.position,
                    transform.right,
                    enemyData.atkDis,
                    LayerMask.GetMask("Player")  
                );
            if (hit.collider != null)
            {
                canAttack = false;
                isAtk = true;
                atkCoolDown.Reset();
                rb.velocity = new Vector2(0,rb.velocity.y);
                anim.SetTrigger(AttackHash);
                return true;
            }
            return false;
        }

        TimeCount atkCoolDown;
        Animator anim;

        void MoveCheck()
        {
            Vector3 newPos;
            newPos = ColliderUtils.AvailablePosBox(transform,cd,5.0f);
            if(Mathf.Abs(newPos.x)<0.01f)rb.velocity= new Vector2(0,rb.velocity.y);
        }
        public void Reset()
        {
            gameObject.transform.position = bornPos;
            hp = enemyData.hp;
            transform.rotation = Quaternion.Euler(0,0,0);
            gameObject.SetActive(true);
        }
        public static readonly int StunHash = Animator.StringToHash("Stun");
        public static readonly int AttackHash = Animator.StringToHash("Attack");
        TimeCount playerKnownCT = new TimeCount();
        BoxCollider2D cd;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            cd = GetComponent<BoxCollider2D>();
            hp = enemyData.hp;
            atkCoolDown = new TimeCount(enemyData.atkSeq);
            atkCoolDown.On_End.AddListener(()=>{canAttack=true;});
            stunCoolDown = new TimeCount(enemyData.stunTime);
            stunCoolDown.On_End.AddListener(()=>{isStun=false;});
            playerKnownCT = new TimeCount(enemyData.lossPlayerTime);
            playerKnownCT.On_End.AddListener(()=>isKnowPlayer = false);
        }
        void Update()
        {
            
            if(Time.deltaTime ==0)return;
            atkCoolDown.Update(Time.deltaTime);
            stunCoolDown.Update(Time.deltaTime);
            playerKnownCT.Update(Time.deltaTime);
            if(isStun || isAtk)return;
            playerPos = Player.instance.transform.position;
            bool isInsight = IsInsight();
            if(!isInsight)Patrol();
            else
            {
                if(!Attack())Catch();
            }
            MoveCheck();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Player.instance.Hurt();
            }
        }
    }
}