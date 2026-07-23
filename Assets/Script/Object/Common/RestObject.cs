using MyPlayer;
using UnityEngine;
using UnityEngine.Playables;

namespace MyObject
{
    public class RestObject : MonoBehaviour
    {
        public Vector3 rebornPos;
        public void Rest()
        {
            PlayerStatus.instance.SetResetPos(rebornPos);
            GameManager.instance.ScenesReload();
        }

    }
}