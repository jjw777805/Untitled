
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
            newPos = ColliderUtils.AvailablePosBox(transform,cd,ps.SlideDistance);
            transform.Translate(newPos);
            rb.velocity = new Vector2(rb.velocity.x,0);
            ps.Slide();
            jumpBegin = false;
            if (ps.IsBlock())
            {
                ps.BlockEnd();
                UIManager.instance.FinishCoundDown();
                blkCnt = 0;
                Time.timeScale=1;
            }
        
        }
    }
}