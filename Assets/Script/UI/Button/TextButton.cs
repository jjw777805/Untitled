
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace MyUI
{
    [AddComponentMenu("MyUI/TextButton")]
    public class TextButton : Button
    {
        [SerializeField]
        protected TMP_Text buttonText;
        protected string nowText;
        protected bool isSelected=false;
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            buttonText.SetText("> "+nowText+" <");
            isSelected=true;
        } 

        public override void OnDeselect(BaseEventData eventData)
        {
            buttonText.SetText(nowText);
            base.OnDeselect(eventData);
            isSelected=false;
        }
        // Start is called before the first frame update

        public virtual void Initailize()
        {
            nowText = buttonText.text;
            isSelected = false;
        }
        void Start()
        {
            Initailize();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

