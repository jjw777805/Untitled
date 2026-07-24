using MyPlayer;
using UnityEngine;

namespace MyManager
{
    public partial class CameraManager : MonoBehaviour
    {
        public void ResetStatus()
        {
            Vector3 t = Player.instance.transform.position;
            t.z=transform.position.z;
            transform.position = t;
        }
    }
 
}
