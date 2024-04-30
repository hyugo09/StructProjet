using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BoutiqSlot : MonoBehaviour
{
    public Turret tourelle;
    public TextMeshProUGUI textprix;
    public void UpdateSlot()
    {
        Image image = GetComponent<Image>();
        image.sprite = tourelle.Sprite;
        image.color = tourelle.Color;
        textprix.text = tourelle.prix.ToString();
    }

    public void SlotClick()
    {
        FindFirstObjectByType<GameManager>().tourelleSelectionner = tourelle;
    }
}
