
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        bool onGround = true;
        BoxCollider2D cd;
        public LayerMask trapLayer;
        public float trapCheckRadius= 2.5f;

        void GroundInitial()
        {
            cd = GetComponent<BoxCollider2D>();
            onGround = true;
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
                onGround = false;
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

            if(hitLeft.collider!=null ||  hitRight.collider != null)
            {
                onGround = true;
            }
            else onGround = false;

            Collider2D trap = Physics2D.OverlapCircle(transform.position, trapCheckRadius, trapLayer);

            // if(hitLeft.collider!=null)
            //     Debug.Log(hitLeft.collider.tag);
            // if(hitRight.collider!=null)
            //     Debug.Log(hitLeft.collider.tag);

            if(hitLeft.collider!=null == (hitRight.collider != null)){
                if(onGround && trap==null)lastStablePos = transform.position;
            }
            // Debug.Log(onGround +" "+ halfOnGround);
        }
    }
}