
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace MyUI
{
    /// <summary>
    /// 用来导航的按钮，也就是选中了之后会自动打开按钮对应的界面
    /// </summary>
    [AddComponentMenu("MyUI/NavigateButton")]
    public class NavigateButton : TextButton
    {
        [SerializeField]
        Closable defaultOpen;
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            defaultOpen?.Open();
        } 

        public override void OnDeselect(BaseEventData eventData)
        { 
            defaultOpen?.Close();
            base.OnDeselect(eventData);
        }

        public override void Initailize()
        {
            base.Initailize();
            
        }

        void Start()
        {
            Initailize();
        }
    }
}

