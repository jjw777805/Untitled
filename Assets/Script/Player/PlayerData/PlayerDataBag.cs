
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyObject;
using Myutils;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace MyPlayer
{
    public partial class PlayerData
    {
        [JsonProperty]
        Dictionary<string,string> playerBag =
            new Dictionary<string,string>();

        LoadSource<CollectionCommonData>collectionSource= new LoadSource<CollectionCommonData>();
        LoadSource<Sprite>imageSource = new LoadSource<Sprite>();
        
        [JsonIgnore]
        public List<CollectionCommonData>collectionCommonDatas
            =new List<CollectionCommonData>();
        [JsonIgnore]
        public List<Sprite>collectionSprites
            =new List<Sprite>();
        void NewBag()
        {
            playerBag = new Dictionary<string,string>();
            collectionSource = new LoadSource<CollectionCommonData>();
        }
        async public void BagAdd(string key , CollectionCommonData sc)
        {
            playerBag.Add(key,sc.soAddressPath);
            var tasks = new List<Task>
            {
                collectionSource.LoadAsync(key, sc.soAddressPath),
                imageSource.LoadAsync(key, sc.ImagePath)
            };
            await Task.WhenAll(tasks);
            collectionCommonDatas.Add(collectionSource.Get(key));
            collectionSprites.Add(imageSource.Get(key));
        }
        public bool HasCollected(string key)
        {
            return playerBag.ContainsKey(key);
        }

        void BagClear()
        {
            collectionSource.Clear();
        }

        async Task LoadBag()
        {
            try
            {
                var tasks=new List<Task>();
                foreach(var a in playerBag)
                {
                    tasks.Add(collectionSource.LoadAsync(a.Key,a.Value));
                }
                await Task.WhenAll(tasks);
                tasks.Clear();
                foreach(var a in collectionSource.dic)
                {
                    tasks.Add(imageSource.LoadAsync(a.Key,a.Value.ImagePath));
                }
                await Task.WhenAll(tasks);

                collectionCommonDatas.Clear();
                collectionSprites.Clear();

                foreach(var a in playerBag.Keys)
                {
                    collectionCommonDatas.Add(collectionSource.Get(a));
                    collectionSprites.Add(imageSource.Get(a));
                }
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                Debug.LogError(e.ToString());
            }
            
        }
    }
}