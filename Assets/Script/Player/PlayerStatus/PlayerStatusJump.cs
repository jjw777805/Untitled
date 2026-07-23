
namespace MyPlayer
{
    public partial class PlayerStatus
    {
        private int canJump;
        private int jumpTimes = 1;
        private bool isJump;
        public bool IsJump()
        {
            return isJump;
        }
        public bool CanJump()
        {   
            // Debug.Log("canJump:"+canJump);
            if(canJump != 0 && !isAttack && !isSlide && !isBlock)return true;
            else return false;      
        }    

        void JumpInitial()
        {
            canJump = jumpTimes;
        }

        public void Jump()
        {
            isJump = true;
            canJump--;
            onGround=false;
            // Debug.Log(canJump);
        }

        void JumpUpdate()
        {
            if (!onGround)return ;
            // Debug.Log("Update! "+ onGround);
            canJump = jumpTimes;
            isJump = false;
        }
    }
}