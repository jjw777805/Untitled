using UnityEngine;

namespace MyObject
{
    public class Tosser : MonoBehaviour
    {
        public class TossData
        {
            public Tossable TossObject;
            public float TossSeq = 3.0f;
            public float AliveTime = 5.0f;
        }
    }
}