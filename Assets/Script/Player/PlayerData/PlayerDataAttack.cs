using System;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEditor.PackageManager;
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