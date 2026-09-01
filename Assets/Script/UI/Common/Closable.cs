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
        GameObject frontSelected=null;
        bool isInitialed = false;
        protected Closable()
        {
        }

        public void SetFrontSelected(GameObject t)
        {
            // Debug.Log(t.name);
            frontSelected = t ;
        }
        public virtual void Open()
        {
            // Debug.Log(gameObject.GetInstanceID()+"_"+gameObject.name+":Open "+frontSelected?.name);
            canvasGroup.alpha=1;
            canvasGroup.blocksRaycasts=true;
            canvasGroup.interactable=true;
            if(     frontSelected == null 
                && EventSystem.current != null
                && !EventSystem.current.alreadySelecting
                && EventSystem.current.currentSelectedGameObject != null
                && EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>()!=null
            )
            {
                frontSelected = EventSystem.current.currentSelectedGameObject;
            }
            if(defaultOpen!=null)defaultOpen.Open();
            else
            {
                if(defaultFocus!=null)defaultFocus?.Select();
            }
        }

        public virtual void Close()
        {
            // Debug.Log(gameObject.GetInstanceID()+"_"+gameObject.name+":Close "+frontSelected?.name);
            if(frontSelected != null)
                frontSelected.GetComponent<Selectable>().Select();
            else
            {
                if ( isInitialed &&!(EventSystem.current == null) && !EventSystem.current.alreadySelecting)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }
            canvasGroup.alpha=0;
            canvasGroup.blocksRaycasts=false;
            canvasGroup.interactable=false;
        }    

        public virtual void Initailize()
        {
            Close();
            isInitialed = true;
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