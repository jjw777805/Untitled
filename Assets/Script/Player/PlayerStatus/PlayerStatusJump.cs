using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        private int canJump;
        private int jumpTimes;
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