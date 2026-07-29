using UnityEngine;

namespace MyEnemy
{
    public partial class Enemy : MonoBehaviour
    {
        public virtual void Death(){}
        public virtual void Hurt(int damage){}
    }
}

