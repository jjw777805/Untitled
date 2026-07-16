
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        private float attackSeq=1.0f;
        private bool canAttack = true;  
        private bool isAttack = false;
        public bool CanAttack()
        {
            if(isSlide)return false;
            return canAttack;
        }

        public bool IsAttack
        {
            get
            {
                return isAttack;
            }
            set
            {
                isAttack = value;
            }
        }

        public void SetAttackSeq(float k)
        {
            attackSeq = k;
        }

        private float attackBeginTime = 0;
        public void Attacked()
        {
            canAttack = false;
            isAttack = true;
            attackBeginTime = 0.0f;
        }

        void AttackUpdate()
        {
            attackBeginTime += Time.deltaTime;
            if(attackBeginTime > attackSeq)
            {
                isAttack = false;
                canAttack = true;
            }
        }
    }
}