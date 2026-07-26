


using UnityEngine;

namespace MyObject{
    public abstract class CollectionCommonData : ScriptableObject
    {
        public string uniqueName;
        public string objName;
        public string soAddressPath;
        public string ImagePath;
        [TextArea(3,10)]
        public string shortDiscription;
        [TextArea(3,10)]
        public string detailDiscription;
        public abstract void Apply();
        public abstract void Remove();
        public abstract void Use();
        public abstract void Desert();
    }    
}