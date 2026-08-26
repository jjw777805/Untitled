using MyPlayer;
using TMPro;
using UnityEngine;

namespace MyUI
{
    public class UIMoney : MonoBehaviour
    {
        int showMoney;
        [SerializeField]
        TMP_Text text;

        void Start()
        {     
            showMoney = GameManager.instance.playerData.MoneyGet();
            text.text = showMoney.ToString();
            GameManager.instance.playerData.onMoneyChange.AddListener(ChangeShowMoney);
        }

        void OnDisable()
        {
            GameManager.instance.playerData.onMoneyChange.RemoveListener(ChangeShowMoney);
        }

        void ChangeShowMoney(int delta)
        {
            showMoney += delta;
            text.text = showMoney.ToString();
        }

    }
}