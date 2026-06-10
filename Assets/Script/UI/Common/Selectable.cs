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
        public void OnSelect(BaseEventData baseEvent)
        {
            
        }

        public void OnDeselect(BaseEventData baseEvent)
        {
            
        }

        public virtual void Select()
        {
            if (!(EventSystem.current == null) && !EventSystem.current.alreadySelecting)
            {
                EventSystem.current.SetSelectedGameObject(this.gameObject);
            }
        }

    }
}