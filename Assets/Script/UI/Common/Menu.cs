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
    [AddComponentMenu("MyUI/Menu", 30)]
    public class Menu : Closable
    {
        protected Menu()
        {
        }

    }
}