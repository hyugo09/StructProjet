using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnnemyStat : ScriptableObject
{
    [Range(1f, 5f)]
    public float baseVitesse = 2;
    [Range(5, 50)]
    public int hp = 10;
    [Range(100, 500)]
    public int valeur = 100;
    public bool Fly;
    public Sprite Sprite;
    public Color Color;
}
