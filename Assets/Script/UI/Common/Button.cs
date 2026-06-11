using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MyUI
{
    [AddComponentMenu("MyUI/Button", 30)]
    public class Button : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler,
    IPointerEnterHandler
    {
        protected Button()
        {
        }

        [Serializable]
        public class ButtonClickedEvent : UnityEvent
        {
        }

        [FormerlySerializedAs("onClick")]
        [SerializeField]
        private ButtonClickedEvent m_OnClick = new ButtonClickedEvent();

        public ButtonClickedEvent onClick
        {
            get
            {
                return m_OnClick;
            }
            set
            {
                m_OnClick = value;
            }
        }

        

        private void Press()
        {
            UISystemProfilerApi.AddMarker("Button.onClick", this);
            m_OnClick.Invoke();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Press();
            }
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            Press();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            Select();
        }

    }
}