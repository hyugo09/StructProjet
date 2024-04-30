using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class GameTile : MonoBehaviour, IPointerEnterHandler,
    IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] SpriteRenderer hoverRenderer;
    [SerializeField] SpriteRenderer turretRenderer;
    [SerializeField] SpriteRenderer spawnRenderer;
    private LineRenderer lineRenderer;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private bool canAttack = true;
    private Turret Turret;
    public Turret defaultTurret;
    public GameManager GM { get; internal set; }
    public int X { get; internal set; }
    public int Y { get; internal set; }
    public bool IsBlocked { get; private set; }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.SetPosition(0, transform.position);
        spriteRenderer = GetComponent<SpriteRenderer>();
        turretRenderer.enabled = false;
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {

        if (turretRenderer.enabled && canAttack)
        {
            if (!Turret.wind)
            {
                Enemy target = null;
                foreach (var enemy in Enemy.allEnemies)
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Turret.range)
                    {
                        if (Turret.electrique)
                        {
                            StartCoroutine(AttackCoroutine(enemy));
                        }
                        else
                        {
                            target = enemy;
                            break;
                        }

                    }
                }

                if (target != null && !Turret.electrique)
                {
                    StartCoroutine(AttackCoroutine(target));
                }
            }
            else
            {
                foreach (var enemy in Enemy.allEnemies)
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Turret.range)
                    {
                        enemy.ChangeSpeedMultiplication(0.5f);
                        break;
                    }
                    else
                    {
                        enemy.ResetSpeed();
                    }
                }
            }
        }

    }

    IEnumerator AttackCoroutine(Enemy target)
    {
        float temp = Turret.attack * FindAnyObjectByType<DataTransfert>().bonusAttack;
        target.Attack((int)temp);
        canAttack = false;
        lineRenderer.SetPosition(1, target.transform.position);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lineRenderer.enabled = false;
        yield return new WaitForSeconds(Turret.shotCooldown * FindAnyObjectByType<DataTransfert>().bonusCooldown);
        canAttack = true;
    }

    internal void TurnGrey()
    {
        spriteRenderer.color = Color.gray;
        originalColor = spriteRenderer.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverRenderer.enabled = true;
        GM.TargetTile = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverRenderer.enabled = false;
    }
    private void ChangeTurretVisual()
    {
        turretRenderer.sprite = Turret.Sprite;
        turretRenderer.color = Turret.Color;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (spawnRenderer.enabled == false|| spriteRenderer.color == Color.yellow)
        {
            if (GM.tourelleSelectionner != null)
            {
                Turret = GM.tourelleSelectionner;
                ChangeTurretVisual();
            }
            else
            {
                Turret = defaultTurret;
            }
            turretRenderer.enabled = !turretRenderer.enabled;
            turretRenderer.sprite = Turret.Sprite;
            turretRenderer.color = Turret.Color;
            IsBlocked = turretRenderer.enabled;
        }
    }

    internal void SetEnemySpawn()
    {
        spawnRenderer.enabled = true;
    }

    internal void SetPath(bool isPath)
    {
        spriteRenderer.color = isPath ? Color.yellow : originalColor;
    }
}
