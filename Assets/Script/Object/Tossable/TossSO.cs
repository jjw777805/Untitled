using UnityEngine;

namespace MyObject
{
    [CreateAssetMenu(menuName ="Game/Toss/TossAttri")]
    public class TossSO : ScriptableObject
    {
        
        /// <summary>
        /// 是否能被攻击
        /// </summary>
        public bool canAttack = false;
        /// <summary>
        /// 受到攻击时是否进行反射
        /// </summary>
        public bool canReflect = false;
        /// <summary>
        /// 是否能被打击损坏
        /// </summary>
        public bool canAttackCrashed = false;
        /// <summary>
        /// 是否能被格挡
        /// </summary>
        public bool canBlock = true;
        /// <summary>
        /// 是否能反弹，如果不能，则碰到墙体就损坏
        /// </summary>
        public bool canBounce = false;
        /// <summary>
        /// 对对象造成伤害之后是否消失
        /// </summary>
        public bool hurtVanish = true;
        /// <summary>
        /// 是否能对enemy造成伤害
        /// </summary>
        public bool canHurtEnemy = false;

        /// <summary>
        /// 调整方向时用right还是left,true为用left
        /// </summary>
        public bool forwardLeft = true;
        /// <summary>
        /// 被击中是是否让Player重生
        /// </summary>
        public bool isReborn = true;
        public int playerDamage = 1;
        public int enemyDamage = 5;
    }
}