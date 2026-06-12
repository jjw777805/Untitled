
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Myutils;
using UnityEngine.UI;

namespace MyUI
{
    [AddComponentMenu("MyUI/ColorChangeButton")]
    public class ColorChangeButton : Button
    {
        [SerializeField]
        protected TMP_Text buttonText;
        [SerializeField]
        ColorChange colorChange;
        [SerializeField]
        Image backGround;
        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            backGround.color = colorChange.To;
        } 

        public override void OnDeselect(BaseEventData eventData)
        {
            backGround.color = colorChange.From;
            base.OnDeselect(eventData);
        }
        // Start is called before the first frame update

        public virtual void Initailize()
        {
            backGround.color = colorChange.From;
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

