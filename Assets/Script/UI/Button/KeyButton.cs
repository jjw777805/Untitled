
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace MyUI
{
    [AddComponentMenu("MyUI/KeyButton")]
    public class KeyButton : TextButton
    {
        public string mapName,actionName,bindingName="";
        public string dis;

        InputAction ia = null;
        GameManager gm;
        protected virtual void Flash()
        {
            if (ia == null)
            {        
                ia = gm.GetInputAction(mapName,actionName);
            }
            string keyName = gm.InputsGetKeyName(ia,bindingName);
            nowText = dis + "     "+keyName;
            if (isSelected)
            {
                buttonText.SetText("> "+nowText+" <");
            }
            else
            {
                buttonText.SetText(nowText);
            }
        }
        
        public void OpenMsgBox()
        {
            UIManager.instance.OpenMsgBox(dis,"请输入新键位",new Vector2(800,200));
        }

        public void Rebinding()
        {
            OpenMsgBox();
            GameManager.instance.InputsRebinding(mapName,actionName,bindingName,true,
                () =>{UIManager.instance.ShutDownMsgBox(dis);},
                () =>{UIManager.instance.ShutDownMsgBox(dis);}
            );
            Flash();
        }

        public void ResetAll()
        {
            UIManager.instance.OpenMsgBoxYN(
                dis,
                "确定要重置吗",
                ()=>
                {
                    GameManager.instance.InputsReset();
                    UIManager.instance.ShutDownMsgBox(dis);
                },
                () =>
                {
                    UIManager.instance.ShutDownMsgBox(dis);
                },
                new Vector2(800,400)
            );
        }

        void Start()
        {
            gm = GameManager.instance;
            Flash();
        }

        void Update()
        {
            Flash();
        }
    }
}

