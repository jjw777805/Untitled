using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MyUI
{
    [AddComponentMenu("MyUI/selectable", 30)]
    public class Selectable : MonoBehaviour,ISelectHandler,IDeselectHandler
    {
        private bool canSelected=true;
        [SerializeField]
        private UnityEvent m_OnSelect = new UnityEvent();
        public UnityEvent onSelect
        {
            get
            {
                return m_OnSelect;
            }
            set
            {
                m_OnSelect = value;
            }
        }
        
        [SerializeField]
        private UnityEvent m_OnDeselect = new UnityEvent();
        public UnityEvent onDeselect
        {
            get
            {
                return m_OnDeselect;
            }
            set
            {
                m_OnDeselect = value;
            }
        }

        public virtual void OnSelect(BaseEventData baseEvent)
        {
            if (canSelected)
            {
                m_OnSelect?.Invoke();
            }
            Deselect();
        }
 
        public virtual void OnDeselect(BaseEventData baseEvent)
        {
            m_OnDeselect?.Invoke();
        }

        public virtual void Select()
        {
            if(canSelected==false)return;
            if (!(EventSystem.current == null) && !EventSystem.current.alreadySelecting)
            {
                EventSystem.current.SetSelectedGameObject(this.gameObject);
            }
        }

        public virtual void Deselect()
        {
            if (!(EventSystem.current == null) && !EventSystem.current.alreadySelecting)
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

        public void SetCanSeleted(bool f)
        {
            canSelected = f;
        }
    }
}