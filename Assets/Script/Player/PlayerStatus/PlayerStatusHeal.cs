
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

namespace MyPlayer
{
    public partial class PlayerStatus
    {
        bool isHeal = false;
        public float healSeq = 0.5f;
        float healDeltaTime=0.0f;

        void HealInitial()
        {
            healDeltaTime =0;
            isHeal = false;
        }

        public bool CanHeal()
        {
            if(isAttack || isSlide || isBlock || isJump || isHeal)return false;
            if(hp==hpMax)return false;
            if(!GameManager.instance.playerData.MoneyCanHeal())return false;
            return true;
        }

        public void Heal()
        {
            isHeal = true;
            GameManager.instance.playerData.MoneyHeal();
            if (hp < hpMax)
            {
                hp++;
                UIManager.instance.SetHP(hp);
            }
            healDeltaTime=0;
        }

        void HealUpdate()
        {
            if(!isHeal)return;
            healDeltaTime += Time.deltaTime;
            if(healDeltaTime > healSeq)isHeal = false;
        }
    }
}