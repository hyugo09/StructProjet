using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
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
    internal void setSprite()
    {
        visual.GetComponent<SpriteRenderer>().sprite = stat.Sprite;
        visual.GetComponent<SpriteRenderer>().color = stat.Color;
    }
    internal void SetPath(List<GameTile> pathToGoal)
    {
        ResetSpeed();
        HP = stat.hp;

        path.Clear();
        if(stat.Fly == false)
        {
            foreach (GameTile tile in pathToGoal)
            {
                path.Push(tile);
            }
        }
        else
        {
            path.Push(pathToGoal.First());
            path.Push(pathToGoal.Last());
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
            float scaleChange = 1f - ((float)degat / (float)stat.hp);            
            visual.transform.localScale *= scaleChange;

        }
    }
}
