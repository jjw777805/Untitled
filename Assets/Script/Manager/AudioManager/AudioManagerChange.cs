using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace MyManager
{
    public partial class AudioManager : MonoBehaviour
    {
        protected float ConvertLinearToDecibel(float linear)
        {
            if (linear <= 0.0001f)return  -80f;
            return Mathf.Log10(Mathf.Clamp01(linear)) * 20f; 
        }

        public void SetVolumn(string name,float value)
        {
            config.dic[name]=value;
            // Debug.Log(name + " " +ConvertLinearToDecibel(value).ToString());
            float dB = ConvertLinearToDecibel(value);
            bool success = ad.SetFloat(name, dB);
            // if (!success)
            // {
            //     Debug.LogError($"设置音量失败！参数名: '{name}' 在 AudioMixer 中不存在。请检查暴露名称是否完全一致。");
            // }
            // else
            // {
            //     Debug.Log($"成功设置 {name} = {dB} dB");
            // }
            SaveToJson();
        }

        public void ResetVolumn()
        {
            var t = new AudioConfig().dic;
            foreach( var i in t)
            {
                SetVolumn(i.Key,i.Value);
            }
        }
    }
 
}
