
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerData
    {
         [SerializeField]
        int hp = 3;
        public int HP
        {
            set
            {
                hp = value;
            }
            get
            {
                return hp;
            }
        }
        

        [SerializeField]
        Weapon weapon = Weapon.NONE;
        public Weapon Weapon{
            set
            {
                weapon = value;
            }
            get
            {
                return weapon;
            }
        }

        [SerializeField]
        int damage = 5;
        public int Damage 
        {
            set
            {
                damage = value;
            }
            get
            {
                return damage;
            }
        }
    
        void NewHp()
        {
            hp = 3;
        }
        void NewAttack()
        {
            weapon = Weapon.NONE;
            damage = 5;
        }
        
    }
}