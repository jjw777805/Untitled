
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace MyUI
{
    [AddComponentMenu("MyUI/DialogButton")]
    public class DialogButton : Button
    {
        [SerializeField]
        private TMP_Text buttonText;
        public bool left = true;
        private string nowText;
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            if(left)buttonText.SetText("> "+nowText);
            else buttonText.SetText(nowText+" <");
        } 

        public override void OnDeselect(BaseEventData eventData)
        {
            buttonText.SetText(nowText);
            base.OnDeselect(eventData);
        }
        // Start is called before the first frame update

    
        void Start()
        {
            
        }
        public void SetText(string txt)
        {
            nowText = txt;
            buttonText.SetText(nowText);
        }
    }
}

