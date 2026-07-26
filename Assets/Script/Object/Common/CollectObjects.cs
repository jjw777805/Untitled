using MyPlayer;
using UnityEngine;
using UnityEngine.Playables;

namespace MyObject
{
    public class CollectObject : MonoBehaviour
    {
        public CollectionCommonData data;
        #region InterFace
        void Initialize()
        {
            bool t = GameManager.instance.HasCollected(data.uniqueName);
            if(t)Destroy(gameObject);
        }
        public void Collect()
        {
            GameManager.instance.PlayerBagAdd(data.uniqueName,data);
            Destroy(gameObject);
        }
        #endregion

        #region  生命周期函数

        void Start()
        {
            Initialize();
        }
        #endregion
    }
}