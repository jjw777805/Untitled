using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UI;

namespace MyUI
{
    /// <summary>
    /// 用来做item滚动条的组件，注意组件下需要放置一个示例item
    /// </summary>
    public class ScrollList:MonoBehaviour
    {
        [SerializeField]
        protected int count  = 1;
        protected List<GameObject>item=new List<GameObject>();
        public GameObject templateGO;
        protected int existCount=0;
        protected int maxCount=0;
        public float heightSpace = 0;
        protected float height = 0;
        protected float upDelta = 0; 
        protected RectTransform rect;
        public RectTransform layoutSize;
        
        public void SetCount(int n)
        {
            count = n;
        }

        void RectInitialize()
        {
            rect = GetComponent<RectTransform>();
            rect.pivot=new Vector2(0.5f,1);
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.anchoredPosition3D = Vector3.zero;
        }

        protected void ClearList()
        {
            foreach(var i in item){
                Destroy(i);
            }
            item.Clear();
        }
        public virtual void Initialized()
        {
            ClearList();
            RectInitialize();
            if(templateGO == null)
            {
                Debug.LogError("no samples");
                return ;
            }
            height = templateGO.GetComponent<RectTransform>().rect.height;
            float tHeight = layoutSize.rect.height;
            maxCount = existCount = 1 + (int)math.ceil((tHeight - height )/(height+heightSpace));
            upDelta = (maxCount-1)*(height+heightSpace) + height - tHeight;
            // Debug.Log("The Max Count is " + maxCount);

            if(existCount > count)existCount = count;
            else existCount = existCount + 1;

            // Debug.Log("The Exist Count is " + existCount);

            templateGO.SetActive(false);
            for(int i = 0; i < existCount; i++)
            {
                GameObject t = Instantiate(templateGO);
                item.Add(t);
                t.transform.SetParent(transform,false);
                t.SetActive(true);
            }
        }

    }
}