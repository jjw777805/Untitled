
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
            newPos = ColliderUtils.AvailablePosBox(transform,cd,ps.SlideDistance,0.1f);
            transform.Translate(newPos);
            rb.velocity = new Vector2(rb.velocity.x,0);
            ps.Slide();
            jumpBegin = false;
            if (ps.IsBlock())
            {
                ps.BlockEnd();
                UIManager.instance.FinishCoundDown();
                ps.GetGround();
                blkS.Invoke();
                rb.velocity = Vector2.zero;
                Time.timeScale = blkTimeScale;
                
                // blkCT.Reset();
            }
        
        }
    }
}