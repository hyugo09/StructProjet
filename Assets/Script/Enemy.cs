using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject visual;
    public static HashSet<Enemy> allEnemies = new HashSet<Enemy>();
    private Stack<GameTile> path = new Stack<GameTile>();
    float vitesse = 2;
    float HP;
    internal EnnemyStat stat;
    internal PlayerStat PlayerStat;
    private void Awake()
    {
        allEnemies.Add(this);
        
    }

    internal void SetPath(List<GameTile> pathToGoal)
    {
        ResetSpeed();
        HP = stat.hp;

        path.Clear();
        foreach (GameTile tile in pathToGoal)
        {
            path.Push(tile);
        }
    }
    public void ResetSpeed()
    {
        vitesse = stat.baseVitesse;
    }
    public void ChangeSpeedMultiplication(float nombre)
    {
        vitesse *= nombre;
        
    }
    void Update()
    {
        if (path.Count > 0)
        {
            Vector3 destPos = path.Peek().transform.position;
            transform.position = Vector3.MoveTowards(transform.position, destPos, vitesse * Time.deltaTime);

            if (Vector3.Distance(transform.position, destPos) < 0.01f)
            {
                path.Pop();
            }
        }
        else
        {
            Die(false);
        }
    }

    private void Die(bool w)
    {
        if (w)
        {

            PlayerStat.GagnerArgent(stat.valeur);
        }
        allEnemies.Remove(this);
        Destroy(gameObject);
    }

    internal void Attack(int degat)
    {
        if (HP - degat <= 0)
        {
            Die(true);
        }
        else
        {
            HP -= degat;
            float scaleChange = 1 - (float)(degat * 0.1);
            visual.transform.localScale *= scaleChange;

        }
    }
}
