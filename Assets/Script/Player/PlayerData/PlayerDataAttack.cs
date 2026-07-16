
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerData
    {
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
    
        
    }
}