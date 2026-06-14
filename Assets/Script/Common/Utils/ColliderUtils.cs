using UnityEditor.Rendering.Universal;
using UnityEngine;

namespace MyUtils{
    public class ColliderUtils
    {
        public static Vector3 AvailablePos(Transform trans,BoxCollider2D box,float distance)
        {
            float skinWidth = 0.1f;   
            Vector2 transPos2D = trans.position;
            Vector2 oriPos = transPos2D + box.offset;
            float t = box.size.x/2*trans.localScale.x;
            oriPos.y = box.bounds.min.y + skinWidth ;

            RaycastHit2D hit = Physics2D.Raycast(oriPos,trans.right,distance,LayerMask.GetMask("Ground"));
            
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