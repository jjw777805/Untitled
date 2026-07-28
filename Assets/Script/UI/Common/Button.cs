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
    IPointerEnterHandler, ICancelHandler
    {
        protected Button()
        {
        }

        [FormerlySerializedAs("onClick")]
        [SerializeField]
        private UnityEvent m_OnClick = new UnityEvent();
        public UnityEvent onClick
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

        [SerializeField]
        private UnityEvent m_OnExit = new UnityEvent();
        public UnityEvent OnExit
        {
            get
            {
                return m_OnExit;
            }
            set
            {
                m_OnExit = value;
            }
        }

        private void Press()
        {
            m_OnClick.Invoke();
        }

        private void PressRight()
        {
            m_OnExit.Invoke();
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Press();
            }
            else if(eventData.button == PointerEventData.InputButton.Right)
            {
                PressRight();
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

        public virtual void OnCancel(BaseEventData eventData)
        {
            
            PressRight();
        }

    }
}