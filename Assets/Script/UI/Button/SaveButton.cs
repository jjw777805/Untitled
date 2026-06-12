
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
        private bool isNew = true;
        public override void Initailize()
        {
            base.Initailize();
            if (GameManager.instance.HasSave(number))
            {
                isNew = false;
            }else
            {
                buttonText.text = "新存档" ;
                isNew = true;
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

