
using MyUtils;
using UnityEngine;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        public string originalLayer;
        public string hiddenLayer;

        public void Hide()
        {
            SetLayerRecursively(gameObject, LayerMask.NameToLayer(hiddenLayer));
        }

        public void Show()
        {
            SetLayerRecursively(gameObject, LayerMask.NameToLayer(originalLayer));
        }

        private void SetLayerRecursively(GameObject obj, int layer,bool rec = false)
        {
            obj.layer = layer;
            if(!rec)return ;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }
    }
}