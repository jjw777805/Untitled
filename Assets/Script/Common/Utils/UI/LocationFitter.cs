using MyEnum;
using UnityEngine;
using UnityEngine.Rendering;

namespace MyUtils
{
    public class LocationFitter : MonoBehaviour
    {
        public RectTransform target;
        public Anchor anchor;
        RectTransform selfRect;

        public Vector3 delta;

        #region 生命周期函数
        void Start()
        {
            selfRect = gameObject.GetComponent<RectTransform>();
            if(target == null)return ;
            Fitter();
        }
        void Update()
        {
            if(target == null)return ;
            // Debug.Log(selfRect.position);
            Fitter();
        }
        #endregion


        #region Utils

        void Fitter()
        {
            selfRect.position = GetPosition();
        }
        Vector3 GetPosition()
        {
            // Debug.Log(pos+","+target.rect.xMin+","+target.rect.yMax);
            switch (anchor)
            {
                case Anchor.TopLeft:
                    return target.TransformPoint (
                            new Vector3(target.rect.xMin,target.rect.yMax,0)+delta
                        );
                case Anchor.TopRight:
                    return target.TransformPoint (
                            new Vector3(target.rect.xMax,target.rect.yMax,0)+delta
                        );
                default:
                    Debug.LogError("No this anchor");
                    break;
            }
            return Vector3.zero;
        }
        #endregion
    }
}