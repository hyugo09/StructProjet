using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransfert : MonoBehaviour
{
    public List<Turret> UnlockedTurret;
    public int bonusHealth;
    public bool lastGameWin;
    public bool[] nodeObtained = new bool[9];
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void nodeActivationf(int i)
    {
        nodeObtained[i] = true;
    }
    public void UnlockerTourelle(Turret tourelle)
    {
        if (!UnlockedTurret.Contains(tourelle))
        {
            UnlockedTurret.Add(tourelle);
        }
    }
}
