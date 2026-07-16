using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        bool onGround = true;

        public bool OnGround()
        {
            return true;
        }

        void OnGroundUpdate()
        {
            onGround = OnGround();
        }
    }
}