using UnityEngine;

namespace MyUtils
{
    public class SizeFitter : MonoBehaviour
    {
        public RectTransform target;
        RectTransform selfRect;

        void Start()
        {
            selfRect = gameObject.GetComponent<RectTransform>();
            if(target == null)return ;
            selfRect.sizeDelta = target.sizeDelta;
        }
        void Update()
        {
            if(target == null)return ;
            selfRect.sizeDelta = target.sizeDelta;
        }
    }
}