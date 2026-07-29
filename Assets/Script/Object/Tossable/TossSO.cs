using UnityEngine;

namespace MyObject
{
    public class TossSO : ScriptableObject
    {
        /// <summary>
        /// 是否能被打击损坏
        /// </summary>
        public bool canAttackCrashed = false;
        /// <summary>
        /// 是否能被攻击
        /// </summary>
        public bool canAttack = false;
        /// <summary>
        /// 是否能被格挡
        /// </summary>
        public bool canBlock = true;
        /// <summary>
        /// 是否能反弹，如果不能，则碰到墙体就损坏
        /// </summary>
        public bool canBounce = false;
    }
}