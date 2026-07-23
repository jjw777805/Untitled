
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        public void Trap(int damage)
        {
            isinjury=false;
            Player.instance.GetComponent<Animator>().SetBool("Hurt",false);
            if(hp>damage)Reborn();
            Hurt(damage);
            
        }
    }
}