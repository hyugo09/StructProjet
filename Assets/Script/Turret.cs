using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Turret : ScriptableObject 
{
    [Range(0f, 10f)]
    public float shotCooldown = 0.2f;
    [Range(1, 20)]
    public int attack = 2;
    [Range(1f, 10f)]
    public float range = 2;
    [Range(100, 1000)]
    public int prix;
    public Sprite Sprite;
    public Color Color;
    public bool wind = false;
    public bool electrique = false;
}
