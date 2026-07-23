
using MyUtils;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        int hp;
        [SerializeField]
        float injuryTime = 1.0f;
        bool isinjury=false;
        TimeCount hurtTc;

        void SetIsInjury(bool f)
        {
            isinjury = f;
        }
        void HurtInitial()
        {
            isinjury = false;
            hp = GameManager.instance.playerData.HP;
            UIManager.instance.SetHP(hp);
            hurtTc = new TimeCount(injuryTime);
            hurtTc.On_End.AddListener(()=>{SetIsInjury(false);Player.instance.QuitHurt();});
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
            UIManager.instance.SetHP(hp);
            if(hp<=0)Death();
        }
    }
}