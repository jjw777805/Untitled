using System;
using TMPro;
using UnityEngine;
namespace MyUI
{
    [Serializable]
    [AddComponentMenu("MyUI/MessageBox", 30)]
    public class MessageBox : Closable
    {
        [SerializeField]
        private TMP_Text MessageText;
        [SerializeField]
        private RectTransform rect;
        protected MessageBox()
        {
        }

        public virtual void SetMessage(string txt)
        {
            MessageText.SetText(txt);
        } 

        public virtual void SetSize(Vector2 size)
        {
            rect.sizeDelta = size;
        }
    }
}