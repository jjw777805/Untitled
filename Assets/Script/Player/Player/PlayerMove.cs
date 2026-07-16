
using MyUtils;
using UnityEngine;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        public void Move()
        {
            if(!ps.CanMove())return ;
            Vector2 moveValue = inputs.Player.Move.ReadValue<Vector2>();
            Vector2 newSpeed = new Vector2( moveValue.x * ps.MoveSpeed ,rb.velocity.y);
            ps.Move(newSpeed);
            
            if(ps.FaceTowards() == -1){
                transform.rotation = Quaternion.Euler(
                    transform.eulerAngles.x,
                    180f,
                    transform.eulerAngles.z
                );
            }
            else
            {
                 transform.rotation = Quaternion.Euler(
                    transform.eulerAngles.x,
                    0f,
                    transform.eulerAngles.z
                );
            }

            Vector3 newPosBottom,newPosTop;
            newPosBottom = ColliderUtils.AvailablePos(transform,cd,ps.SlideDistance,Anchor.Bottom);
            newPosTop = ColliderUtils.AvailablePos(transform,cd,ps.SlideDistance,Anchor.Top);
            if(Mathf.Abs(newPosBottom.x)>0.1 && Mathf.Abs(newPosTop.x)>0.1)
                rb.velocity = newSpeed;
        }
    }
}