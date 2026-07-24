
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Analytics;

namespace MyUtils
{
    public class Follow:MonoBehaviour
    {
        public float minSpeed = 2.0f;
        public float maxSpeed = 2.0f;
        public float maxDistance = 5f;
        public GameObject targetObject;
        void LateUpdate()
        {
            if(targetObject == null)return ;
            Vector3 endPos = targetObject.transform.position;
            endPos.z = transform.position.z;
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