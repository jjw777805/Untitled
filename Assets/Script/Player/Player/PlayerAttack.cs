
using UnityEngine;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        void Attack()
        {
            if(!ps.CanAttack())return ;
            if(!inputs.Player.Attack.WasPressedThisFrame())return ;
            am.SetTrigger("Attack");
        }
    }
}