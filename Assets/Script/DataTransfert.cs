using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataTransfert : MonoBehaviour
{
    public List<Turret> UnlockedTurret;
    public int bonusHealth;
    public bool lastGameWin;
    public bool[] nodeObtained = new bool[9];
    public int NombrePCTotal;
    public int NombrePCDispo;
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
    public void GagnerPC(int nombre)
    {
        NombrePCTotal += nombre;
        NombrePCDispo += nombre;
    }
    public bool dépenserPC(int cout)
    {
        if(NombrePCDispo - cout <= 0)
        {
            return false;
        }
        NombrePCDispo -= cout;
        return true;
    }
}
