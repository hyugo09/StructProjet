using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutiqSlot : MonoBehaviour
{
    public Turret tourelle;

    public void UpdateSlot()
    {
        Image image = GetComponent<Image>();
        image.sprite = tourelle.Sprite;
        image.color = tourelle.Color;
    }
}
