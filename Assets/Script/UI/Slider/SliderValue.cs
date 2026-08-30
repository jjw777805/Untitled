using MyUI;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace MyUtils
{
    public class SliderValue:MonoBehaviour
    {
        public bool isConstant = true;
        public int minNumber,maxNumber;
        public int roundNumber = 0;
        string txt;
        public TMP_Text tmpText;

        public Slider slider;

        void GetValue(float value)
        {
            float trueValue = Mathf.Lerp(minNumber,maxNumber,value);
            if (isConstant)
            {
                txt = trueValue.ToString("F0");
            }
            else
            {
                if(roundNumber<0)roundNumber = 0;
                txt = trueValue.ToString("F"+roundNumber.ToString());
            }

            tmpText.text = txt;
        }

        void Start()
        {
            slider.onValueChanged.AddListener(GetValue);
        }

        void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(GetValue);
        }

    }
}