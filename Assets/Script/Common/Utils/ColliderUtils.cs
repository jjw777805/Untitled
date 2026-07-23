using Unity.Mathematics;
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
            Vector2 oriPos = transPos2D + Vector2.Scale(box.offset,trans.localScale);
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
        public static Vector3 AvailablePosRay(Transform trans,BoxCollider2D box,float distance,
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

        public static Vector3 AvailablePosBox(Transform trans,BoxCollider2D box,float distance)
        {
            Vector2 oriPos = trans.position;
            oriPos += Vector2.Scale(box.offset,trans.localScale);
            Vector2 size = Vector3.Scale(box.size,trans.localScale);
            size.x -= 0f;
            size.y -= 0f;

            
            RaycastHit2D hit = Physics2D.BoxCast(
                oriPos,
                size,
                0f,
                trans.right,
                distance,
                LayerMask.GetMask("Ground")
            );

            // Debug.Log(oriPos);
            if(hit.collider != null)return Vector3.right*hit.distance;
            return Vector3.right*distance;
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

        public static Vector2 GetWay(Vector2 delta)
        {
            Vector2 ans = Vector2.zero;
            float theta = Vector2.Angle(Vector2.zero,ans);
            theta += 22.5f ;
            int time = (int)math.floor(theta%360f / 45f);
            switch (time)
            {
                case 0:
                    return new Vector2(1,0).normalized;
                case 1 :
                    return new Vector2(1,1).normalized;
                case 2 :
                    return new Vector2(0,1).normalized;
                case 3 :
                    return new Vector2(-1,1).normalized;
                case 4 :
                    return new Vector2(-1,0).normalized;
                case 5 :
                    return new Vector2(-1,-1).normalized;
                case 6 :
                    return new Vector2(0,-1).normalized;
                case 7 :
                    return new Vector2(1,-1).normalized;
                default :
                    Debug.LogError("Wrong!");
                    return new Vector2(0,0);
            }
        }
    }

}