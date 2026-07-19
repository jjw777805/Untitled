using UnityEditor.Rendering.Universal;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        bool onGround = true;

        public bool OnGround()
        {
            if(isJump && GetComponent<Rigidbody2D>().velocity.y>0f)return false;
            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                -transform.up,
                transform.localScale.y*(GetComponent<RectTransform>().rect.height+0.1f)/2,
                LayerMask.GetMask("Ground")
            );
            // Debug.Log(-transform.up*(GetComponent<RectTransform>().rect.height+0.1f));
            // Debug.DrawRay(transform.position,Vector3.Scale(-transform.up,transform.localScale)*(GetComponent<RectTransform>().rect.height+0.1f)/2);
            if(hit.collider!=null)return true;
            else return false;
        }

        void OnGroundUpdate()
        {
            onGround = OnGround();
            // Debug.Log(onGround);
        }
    }
}