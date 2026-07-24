
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        void Death()
        {
            GameManager.instance.ScenesReload();
        }
    }
}