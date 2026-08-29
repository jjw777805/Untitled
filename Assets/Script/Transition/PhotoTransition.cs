
using System;
using MyUtils;
using UnityEngine;

namespace MyTransition
{
    [AddComponentMenu("Transition/Photo")]
    public class PhotoTransition : TransitionTemplate
    {
        [System.Serializable]
        public class ImageBundle
        {
            public ImageFade image;
            public float alphaFrom,alphaTo;
        }

        public ImageBundle imageFrom,imageTo;
        public float durationTime=1.0f;
        public float pauseTime=0.0f;
        protected float scale = 1.0f;

        [ContextMenu("SetIn")]
        void TrySetIn()
        {
            SetIn();
        }
        public override void SetIn(Action onComplete=null)
        {
            finished = false;
            scale = Time.timeScale;
            Time.timeScale = 0;
            Debug.Log("SetIn");
            imageFrom.image.gameObject.SetActive(true);
            imageTo.image.gameObject.SetActive(true);
            imageFrom.image.FadeTo(imageFrom.alphaFrom,0f);
            imageTo.image.FadeTo(imageTo.alphaFrom,0f);

            imageFrom.image.FadeTo(imageFrom.alphaTo,durationTime,()=>{finished = true;});
            imageTo.image.FadeTo(imageTo.alphaTo,durationTime,onComplete);
        }

        [ContextMenu("SetOut")]
        void TrySetOut()
        {
            SetOut();
        }
        public override void SetOut(Action onComplete=null)
        {
            Debug.Log("SetOut");
            finished = false;
            imageFrom.image.FadeTo(
                imageFrom.alphaTo,
                pauseTime,
                () =>
                {
                    imageFrom.image.FadeTo(
                        imageFrom.alphaFrom,
                        durationTime,
                        ()=>{
                            imageFrom.image.gameObject.SetActive(false);
                        }
                    );
                    imageTo.image.FadeTo(
                        imageTo.alphaFrom,
                        durationTime,
                        ()=>{
                            imageTo.image.gameObject.SetActive(false);
                            finished = true;
                            Time.timeScale =scale;
                            onComplete?.Invoke();
                        }
                    ); 
                }
            );   
        }

        void Start()
        {
            imageFrom.image.gameObject.SetActive(false);
            imageTo.image.gameObject.SetActive(false);
            finished = true;
        }
    }
}