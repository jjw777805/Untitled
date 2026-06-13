using System;

using Unity.VisualScripting;
using UnityEngine;

namespace Myutils
{
    [Serializable]
    class ColorChange
    {
        [SerializeField]
        Color from;
        public Color From
        {
            set
            {
                From = value;
            }
            get
            {
                return  from;
            }
        }
        [SerializeField]
        Color to;
        public Color To
        {
            set
            {
                to = value;
            }
            get
            {
                return to;
            }
        }
    }
}