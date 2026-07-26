using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyUI
{
    public class CltDiscription:MonoBehaviour
    {
        public TMP_Text brief;
        public Image image;

        public void SetImage(Sprite sp)
        {
            image.sprite = sp;
        }    

        public void SetBriefText(string txt)
        {
            brief.SetText(txt);
        }

        void Start()
        {
            brief.SetText("");
            image.sprite = null;
        }
    }
}