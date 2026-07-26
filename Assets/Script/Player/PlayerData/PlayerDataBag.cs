
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyObject;
using Myutils;
using Newtonsoft.Json;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerData
    {
        [JsonProperty]
        Dictionary<string,string> playerBag =
            new Dictionary<string,string>();

        LoadSource<CollectionCommonData>collectionSource= new LoadSource<CollectionCommonData>();
        void NewBag()
        {
            playerBag = new Dictionary<string,string>();
            collectionSource = new LoadSource<CollectionCommonData>();
        }
        public void BagAdd(string key , CollectionCommonData sc)
        {
            playerBag.Add(key,sc.soAddressPath);
            collectionSource.Load(key,sc.soAddressPath);
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
            var tasks=new List<Task>();
            foreach(var a in playerBag)
            {
                tasks.Add(collectionSource.LoadAsync(a.Key,a.Value));
            }
            try
            {
                await Task.WhenAll(tasks);
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                Debug.LogError(e.ToString());
            }
            
        }
    }
}