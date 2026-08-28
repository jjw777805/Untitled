using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

namespace MyTransition
{
    public abstract class TransitionTemplateSO : ScriptableObject
    {
        abstract public void SetIn();
        abstract public void SetOut();
    }
}