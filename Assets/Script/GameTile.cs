using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameTile : MonoBehaviour, IPointerEnterHandler
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    internal void TurnGray()
    {
        spriteRenderer.color = Color.gray;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}
