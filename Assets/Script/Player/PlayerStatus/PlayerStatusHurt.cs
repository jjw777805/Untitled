
using MyUtils;
using Unity.VisualScripting;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        int hp;
        int hpMax;
        [SerializeField]
        float injuryTime = 1.0f;
        bool isinjury=false;
        TimeCount hurtTc;

        void SetIsInjury(bool f)
        {
            isinjury = f;
        }

        void HurtBinding()
        {
            hurtTc = new TimeCount(injuryTime);
            hurtTc.On_End.AddListener(()=>{SetIsInjury(false);Player.instance.QuitHurt();});
        }
        public void HurtInitial()
        {
            isinjury = false;
            hpMax = GameManager.instance.playerData.HP;
            hp = hpMax;
            UIManager.instance.SetHP(hp);
        }

        void HurtUpdate()
        {
            hurtTc.Update(Time.deltaTime);
        }

        public void Hurt(int k)
        {
           
            if(isinjury != false)return ;
            hurtTc.Reset();
            isinjury = true;
            hp-=k;
            if(hp<=0)Death();
            UIManager.instance.SetHP(hp);
        }
    }
}