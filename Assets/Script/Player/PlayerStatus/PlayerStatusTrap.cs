
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        bool hasTrap =false;
        public void Trap(int damage)
        {
            if(hasTrap)return ;
            // Debug.Log("hurt");
            isinjury=false;
            hasTrap = true;
            if(hp>damage)Reborn(); 
            Hurt(damage);
            
        }

        public bool CanTrap()
        {
            return !hasTrap;
        }
        void TrapLateUpdate()
        {
            hasTrap = false;
        }
    }
}