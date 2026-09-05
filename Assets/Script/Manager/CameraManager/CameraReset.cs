using MyCamera;
using MyPlayer;
using UnityEngine;

namespace MyManager
{
    public partial class CameraManager : MonoBehaviour
    {
        public void ResetStatus()
        {
            Vector3 t = Player.instance.transform.position;
            t.z=transform.position.z;
            transform.position = t;
            TrySeparate();
        }

        public float extraOffset = 0.01f;
        public string tagString = "CameraBounds";

        Collider2D myCollider,otherCollider;

        public void TrySeparate()
        {
            // Debug.Log("before : "+transform.position);
            myCollider = GetComponentInChildren<Collider2D>();
            GameObject t = GameObject.FindWithTag(tagString);
            // Debug.Log(t==null);
            if(t!=null)otherCollider = t.GetComponentInChildren<Collider2D>();
            
            if (myCollider == null || otherCollider == null) return;
            // Debug.Log("In Sep!");
            Bounds myBounds = myCollider.bounds;
            Bounds targetBounds = otherCollider.bounds;

            Vector3 pos = transform.position;

            Vector2 halfSize = myBounds.extents;

            float minX = targetBounds.min.x + halfSize.x;
            float maxX = targetBounds.max.x - halfSize.x;
            if (minX > maxX) pos.x = targetBounds.center.x;
            else pos.x = Mathf.Clamp(pos.x, minX, maxX);

            float minY = targetBounds.min.y + halfSize.y;
            float maxY = targetBounds.max.y - halfSize.y;
            if (minY > maxY)pos.y = targetBounds.center.y;
            else pos.y = Mathf.Clamp(pos.y, minY, maxY);

            transform.position = pos;
        }
    }
 
}
