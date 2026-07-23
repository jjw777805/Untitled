
using MyUtils;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        Vector3 restPos= new Vector3(-10.99f,0.77f,0);
        Vector3 lastStablePos;

        public void SetResetPos(Vector3 p)
        {
            restPos = p;
        }
        public void Reset()
        {
            Initialize();
            Player.instance.transform.position = restPos;
        }

        void Reborn()
        {
            transform.position = lastStablePos;
        }
    }
}