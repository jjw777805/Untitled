

using System;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public partial class GameManager 
{
    protected string GetInputsSavingPath()
    {
        return Path.Combine(Application.persistentDataPath,"inputs.json");
    }

    public string InputsGetKeyName(InputAction action,string bindingName)
    {
        if(action == null){Debug.LogError("empty action");return null;}
        int bindingIndex = FindBindingIndex(action, bindingName);
        if(bindingIndex == -1 ){Debug.LogError("empty binding");return null;}
        return action.GetBindingDisplayString(bindingIndex);
    }
    public void SaveInputsToJson()
    {
        string json = instance.inputs.asset.SaveBindingOverridesAsJson();
        File.WriteAllText(GetInputsSavingPath(),json);
    }

    public void LoadInputsFromJson()
    {
        string path = GetInputsSavingPath();
        if (!File.Exists(path))
        {
            Debug.Log("No Such InputsJson File");
            InputsReset();
        }
        else
        {
            string json = File.ReadAllText (path);
            var inputsAsset = instance.inputs.asset;
            inputsAsset.Disable();
            inputsAsset.LoadBindingOverridesFromJson(json);
            inputsAsset.Enable();
        }
        
    }

    public InputAction GetInputAction(string mapName, string actionName){
        string fullPath = $"{mapName}/{actionName}";
        var action = instance.inputs.asset.FindAction(fullPath);
        if (action == null) { Debug.LogError($"未找到: {fullPath}"); return null; }
        return action;
    }

    public void InputsRebinding(string mapName, string actionName, string bindingName = null, 
                               bool autoResolveConflicts = true, Action onComplete = null,
                               Action onCancel = null)
    {

        var action = GetInputAction(mapName,actionName);

        int bindingIndex = FindBindingIndex(action, bindingName);
        if (bindingIndex == -1) return;
        action.Disable();
        var nowEventSystem = EventSystem.current;
        nowEventSystem.enabled = false;
        var rebindOp = action.PerformInteractiveRebinding()
            .WithTargetBinding(bindingIndex)
            .WithControlsExcluding("Mouse")
            .WithControlsExcluding("<Keyboard>/escape")
            .WithControlsExcluding("<Keyboard>/anyKey")
            .WithCancelingThrough("<Keyboard>/escape")
            // .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                Debug.Log($"OnComplete triggered, selectedControl path: {operation.selectedControl?.path}");
                //获取玩家刚设置的新按键路径
                string newBindingPath = action.bindings[bindingIndex].overridePath;

                if (autoResolveConflicts && !string.IsNullOrEmpty(newBindingPath))
                {
                    RemoveConflictingBindings(instance.inputs.asset, action, bindingIndex, newBindingPath);
                }

                SaveInputsToJson();
                onComplete?.Invoke();
                action.Enable();
                nowEventSystem.enabled = true;
                Debug.Log($"重绑成功：{mapName}/{actionName} -> {action.GetBindingDisplayString(bindingIndex)}");
                operation.Dispose();
                FocusReset();
            })
            .OnCancel(op => 
            { 
                Debug.Log("Canceled !");
                nowEventSystem.enabled = true;
                action.Enable(); 
                onCancel?.Invoke();
                op.Dispose(); 
                FocusReset();
            }
            )
            .Start();
           
    }

    protected void RemoveConflictingBindings(InputActionAsset asset, InputAction currentAction, 
                                           int currentBindingIndex, string conflictPath)
    {
        Debug.Log("Check!:"+conflictPath);
        foreach (var map in asset.actionMaps)
        {
            if(map!=currentAction.actionMap)continue;
            foreach (var action in map.actions)
            {
                for (int i = 0; i < action.bindings.Count; i++)
                {
                    // 跳过当前正在修改的绑定本身
                    if (action == currentAction && i == currentBindingIndex) continue;

                    // 检查路径是否完全匹配（说明撞键了）
                    
                    string effectivePath = action.bindings[i].overridePath ?? action.bindings[i].path;
                    // Debug.Log(effectivePath);
                    if (effectivePath == conflictPath)
                    {
                        Debug.Log($"移除冲突绑定：{action.name} 的 {effectivePath}");
                        action.ApplyBindingOverride(i, ""); // 清空覆盖
                    }
                }
            }
        }
    }

    protected int FindBindingIndex(InputAction action, string bindingName)
    {
        if (string.IsNullOrEmpty(bindingName) 
        || bindingName.StartsWith("null",StringComparison.OrdinalIgnoreCase))
        {
            // 简单动作：自动找第一个键盘绑定
            for (int i = 0; i < action.bindings.Count; i++)
                if (action.bindings[i].groups.Contains("Keyboard&Mouse")
                && action.bindings[i].path.StartsWith("<Keyboard>/", StringComparison.OrdinalIgnoreCase)) return i;
            return 0;
        }
        else
        {
            // 复合动作：根据 Binding Name 精确查找（如 "up" / "down"）
            for (int i = 0; i < action.bindings.Count; i++)
            {
                if (String.Equals(action.bindings[i].name,bindingName,StringComparison.OrdinalIgnoreCase)) return i;
                // Debug.Log(action.bindings[i].name);
            }
                
            return -1;
        }
    }

    public void InputsReset()
    {
        instance.inputs.asset.RemoveAllBindingOverrides();
        SaveInputsToJson();
    }
}
