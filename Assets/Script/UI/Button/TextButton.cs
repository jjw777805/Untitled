
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace MyUI
{
    [AddComponentMenu("MyUI/TextButton")]
    public class TextButton : Button
    {
        [SerializeField]
        private TMP_Text buttonText;
        private string nowText;
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            buttonText.SetText("> "+nowText+" <");
        } 

        public override void OnDeselect(BaseEventData eventData)
        {
            buttonText.SetText(nowText);
            base.OnDeselect(eventData);
        }
        // Start is called before the first frame update

        public virtual void Initailize()
        {
            nowText = buttonText.text;
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

