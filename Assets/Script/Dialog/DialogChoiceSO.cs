using System.Collections.Generic;
using UnityEngine;

namespace MyDiaLog
{
    [CreateAssetMenu(menuName ="Game/DialogChoice")]
    public class DiaLogChoiceSO:ScriptableObject
    {   
        public List<DiaLogSO>dialogList; 
    }
}

