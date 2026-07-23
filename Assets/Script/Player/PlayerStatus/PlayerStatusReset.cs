
using MyUtils;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        Vector3 restPos , lastStablePos;
        void RebornInitial()
        {
        }
        void Reset()
        {
            
        }

        void Reborn()
        {
            transform.position = lastStablePos;
        }
    }
}