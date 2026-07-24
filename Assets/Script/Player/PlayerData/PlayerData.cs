using System;
using UnityEngine;

namespace MyPlayer
{
    public enum Weapon
    {
        NONE,JI,GUN,DAO,BIAN
    }

    [Serializable]
    public partial class PlayerData
    {
        void NewPlayerData()
        {
            NewAttack();
            NewHp();
            NewPosition();
        }
        public PlayerData()
        {
            NewPlayerData();
        }
        
    }
}