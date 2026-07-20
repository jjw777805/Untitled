using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.SceneManagement;

namespace MyEnemy
{
    public partial class Enemy : MonoBehaviour
    {
        public virtual void Hurt(float damage){}
    }
}

