
using System;
using MyEnum;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MyScene
{
    [CreateAssetMenu(menuName ="Game/Scene")]
    [Serializable]
    public class SceneSO:ScriptableObject
    {
        public AssetReference sceneReference;
        public string sceneName;
        public string showName;
    }
}
