
using Newtonsoft.Json;
using UnityEngine.Events;

namespace MyPlayer
{
    public partial class PlayerData
    {
        [JsonProperty]
        int money = 0;
        void NewMoney()
        {
            money = 0;
        }


        private UnityEvent<int> m_MoneyChange = new UnityEvent<int>();

        [JsonIgnore]
        ///int为delta
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
        public void MoneyDelta(int x)
        {
            money+=x;
            if(x!=0)onMoneyChange?.Invoke(x);
        }

        public int MoneyGet()
        {
            return money;
        }
    }
}