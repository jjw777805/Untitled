
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace MyUI
{
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

