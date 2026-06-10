
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyUI
{
    public class ButtonSelectHandler : MonoBehaviour,ISelectHandler,IDeselectHandler
    {

        public void OnSelect(BaseEventData eventData)
        {
            Debug.Log("select!");
        } 

        public void OnDeselect(BaseEventData eventData)
        {
            Debug.Log("Deselect!");
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

