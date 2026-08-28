
using MyUtils;
using UnityEngine;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        void Heal()
        {
            // Debug.Log("in this!!");
            if(!ps.CanHeal())return ;
            // Debug.Log("in!");
            if(!inputs.Player.Heal.WasPressedThisFrame())return ;
            ps.Heal();
        }
    }
}