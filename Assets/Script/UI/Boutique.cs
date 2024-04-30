using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boutique : MonoBehaviour
{
    public BoutiqSlot[] boutiqSlots;
    //public GameObject prefab;
    private void Awake()
    {
        var data = FindAnyObjectByType<DataTransfert>();

        int i = 0;
        foreach(var tourelle in data.UnlockedTurret)
        {
            boutiqSlots[i].gameObject.SetActive(true);
            boutiqSlots[i].tourelle = tourelle;
            boutiqSlots[i].UpdateSlot();
            i++;
        }
    }
}
