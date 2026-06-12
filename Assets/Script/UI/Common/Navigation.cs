using System;
using System.Collections;
using System.Threading;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MyUI
{
    [AddComponentMenu("MyUI/navigation", 30)]
    public class Navigation : MonoBehaviour,ISelectHandler,IDeselectHandler,ICancelHandler
    {
        public Selectable up,down,left,right,cancel;
        private MyInput inputs;
        private bool isSelect=false;
        static float beginTime;

        public virtual void OnCancel(BaseEventData eventData)
        {
            cancel?.Select();
        }
        public virtual void OnSelect(BaseEventData eventData)
        {
            isSelect = true;  
        }

         public virtual void OnDeselect(BaseEventData eventData)
        {
            isSelect = false;
        }
        public void Start()
        {
            inputs=GameManager.instance.GetInputs();
            beginTime=Time.realtimeSinceStartup;
            
        }
        public void Update()
        {
            float delta = Time.realtimeSinceStartup - beginTime;
            if(delta < 0.2f )return ;
            if (inputs.Player.Move.IsPressed() && isSelect )
            {   
                Vector2 move = inputs.Player.Move.ReadValue<Vector2>();
                if (move.x > 0.1f)right?.Select();
                else if (move.x < -0.1f)left?.Select();
                
                if (move.y > 0.1f) up?.Select();
                else if (move.y < -0.1f)  down?.Select();

                beginTime = Time.realtimeSinceStartup;
            }
        }

    }
}