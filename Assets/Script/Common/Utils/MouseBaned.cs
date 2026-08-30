using UnityEngine;
using UnityEngine.InputSystem;

namespace MyUtils
{
    public class MouseBaned : MonoBehaviour
    {
        void Awake()
        {
            if (Application.isEditor)
            {
                // 在编辑器里，不做任何禁用操作，保持鼠标可用
                Debug.Log("在编辑器模式下，鼠标保持启用。");
                return;
            }

            // 在非编辑器（即正式游戏）环境下禁用鼠标
            if (Mouse.current != null)
            {
                InputSystem.DisableDevice(Mouse.current);
                Debug.Log("鼠标设备已被禁用。");
            }
            else
            {
                Debug.LogWarning("未找到鼠标设备。");
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}