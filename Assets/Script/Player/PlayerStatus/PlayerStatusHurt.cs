
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
            hp = GameManager.instance.playerData.HP;
            UIManager.instance.SetHP(hp);
            hurtTc = new TimeCount(injuryTime);
            hurtTc.On_End.AddListener(()=>{SetIsInjury(false);Player.instance.QuitHurt();});
        }

        void HurtUpdate()
        {
            hurtTc.Update(Time.deltaTime);
        }
        public void Hurt()
        {
           
            if(isinjury != false)return ;
            hurtTc.Reset();
            isinjury = true;
            hp--;
            UIManager.instance.SetHP(hp);
        }
    }
}