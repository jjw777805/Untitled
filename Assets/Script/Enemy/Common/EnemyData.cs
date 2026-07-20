

using UnityEngine;

namespace MyEnemy{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Game/EnemyData")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;
        public float hp;
        public float attackDamage;
        public float atkSeq;
        public float atkDis;
        public float catchDis;
        public float catchSpeed;
        public float lossPlayerTime;
        public float patrolDis;
        public float patrolSpeed;
        public float stunTime;
    }    
}