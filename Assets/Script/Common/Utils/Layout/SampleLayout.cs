using UnityEngine;

namespace MyUtils
{
    public class SampleLayout : SimpleLayout
    {
       
        public int number;
        public GameObject sample;

        [ContextMenu("SampleLayout")]
        protected override void LayoutChildren()
        {

            if (transform.childCount < number)
            {
                for(int i = transform.childCount; i < number; i++)
                {
                    Instantiate(sample,transform);
                }
            }else if(transform.childCount > number)
            {
                for(int i=transform.childCount -1 ; i>number-1 && i>0; i--)
                {
                    #if UNITY_EDITOR
                        DestroyImmediate(transform.GetChild(i).gameObject);
                    #else
                        Destroy(transform.GetChild(i).gameObject);
                    #endif
                }
            }
            base.LayoutChildren();
        }
        void Update()
        {
            LayoutChildren();
        }
    }
}