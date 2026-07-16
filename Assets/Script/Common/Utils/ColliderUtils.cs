using UnityEditor.PackageManager;
using UnityEditor.Rendering.Universal;
using UnityEngine;

namespace MyUtils{
    public enum Anchor {
        Top,Bottom,Left,Right,TopLeft,TopRight,BottomLeft,BottomRight
    };
    public class ColliderUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="box"></param>
        /// <param name="skinWidth"></param>
        /// <param name="anchor"></param>
        /// <returns>(x,y,z) = (pos_X , pos_Y , offset_from_X_edge)</returns>
        public static Vector3 GetAnchor(Transform trans,BoxCollider2D box,
                float skinWidth,Anchor anchor = Anchor.Bottom)
        {
            Vector2 transPos2D = trans.position;
            Vector2 oriPos = transPos2D + box.offset;
            Vector3 ans=Vector3.zero;
            switch (anchor)
            {
                case Anchor.Bottom:
                    oriPos.y = box.bounds.min.y + skinWidth ;
                    ans = oriPos;
                    ans.z = box.size.x/2*trans.localScale.x;
                    break; 
                case Anchor.Top:
                    oriPos.y = box.bounds.max.y - skinWidth ;
                    ans = oriPos;
                    ans.z = box.size.x/2*trans.localScale.x;
                    break;
                default:
                    Debug.LogError("unknown anchor");
                    break;
            }
            return ans;
        }
        /// <summary>
        /// 从box底部稍微高一点的地方，中心射出
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="box"></param>
        /// <param name="distance"></param>
        /// <returns>前进方向的偏移量</returns>
        public static Vector3 AvailablePos(Transform trans,BoxCollider2D box,float distance,
            Anchor anchor = Anchor.Bottom )
        {
            Vector3 anchorPos = GetAnchor(trans,box,0.01f,anchor);
            Vector2 oriPos = anchorPos;
            float t = anchorPos.z;

            RaycastHit2D hit = Physics2D.Raycast(oriPos,trans.right,distance,LayerMask.GetMask("Ground"));
            

            // Debug.Log(oriPos);
            if(hit.collider != null)
            {
                return Vector3.right*(hit.distance-t);
            }
            else return Vector3.right*distance;
        }
    
        public static Vector2 GetCollionWay(Collision2D col)
        {
            Vector2 avg = Vector2.zero;
            for(int i = 0; i < col.contactCount; i++)
            {
                avg += col.GetContact(i).normal;
            }
            avg.Normalize();
            return avg;
        }
    }

}