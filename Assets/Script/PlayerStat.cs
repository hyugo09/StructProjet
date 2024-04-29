using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public int vague = 0;
    public int ennemieRestant;
    private void Start()
    {
        var temp = FindAnyObjectByType<DataTransfert>();
        HP += temp.bonusHealth;
        setStatUi();
        VagueSuivante();
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
    public void VagueSuivante()
    {
        if(vague == 5)
        {

        }
        vague++;
        ennemieRestant = vague * 50;
        
    }
    public bool ennemieBattu()
    {
        ennemieRestant--;
        if(ennemieRestant <= 0)
        {
            
        }
        return false;
    }
    internal void GameOver()
    {
        FindAnyObjectByType<DataTransfert>().lastGameWin = false;
        endScreen.SetActive(true);
    }
    internal void GameWin()
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
