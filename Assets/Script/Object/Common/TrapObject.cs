using MyEnemy;
using MyPlayer;
using UnityEngine;

namespace MyObject
{
    public class TrapObject : MonoBehaviour
    {
        public int trapDamage=1;
        public bool canBlock=true;
        public bool canAtkEnemy=true;
        void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.CompareTag("Player"))
            {
                Vector3 delta = transform.position - Player.instance.transform.position;
                if(canBlock)Player.instance.MayHurt(
                        delta,
                        trapDamage,
                        Player.BlkType.Trap,
                        ()=>{},
                        ()=>{}
                    );
                else Player.instance.Trap(trapDamage);
            }
            else if (collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Death();
            }
        }
    }
}