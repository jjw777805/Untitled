
using MyUtils;
using Unity.Mathematics;
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

            Vector3 newPos;
            newPos = ColliderUtils.AvailablePosBox(transform,cd,ps.SlideDistance);
            if(Mathf.Abs(newPos.x)>0.1)
                rb.velocity = newSpeed;
            else
            {
                if(rb.velocity.y>0 && math.abs(newSpeed.x)>0.1f)rb.velocity = new Vector2(rb.velocity.x,0);
            }
        }
    }
}