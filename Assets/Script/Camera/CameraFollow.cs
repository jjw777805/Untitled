using MyUtils;
using Unity.Mathematics;
using UnityEngine;

namespace MyCamera
{
    public class CameraFollow : Follow
    {
        [SerializeField]
        protected Vector2 windowSize = Vector2.zero;
        public string layerName="Camera";
        void Start()
        {
             if (Camera.main.orthographic)
            {
                float verticalSize = Camera.main.orthographicSize*2;
                // 水平宽度会根据屏幕宽高比（aspect）变化[reference:6]
                float horizontalSize = verticalSize * Camera.main.aspect;
                windowSize = new Vector2(horizontalSize,verticalSize);
            }
        }
        void LateUpdate()
        {
            if(targetObject == null)return ;
            Vector3 endPos = targetObject.transform.position;
            endPos.z = transform.position.z;
            Vector3 delta = endPos - transform.position;
    
            float ratio = math.pow(2,Vector3.Distance(endPos,transform.position))/math.pow(2,maxDistance);
            if(ratio>1)ratio=1;
            float speed = maxSpeed - (maxSpeed-minSpeed)*ratio;
            transform.position = Vector3.MoveTowards(
                    transform.position,
                    endPos,
                    speed*Time.deltaTime
                );
        }
    }
}