using System;
using Unity.VisualScripting;
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
        GameObject frontSelected;
        protected Closable()
        {
        }

        public void SetFrontSelected(GameObject t)
        {
            frontSelected = t ;
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
            if(     EventSystem.current!=null 
                && !EventSystem.current.alreadySelecting
                && frontSelected != null )
            {
                EventSystem.current.SetSelectedGameObject(frontSelected);
            }
            canvasGroup.alpha=0;
            canvasGroup.blocksRaycasts=false;
            canvasGroup.interactable=false;
        }    

        public virtual void Initailize()
        {
            Close();
        }

        public virtual void ShutDown()
        {
            Close();
            Destroy(gameObject);
        }
        void Start()
        {
            Initailize();
        }
    }
}