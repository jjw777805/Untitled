using MyUI;
using UnityEngine;
using UnityEngine.Analytics;

namespace MyScene
{
    [AddComponentMenu("SceneController/MainMenu", 30)]
    
    public class MainMenu: Scene
    {
        protected MainMenu()
        {
            
        }

        void Update()
        {
            if(defaultOpen == null)
            {
                defaultOpen = GameObject.FindWithTag("MainMenu")?.GetComponent<Closable>();
            }
            if(defaultOpen == null)return ;
            Open();
        }
    }
}