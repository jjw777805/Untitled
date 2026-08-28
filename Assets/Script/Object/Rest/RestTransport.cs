using MyTransition;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MyObject
{
    public class RestTransport : RestObject
    {
        public string portName;
        public AssetReference transRef;
        TransitionTemplate trans;

        void Clear()
        {
            if(trans!=null)Destroy(trans.gameObject);
            if(transRef.IsValid())transRef.ReleaseAsset();
        }
        async void Initial()
        {
            GameObject t;
            if (!transRef.IsValid())
            {
                await transRef.LoadAssetAsync<GameObject>().Task;
            }
            t = Instantiate((GameObject)transRef.Asset);
            trans = t.GetComponent<TransitionTemplate>();
        }
        public override void Rest()
        {
            base.Rest();
            var pd = GameManager.instance.playerData;
            string queryKey = portName+"_is_active";
            if(pd.BoolGet(queryKey)==true)return ;
            // transRef.LoadAssetAsync<GameObject>();
            pd.BoolSet(queryKey,true);
            pd.HPLimitChange(1);
            trans.SetIn(
                ()=>
                {        
                    trans.SetOut();
                }
            );
            
        }

        void Awake()
        {
            Initial();
        }

        void OnDestroy()
        {
            Clear();
        }
    }
}