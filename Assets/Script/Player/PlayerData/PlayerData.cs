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
            NewBag();
            NewMoney();
        }

        public void Clear()
        {
            BagClear();
        }
        public PlayerData()
        {
            NewPlayerData();
        }
        

    }
}