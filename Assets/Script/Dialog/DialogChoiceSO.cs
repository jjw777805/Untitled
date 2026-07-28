using System.Collections.Generic;
using UnityEngine;

namespace MyDiaLog
{
    [CreateAssetMenu(menuName ="Game/DialogChoice")]
    public class DiaLogChoiceSO:ScriptableObject
    {   
        [System.Serializable]
        public class DialogChoiceData
        {
            public string choiceText;      // 选项显示的文字
            public DiaLogSO next;    
            public UnityEngine.Events.UnityEvent onClick;
        }

        public List<DialogChoiceData> choices;   // 选项列表
    }
}

