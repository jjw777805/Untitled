
using UnityEditor.SceneTemplate;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Timeline;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        [SerializeField]
        private float upG = 0.5f , downG = 1f ,maxFallingV = -10;
        [SerializeField]
        private float maxJumpTime = 1.0f;
        [SerializeField]
        private float jumpV1=7.0f,jumpV2=5.0f;
        private float deltaJumpTime=0;
        private bool jumpBegin = false;
        void Jump()
        {
            if(rb.velocity.y<maxFallingV)rb.velocity = new Vector2(rb.velocity.x,maxFallingV);
            if (ps.IsJump())
            {
                if (jumpBegin && deltaJumpTime < maxJumpTime && inputs.Player.Jump.IsPressed())
                {
                    rb.velocity = new Vector2(rb.velocity.x,jumpV2);
                    deltaJumpTime += Time.deltaTime;     
                }
                else jumpBegin = false;
                
                if(!jumpBegin){
                    rb.gravityScale = downG;
                }      
            }

            if(!ps.CanJump())return ;
            if(ps.OnGround())rb.velocity = new Vector2(rb.velocity.x,0);
            if(!inputs.Player.Jump.WasPressedThisFrame())return ;
            ps.Jump();
            jumpBegin = true;
            // Debug.Log("Here!");
            rb.velocity = new Vector2(rb.velocity.x,jumpV1);
            deltaJumpTime = Time.deltaTime;
            rb.gravityScale = upG;
        }
    }
}