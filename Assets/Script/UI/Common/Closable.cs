using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyUI
{
    [Serializable]
    public class Closable : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvasGroup;
        [SerializeField]
        Selectable defaultFocus;
        [SerializeField]
        Closable defaultOpen;

        protected Closable()
        {
        }

        public virtual void Open()
        {
            canvasGroup.alpha=1;
            canvasGroup.blocksRaycasts=true;
            canvasGroup.interactable=true;
            if(defaultOpen!=null)defaultOpen.Open();
            else defaultFocus?.Select();
        }

        public virtual void Close()
        {
            EventSystem.current=null;
            canvasGroup.alpha=0;
            canvasGroup.blocksRaycasts=false;
            canvasGroup.interactable=false;
        }    
    }
}