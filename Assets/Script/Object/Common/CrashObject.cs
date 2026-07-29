using System.Threading.Tasks;
using MyEnemy;
using MyPlayer;
using UnityEngine;

namespace MyObject
{
    public class CrashObject : MonoBehaviour
    {
        public bool canCrashed = true;
        public int hp = 3;
        SpriteRenderer sr;

        public virtual void Start()
        {
            sr = GetComponent<SpriteRenderer>();
        }
        async void Shilver()
        {
            await Task.Delay(33);
            sr.enabled = false;
            await Task.Delay(200);
            sr.enabled = true;
        }
        public void BeAttacked()
        {
            hp--;
            if (hp == 0)
            {
                Destroy(gameObject);
            } 
            else Shilver();          
        }
    }
}