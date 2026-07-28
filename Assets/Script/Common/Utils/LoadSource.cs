
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MyUtils
{
    public class LoadSource<T>
    {
        public Dictionary<string , T>dic = new Dictionary<string ,T >();
        public Dictionary<string , AsyncOperationHandle<T> >handleDic
            =new  Dictionary<string , AsyncOperationHandle<T> >();


        public LoadSource(){
            dic = new Dictionary<string ,T >();
            handleDic=new  Dictionary<string , AsyncOperationHandle<T> >();
        }
        public T Get(string key)
        {
            if (!Contains(key))
            {
                Debug.LogError("Not Contain this !");
                return default;
            }
            return dic[key];
        }
        
        async public void Load(string key,string sourcePath)
        {
            try
            {
                if(Contains(key))return ;
                var handle = Addressables.LoadAssetAsync<T>(sourcePath);
                T set = await handle.Task;
                dic.Add(key,set);
                handleDic.Add(key,handle);
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                Debug.LogError(e.ToString());
            }
        // Debug.Log("Open!: check "+name+ "\n"+string.Join(", ", MsgBoxList.Keys));
        }

        async public Task LoadAsync(string key,string sourcePath)
        {
            try
            {
                if(Contains(key))return ;
                var handle = Addressables.LoadAssetAsync<T>(sourcePath);
                T set = await handle.Task;
                dic.Add(key,set);
                handleDic.Add(key,handle);
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                Debug.LogError(e.ToString());
            }
        // Debug.Log("Open!: check "+name+ "\n"+string.Join(", ", MsgBoxList.Keys));
        }
        public bool Contains(string key)
        {
            return dic.ContainsKey(key);
        }
        public void Remove(string key)
        {
            if (Contains(key))
            {
                // Debug.Log("In!");
                Addressables.Release(handleDic[key]);
                dic.Remove(key);
                handleDic.Remove(key);
            }
        }

        public void Clear()
        {
            foreach (var kvp in handleDic)
            {
                Addressables.Release(kvp.Value);
            }
            handleDic.Clear();
            dic.Clear();
        }
    }
    
}