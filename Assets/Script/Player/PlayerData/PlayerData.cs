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
        [SerializeField]
        int hp = 3;
        public int HP
        {
            set
            {
                HP = value;
            }
            get
            {
                return hp;
            }
        }
        

        [SerializeField]
        Weapon weapon = Weapon.NONE;
        public Weapon Weapon
        {
            set
            {
                weapon = value;
            }
            get
            {
                return weapon;
            }
        }

        
    }
}