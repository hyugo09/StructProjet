using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Turret : ScriptableObject 
{
    [Range(0f, 2f)]
    public float shotCooldown = 0.2f;
    [Range(1, 10)]
    public int attack = 2;
    [Range(1f, 10f)]
    public float range = 2;
    public Sprite Sprite;
    public Color Color;
    public bool wind = false;
    public bool electrique = false;
}
