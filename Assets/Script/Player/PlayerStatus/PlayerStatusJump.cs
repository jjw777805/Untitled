using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

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
            if(canJump != 0 && !isAttack && !isSlide)return true;
            else return false;      
        }    

        void JumpInitial()
        {
            jumpTimes = canJump;
        }

        public void Jump()
        {
            isJump = true;
            canJump--;
        }

        void JumpUpdate()
        {
            if (!onGround)return ;
            canJump = jumpTimes;
            isJump = false;
        }
    }
}