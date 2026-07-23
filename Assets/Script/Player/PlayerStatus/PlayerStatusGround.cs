
using UnityEngine;
using UnityEngine.UIElements;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        bool onGround = true;
        bool halfOnGround = false;
        BoxCollider2D cd;

        void GroundInitial()
        {
            cd = GetComponent<BoxCollider2D>();
            onGround = true;
            halfOnGround = false;
        }

        public bool OnGround()
        {
            OnGroundUpdate();
            return onGround;
        }

        public void GetGround()
        {
            JumpInitial();
            SlideInitial();
        }

        void OnGroundUpdate()
        {
            if(isJump && GetComponent<Rigidbody2D>().velocity.y>0f)
            {
                onGround = halfOnGround = false;
                return ;
            }

            Vector3 centerPos = 
                transform.position + Vector3.Scale(cd.offset,transform.localScale);
            Vector3 leftPos,RightPos;
            RightPos = leftPos = centerPos;
            leftPos.x = centerPos.x - transform.localScale.x * cd.size.x/2+0.1f;  
            RightPos.x = centerPos.x + transform.localScale.x * cd.size.x/2-0.1f;  
            RaycastHit2D hitLeft = Physics2D.Raycast(
                leftPos,
                -transform.up,
                transform.localScale.y*(cd.size.y+0.1f)/2,
                LayerMask.GetMask("Ground")
            );
            RaycastHit2D hitRight = Physics2D.Raycast(
                RightPos,
                -transform.up,
                transform.localScale.y*(cd.size.y+0.1f)/2,
                LayerMask.GetMask("Ground")
            );
            if(hitLeft.collider!=null || hitRight.collider != null)
            {
                onGround = true;
            }else onGround = false;

            if(hitLeft.collider!=null == (hitRight.collider != null)){
                halfOnGround = false;
                if(onGround)lastStablePos = transform.position;
            }else halfOnGround = true;
            // Debug.Log(onGround +" "+ halfOnGround);
        }
    }
}