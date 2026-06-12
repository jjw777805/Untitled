using MyUI;
using UnityEngine;

namespace MyScene
{
    [AddComponentMenu("SceneController/MainMenu", 30)]
    
    public class Scene:MonoBehaviour
    {
        [SerializeField]
        Closable defaultOpen;
        protected Scene()
        {
        }

        bool isOpened = false;
        public virtual void OpenCanvas()
        {
            defaultOpen?.Open();
        }
        
        public virtual void Open()
        {
            if(isOpened)return ;
            OpenCanvas();

            isOpened = true;
        }
    }
}