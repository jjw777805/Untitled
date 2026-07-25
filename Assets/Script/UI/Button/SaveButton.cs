
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

        [SerializeField]
        public GameObject delBt;
        public override void Initailize()
        {
            base.Initailize();
            if (GameManager.instance.HasSave(number))
            {
                buttonText.text = "存档 " + number ;
                delBt.SetActive(true);
            }else
            {
                buttonText.text = "新存档" ;
                delBt.SetActive(false);
            }
        }

        public void DelSave(int i)
        {
            UIManager.instance.OpenMsgBoxYN(
                "SaveDelete",
                "确认要删除该存档吗？",
                ()=>{
                    GameManager.instance.DelSave(i);
                    UIManager.instance.ShutDownMsgBox("SaveDelete");
                    buttonText.text = "新存档" ;
                    delBt.SetActive(false);
                    if (EventSystem.current != null)
                    {
                        EventSystem.current.SetSelectedGameObject(gameObject);
                    }
                    },
                ()=>{
                    UIManager.instance.ShutDownMsgBox("SaveDelete");},
                new Vector2(1600,800)
            );
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

