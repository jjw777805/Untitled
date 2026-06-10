using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MyUI
{
    [AddComponentMenu("MyUI/navigation", 30)]
    public class Navigation : MonoBehaviour,ISelectHandler,IDeselectHandler
    {
        public Selectable up,down,left,right;
        private bool isSelected=false;
        private MyInput inputs;
        public void OnSelect(BaseEventData baseEvent)
        {
            isSelected=true;
        }

        public void OnDeselect(BaseEventData baseEvent)
        {
            isSelected=false;
        }
        public void Start()
        {
            inputs=GameManager.instance.GetInputs();
            inputs.Player.Move.GetBindingIndex();
        }
        public void Update()
        {
            Vector2 move = inputs.Player.Move.ReadValue<Vector2>();

            if (move.x > 0.1f)right?.Select();
            else if (move.x < -0.1f)left?.Select();
            
            if (move.y > 0.1f) up?.Select();
            else if (move.y < -0.1f)  down?.Select();
        }
    }
}