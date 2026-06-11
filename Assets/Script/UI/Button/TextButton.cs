
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
            buttonText.SetText("> "+nowText+" <");
        } 

        public override void OnDeselect(BaseEventData eventData)
        {
            buttonText.SetText(nowText);
        }
        // Start is called before the first frame update
        void Start()
        {
            nowText = buttonText.text;
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}

