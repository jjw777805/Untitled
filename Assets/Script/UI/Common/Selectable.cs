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
        public virtual void OnSelect(BaseEventData baseEvent)
        {
            if(canSelected)return;
            Deselect();
        }
 
        public virtual void OnDeselect(BaseEventData baseEvent)
        {
            
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
            EventSystem.current = null;
        }

        public void SetCanSeleted(bool f)
        {
            canSelected = f;
        }
    }
}