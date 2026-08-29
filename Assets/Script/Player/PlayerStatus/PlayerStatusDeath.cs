
using MyTransition;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        public AssetReference transRef;
        TransitionTemplate trans;

        public async void TransRefLoad()
        {
            GameObject t;
            if (!transRef.IsValid())
            {
                await transRef.LoadAssetAsync<GameObject>().Task;
            }
            t = Instantiate((GameObject)transRef.Asset);
            DontDestroyOnLoad(t);
            trans = t.GetComponent<TransitionTemplate>();
        }

        public void TransRefClear()
        {
            if(trans!=null)Destroy(trans.gameObject);
            if(transRef.IsValid())transRef.ReleaseAsset();
        }
        void Death()
        {
            Player.instance.Hide();
            trans.SetIn(
                ()=>
                {
                    trans.SetOut(
                        () =>
                        {
                            GameManager.instance.ScenesReload(
                                () =>
                                {
                                    Player.instance.Show();
                                    Time.timeScale = 1;
                                }
                            );
                        }
                    );
                    
                }
            );
            
        }
    }
}