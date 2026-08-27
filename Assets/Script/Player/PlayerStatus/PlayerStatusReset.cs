
using System.Threading.Tasks;
using MyUtils;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        Vector3 lastStablePos;

        public void ResetStatus(Vector3 pos)
        {
            Initialize();
            Player.instance.transform.position = pos;
            GameManager.instance.playerData.MoneyReset();
        }

        void Reborn()
        {
            transform.position = lastStablePos;
        }
    }
}