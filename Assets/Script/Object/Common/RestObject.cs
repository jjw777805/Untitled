using MyPlayer;
using UnityEngine;
using UnityEngine.Playables;

namespace MyObject
{
    public class RestObject : MonoBehaviour
    {
        public Vector3 rebornPos;
        public virtual void Rest()
        {
            GameManager.instance.playerData.SetResetPos(rebornPos);
            GameManager.instance.ScenesReload();
        }

    }
}