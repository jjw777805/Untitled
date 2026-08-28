using System;
using UnityEngine;

namespace MyTransition
{
    public abstract class TransitionTemplate :MonoBehaviour
    {
        public bool finished;
        abstract public void SetIn(Action onComplete=null);
        abstract public void SetOut(Action onComplete=null);
    }
}