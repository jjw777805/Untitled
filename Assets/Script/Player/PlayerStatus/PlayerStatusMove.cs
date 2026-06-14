using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        [SerializeField]
        private float moveSpeed = 5.0f;

        public float MoveSpeed
        {
            set
            {
                moveSpeed = value;
            }
            get
            {
                return moveSpeed;
            }
        }
        public bool CanMove()
        {
            if(isAttack==true||isSlide==true)return false;
            else return true;    
        }

        public void Move(Vector2 v)
        {
            if(v.x<-0.1f)faceTowards = -1;
            if(v.x>0.1f) faceTowards = 1;
        }

        int faceTowards = 1;
        public int FaceTowards()
        {
            return faceTowards;
        }
    }
}