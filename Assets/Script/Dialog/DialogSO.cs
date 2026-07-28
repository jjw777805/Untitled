using MyEnum;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MyDiaLog
{
    [CreateAssetMenu(menuName ="Game/Dialog")]
    public class DiaLogSO:ScriptableObject
    {
        public DialogType type;
        public DialogPos pos;
        public string personName;
        public AssetReference image;
        [TextArea(3,10)]
        public string content;
        public DiaLogChoiceSO choice;
        public DiaLogSO next;
    }
}

namespace MyEnum
{
    public enum DialogType
    {
        Text,Choice,End
    }
    public enum DialogPos
    {
        Left,Right
    }
}