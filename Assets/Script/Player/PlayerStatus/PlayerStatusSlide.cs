using System;
using Newtonsoft.Json.Linq;
using UnityEditor.SceneTemplate;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        bool isSlide,canSlide;
        float slideSeq=1.0f,slideTime;
        public void SetSlideSeq(float k)
        {
            slideSeq = k;
        }
        public bool CanSlide()
        {
            return canSlide;
        }
        public void Slide()
        {
            isSlide = true;
            slideTime = 0f;
        }

        void SlideUpdate()
        {
            slideTime += Time.deltaTime;
            if(slideTime < slideSeq)return ;
            isSlide = false;
            canSlide = true;
        }
    }
}