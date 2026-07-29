

using System;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        int hurtDamage;
        public void QuitHurt()
        {
            am.SetBool("Hurt",false);
        }
        public void MayHurt(Vector3 deltaPos,int damage,BlkType type,Action success,Action failed)
        {
            if(type==BlkType.Trap && !ps.CanTrap())return ;
            hurtDamage = damage;
            blkType = type;
            if (!ps.CanBlock())return ;
            blkS = success;
            blkF = failed;
            ps.BlockBegin();
            //Warning！感觉会有bug后期可以重点关注
            BlockStart(Time.realtimeSinceStartup,deltaPos);
        }

        void Hurt(int damage)
        {
            ps.Hurt(damage);
            am.SetBool("Hurt",true);
        }

        public void Trap(int damage)
        {
            am.SetBool("Hurt",true);
            ps.Trap(damage);
        }
        
    }
}