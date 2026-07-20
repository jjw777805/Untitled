
using MyUtils;
using UnityEngine;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        public void QuitHurt()
        {
            am.SetBool("Hurt",false);
        }
        public void Hurt()
        {
            ps.Hurt();
            am.SetBool("Hurt",true);
        }
    }
}