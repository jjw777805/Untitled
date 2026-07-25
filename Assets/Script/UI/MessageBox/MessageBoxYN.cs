
using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;

namespace MyUI
{
    public class MessageBoxYN : MessageBox
    {
        public Button yes,no;

        public MessageBoxYN()
        {
        }

        public void SetStatus(string msg,Action yesAction,Action noAction)
        {
            SetMessage(msg);
            yes.onClick.AddListener(()=>yesAction?.Invoke());
            no.onClick.AddListener(()=>noAction?.Invoke());
        }
    }
    
}