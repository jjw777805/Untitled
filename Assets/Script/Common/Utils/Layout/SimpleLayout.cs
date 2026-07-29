using System.ComponentModel.Design;
using UnityEngine;

namespace MyUtils
{
    public class SimpleLayout : MonoBehaviour
    {
        public Vector3 delta;
        
        [ContextMenu("SimpleLayout")]
        protected virtual void LayoutChildren()
        {

            Vector3 pos = transform.GetChild(0).position;
            for (int i = 1; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                // 计算水平位置：起始位置为0，每个物体向右偏移 i * spacing
                Vector3 t = pos + i*delta;
                // 保持物体的Y和Z坐标不变，只修改X
                child.position = t;
            }
        }

        void Update()
        {
            LayoutChildren();
        }
    }
}