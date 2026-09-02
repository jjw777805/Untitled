using MyManager;
using UnityEngine;

namespace MyCamera
{
    public class CameraLock : MonoBehaviour
    {
        /// <summary>
        /// 需要锁定的位置
        /// </summary>
        [Tooltip("需要锁定的位置")]
        public Vector3 lockPos;
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                var t = CameraManager.instance.gameObject.GetComponent<CameraFollow>();
                t.targetObject = gameObject;
                // t.SetLock(true);
                // t.SetPosition(lockPos);
            }
        }
    }
}