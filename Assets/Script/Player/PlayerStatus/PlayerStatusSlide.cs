using System;
using Newtonsoft.Json.Linq;
using UnityEditor.SceneTemplate;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        bool isSlide,canSlide;
        [SerializeField]
        float slideSeq=1.0f;
        float slideTime;

        [SerializeField]
        float slideDistance = 5;
        public float SlideDistance
        {
            set {slideDistance = value;}
            get {return slideDistance;}
        }

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
            slideTime = 0f;
            canSlide = false;
        }

        void SlideInitial()
        {
            canSlide = true;
            isSlide = false;    
        }

        void SlideUpdate()
        {
            slideTime += Time.deltaTime;
            if(slideTime < slideSeq)return ;
            canSlide = true;
        }
    }
}