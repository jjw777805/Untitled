using MyManager;
using MyPlayer;
using UnityEngine;

namespace MyCamera
{
    public class CameraRelease : MonoBehaviour
    {
        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                var t = CameraManager.instance.gameObject.GetComponent<CameraFollow>();
                t.targetObject = Player.instance.gameObject;
                Debug.Log("Exit!");
                // t.SetLock(false);
            }
        }
    }
}