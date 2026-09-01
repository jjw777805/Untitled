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
        Rigidbody2D rb;
        public BoxCollider2D bc;
        void Start()
        {
            if (Camera.main.orthographic)
            {
                float verticalSize = Camera.main.orthographicSize*2;
                // 水平宽度会根据屏幕宽高比（aspect）变化[reference:6]
                float horizontalSize = verticalSize * Camera.main.aspect;
                windowSize = new Vector2(horizontalSize,verticalSize);
                bc.size = windowSize;
            }
            rb = GetComponent<Rigidbody2D>();
            filter = new ContactFilter2D();
            
            filter.SetLayerMask(LayerMask.GetMask(layerName));
            filter.useLayerMask = true;
        }
        public void LateUpdate()
        {
            if(targetObject == null)return ;
            Vector3 endPos = targetObject.transform.position;
            Vector3 delta = endPos - transform.position;
            endPos = AdjustPos(delta);
            endPos.z = transform.position.z;
            // Debug.Log(endPos+" "+targetObject.transform.position);
            float ratio = math.pow(2,Vector3.Distance(endPos,transform.position))/math.pow(2,maxDistance);
            if(ratio>1)ratio=1;
            float speed = maxSpeed - (maxSpeed-minSpeed)*ratio;
            transform.position = Vector3.MoveTowards(
                    transform.position,
                    endPos,
                    speed*Time.deltaTime
                );
        }

        protected RaycastHit2D[] hitResults = new RaycastHit2D[1]; 
        protected ContactFilter2D filter;
        protected Vector3 AdjustPos(Vector3 delta)
        {
            Vector2 moveDelta = new Vector2(delta.x,delta.y);
            // 最终位置从当前刚体位置开始
            Vector2 finalPos = rb.position;
            float margin = 0.01f;
            float boxMargin = 0.999f;

            // ========== 2. 水平移动（X轴）检测 ==========
            if (Mathf.Abs(moveDelta.x) > 0.001f)
            {
                Vector2 dirX = new Vector2(Mathf.Sign(moveDelta.x), 0);
                float distX = Mathf.Abs(moveDelta.x);

                // 从当前刚体位置发射一个扁平的盒子射线
                int count = Physics2D.BoxCast(
                    rb.position,                
                    bc.size*bc.transform.localScale*boxMargin,           
                    0f,                       
                    dirX,                       
                    filter,                     
                    hitResults,                 
                    distX                      
                );

                if (count > 0)
                {
                    if(hitResults[0].fraction>margin)
                        finalPos.x = rb.position.x + moveDelta.x * (hitResults[0].fraction - margin);
                    // Debug.Log("here");     
                }
                else
                {
                    finalPos.x = rb.position.x + moveDelta.x;
                }
            }

            // ========== 3. 垂直移动（Y轴）检测 ==========
            if (Mathf.Abs(moveDelta.y) > 0.001f)
            {
                Vector2 dirY = new Vector2(0, Mathf.Sign(moveDelta.y));
                float distY = Mathf.Abs(moveDelta.y);

                Vector2 startPosY = new Vector2(finalPos.x, rb.position.y);

                int count = Physics2D.BoxCast(
                    startPosY,                  
                    bc.size*bc.transform.localScale*boxMargin,
                    0f,
                    dirY,
                    filter,
                    hitResults,
                    distY
                );

                if (count > 0)
                {
                    if(hitResults[0].fraction>margin)
                        finalPos.y = rb.position.y + moveDelta.y * (hitResults[0].fraction - margin);
                }
                else
                {
                    finalPos.y = rb.position.y + moveDelta.y;
                }
            }

            // ========== 4. 执行最终位移 ==========
            return finalPos;
        }
    }
}