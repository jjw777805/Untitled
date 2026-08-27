
using System.Data;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine.Events;

namespace MyPlayer
{
    public partial class PlayerData
    {
        [JsonProperty]
        public int money = 0;
        void NewMoney()
        {
            money = 0;
        }

        float moneyRatio = 1.0f;
        private UnityEvent<int> m_MoneyChange = new UnityEvent<int>();

        [JsonIgnore]
        ///
        /// int为delta
        /// 
        public UnityEvent<int> onMoneyChange {
            get
            {
                return m_MoneyChange;
            }
            set
            {
                onMoneyChange = value;
            }
        }
        public void MoneyDelta(int x , bool ratio = true)
        {
            int delta = (int)(x*moneyRatio);
            if(ratio) delta = (int)(x*moneyRatio);
            else delta = x;

            money+= delta;
            if(x!=0)onMoneyChange?.Invoke(delta);
        }

        public int MoneyGet()
        {
            return money;
        }

        float healRatio = 1.0f;
        public int healCost = 50;
        int initialHealTime = 1;

        public void MoneyReset()
        {
            int need = initialHealTime * MoneyGetHealCost();
            if(money<need)MoneyDelta(need);
        }

        public int MoneyGetHealCost()
        {
            return (int)(healCost * healRatio);
        }
        public bool MoneyCanHeal()
        {
            int delta = MoneyGetHealCost();
            if(delta<=money)return true;
            else return false;
        }
        public void MoneyHeal()
        {
            MoneyDelta( -MoneyGetHealCost() );
        }
    }
}