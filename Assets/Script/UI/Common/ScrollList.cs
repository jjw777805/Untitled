using System.Collections.Generic;
using UnityEngine;

namespace MyUI
{
    /// <summary>
    /// 用来做item滚动条的组件，注意组件下需要放置一个示例item
    /// </summary>
    public class ScrollList:MonoBehaviour
    {
        protected int count  = 1;
        protected List<GameObject>item=new List<GameObject>();
        public GameObject templateGO;
        public void SetCount(int n)
        {
            count = n;
        }

        public virtual void Initialized()
        {
            if(templateGO == null)
            {
                Debug.LogError("no samples");
                return ;
            }
            templateGO.SetActive(false);
            for(int i = 0; i < count; i++)
            {
                GameObject t = Instantiate(templateGO);
                item.Add(t);
                t.transform.SetParent(transform,false);
                t.SetActive(true);
            }
        }

    }
}