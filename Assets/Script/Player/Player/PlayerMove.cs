
using UnityEngine;

namespace MyPlayer
{
    public partial class Player:MonoBehaviour
    {
        public void Move()
        {
            if(   PlayerStatus.instance.isSlide == true
                )
            {
                return ;
            }


        }
    }
}