using UnityEngine;

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
            Open();
        }
    }
}