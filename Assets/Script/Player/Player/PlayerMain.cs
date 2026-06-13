using System.Numerics;
using UnityEngine;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        PlayerData playerData;
        void Start()
        {
            playerData = GameManager.instance.playerData;
            PlayerStatus.instance.Initialize(playerData);
        }
        void Update()
        {
            
        }
    }
}