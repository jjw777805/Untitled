using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace MyUI
{
    [Serializable]
    [AddComponentMenu("MyUI/Panel", 30)]
    public class Panel : Closable
    {
        protected Panel()
        {
        }
    }
}