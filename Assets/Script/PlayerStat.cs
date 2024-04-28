using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public int money = 0;
    public int HP;
    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textHP;

    public void setStatUi()
    {
        textHP.text = HP.ToString();
        textMoney.text = money.ToString();
    }
}
