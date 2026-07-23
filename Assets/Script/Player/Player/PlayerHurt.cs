

using UnityEngine;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        public void QuitHurt()
        {
            am.SetBool("Hurt",false);
        }
        public void MayHurt(Vector3 deltaPos)
        {
            if (!ps.CanBlock())return ;
            ps.BlockBegin();
            //Warning！感觉会有bug后期可以重点关注
            BlockStart(Time.realtimeSinceStartup,deltaPos);
        }

        void Hurt()
        {
            ps.Hurt();
            am.SetBool("Hurt",true);
        }
        
    }
}