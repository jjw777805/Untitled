
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Myutils;
using UnityEngine.UI;

namespace MyUI
{
    [AddComponentMenu("MyUI/SaveButton")]
    public class SaveButton : ColorChangeButton
    {
        [SerializeField]
        private int number;
        public override void Initailize()
        {
            base.Initailize();
            if (GameManager.instance.HasSave(number))
            {
                buttonText.text = "存档 " + number ;
            }else
            {
                buttonText.text = "新存档" ;
            }
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

