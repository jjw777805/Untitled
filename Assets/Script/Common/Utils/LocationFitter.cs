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
            selfRect.position = GetPosition(target.position);
        }
        Vector3 GetPosition(Vector3 pos)
        {
            // Debug.Log(pos+","+target.rect.xMin+","+target.rect.yMax);
            switch (anchor)
            {
                case Anchor.TopLeft:
                    return target.TransformPoint (
                            new Vector3(target.rect.xMin,target.rect.yMax,0)+delta
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