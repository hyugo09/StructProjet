using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataTransfert : MonoBehaviour
{
    public List<Turret> UnlockedTurret;
    public int bonusHealth;
    public int bonusAttack;
    public float bonusCooldown;
    public bool lastGameWin;
    public List<GameObject> niveauButton;
    public bool[] niveauUnlock = new bool[5];
    public bool[] nodeObtained = new bool[9];
    public int NombrePCTotal;
    public int NombrePCDispo;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += delegate { resetButton(); };
    }
    public void resetButton()
    {
        niveauButton.Clear();
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
    public void verifierNiveau()
    {
        for (int i = 0; i < niveauUnlock.Length; i++)
        {
            niveauButton.ElementAt(i).SetActive(niveauUnlock[i]);
        }
    }
    public void UnlockerNiveau()
    {       
        for(int i = 0; i < niveauUnlock.Length; i++)
        {
            if(niveauUnlock[i] == false)
            {
                niveauUnlock[i] = true;
                return;
            }
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
