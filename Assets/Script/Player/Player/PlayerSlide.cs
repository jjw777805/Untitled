
using MyUtils;
using UnityEngine;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        void Slide()
        {
            if(  !ps.CanSlide())return ;
            if(!inputs.Player.Slide.WasPressedThisFrame())return ;
            Vector3 newPos;
            newPos = ColliderUtils.AvailablePos(transform,cd,ps.SlideDistance);
            transform.Translate(newPos);
            ps.Slide();
        
        }
    }
}