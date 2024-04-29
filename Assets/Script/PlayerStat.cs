using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    private int MaxMoney = 10000;
    public int money = 1000;
    public int HP = 3;
    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textHP;
    public GameObject endScreen;
    public TextMeshProUGUI endText;

    private void Start()
    {
        setStatUi();
    }
    public void degat(int degat)
    {
        HP -= degat;
        if (HP <= 0)
        {
            GameOver();
        }
        setStatUi();
    }
    public void GagnerArgent(int won)
    {
        if(money + won <= MaxMoney)
        {
            money += won;
        }
        else
        {
            money = MaxMoney;
        }
        setStatUi();
    }
    internal void GameOver()
    {
        FindAnyObjectByType<DataTransfert>().lastGameWin = false;
        endScreen.SetActive(true);
    }
    public void setStatUi()
    {
        textHP.text = HP.ToString();
        textMoney.text = money.ToString();
    }
}
