
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MyUtils;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace MyPlayer
{
    public partial class PlayerData
    {
        [JsonProperty]
        Dictionary<string,bool> boolDiction = new Dictionary<string, bool>();
        /// <summary>
        /// 建议设计变量名时初始值为false
        /// </summary>
        /// <param name="name"></param>
        /// <param name="k"></param>
        public void BoolSet(string name,bool k)
        {
            boolDiction[name]=k;
        }
        /// <summary>
        /// 如果查找不到返回false
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool BoolGet(string name)
        {
            if (boolDiction.ContainsKey(name))
            {
                return boolDiction[name];
            }
            return false;
        }

        void NewBoolDic()
        {
            boolDiction = new Dictionary<string, bool>();
            boolDiction["NewGame"] = true;
        }
    }
}