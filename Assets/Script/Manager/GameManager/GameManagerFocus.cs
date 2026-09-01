using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public partial class GameManager 
{
    GameObject focus = null;

    void FocusUpdate()
    {
        if(EventSystem.current!=null && EventSystem.current.currentSelectedGameObject != null)
        {
            focus = EventSystem.current.currentSelectedGameObject;
        }
    }
    void FocusReset()
    {
        if(EventSystem.current!=null && EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(focus);
        }
    }
}
