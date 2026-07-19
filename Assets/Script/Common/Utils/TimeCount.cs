using System.Drawing;
using UnityEditor.PackageManager;
using UnityEditor.Rendering.Universal;
using UnityEngine;
using UnityEngine.Events;

namespace MyUtils{
    public class TimeCount
    {
        private UnityEvent on_End = new UnityEvent();
        public UnityEvent On_End
        {
            get
            {
                return on_End;
            }
            set
            {
                on_End = value;
            }
        }
        float time;
        float deltaTime;

        public TimeCount()
        {
            
        }

        public TimeCount(float t )
        {
            time=t;
        }
        public TimeCount(float t , UnityEvent e)
        {
            time=t;
            on_End=e;
        }
        public void Update(float t)
        {
            deltaTime += t;
            if(t>time)on_End?.Invoke();
        }
        public void Reset()
        {
            deltaTime = 0;
        }
    }

}