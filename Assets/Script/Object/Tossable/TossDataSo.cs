using UnityEngine;

namespace MyObject
{
    [CreateAssetMenu(menuName ="Game/Toss/TossData")]
    [SerializeField]
    public class TossDataSo : ScriptableObject
    {
        public GameObject TossObject;
        public bool shotOnce = false;
        /// <summary>
        /// 需要几次才能被损坏
        /// </summary>
        [Tooltip("需要攻击几次才能被损坏/要在Attri中启用")]
        public int CrashTime = 3;
        public float TossSeq = 3.0f;
        public float AliveTime = 5.0f;
        /// <summary>
        /// 起点的loaclposition
        /// </summary>
        public Vector3 beginPos = Vector3.zero;
        public Vector3 beginVelocity = Vector3.zero;
        /// <summary>
        /// 是否保持恒定速度
        /// </summary>
        public bool isConstant = true;
        public float gravity=1.0f;
    }
}