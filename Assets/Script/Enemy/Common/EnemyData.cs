

using System.Collections.Generic;
using MyObject;
using UnityEngine;

namespace MyEnemy{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Game/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public int hp =15;
        public float attackDamage;
        public float atkSeq;
        public float atkMinDis;

        public float atkMaxDis;
        public float catchDis;
        public float catchSpeed;
        public float lossPlayerTime;
        public float patrolDis;
        public float patrolSpeed;
        public float stunTime;

        public float canSeeTime;
        public int leftMoney;
        public List<CollectionData> leftCollection;
        public List<float> leftPossibility;
    }    
}