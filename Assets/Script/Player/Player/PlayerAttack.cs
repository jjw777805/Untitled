
using MyEnemy;
using MyObject;
using UnityEngine;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        public AudioClip audioAttack;
        void Attack()
        {
            if(!ps.CanAttack())return ;
            if(!inputs.Player.Attack.WasPressedThisFrame())return ;
            am.SetTrigger("Attack");
            ps.Attacked();
            audioOneshot.PlayOneShot(audioAttack);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().Hurt(playerData.Damage);
            }
            else if (collision.gameObject.CompareTag("Crash"))
            {
                collision.GetComponent<CrashObject>().BeAttacked();
            }
        }
    }
}