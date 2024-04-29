using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
enum State
{
    Obtained,
    Accessible,
    Unaccessible
}
public class SkillNode : MonoBehaviour
{
    [SerializeField] int ID;
    [SerializeField] SkillNode parentNode;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Turret bonus;
    [SerializeField] internal string description;
    internal Sprite sprite;
    internal Color couleur;
    LineRenderer lineRenderer;
    internal State currentState = State.Unaccessible;
    List<SkillNode> children = new List<SkillNode>();
    public CompetenceUI ui;
    public UnityEvent Obtained;

    private void Awake()
    {

        sprite = bonus.Sprite;
        couleur = bonus.Color;
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<SpriteRenderer>().color = couleur;
        if (parentNode != null)
        {
            parentNode.children.Add(this);
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, parentNode.transform.position);
            SetState(State.Unaccessible);
        }
        else
        {

            // On est à la racine
            SetState(State.Accessible);
        }

        if (FindAnyObjectByType<DataTransfert>().nodeObtained[ID])
        {
            SetState(State.Obtained);
        }
    }

    internal void SetState(State nodeState)
    {
        currentState = nodeState;
        switch (currentState)
        {
            case State.Obtained:
                spriteRenderer.color = Color.green;
                foreach (var child in children)
                    child.SetState(State.Accessible);
                break;
            case State.Accessible:
                spriteRenderer.color = new Color(1, 0.75f, 0);
                foreach (var child in children)
                    child.SetState(State.Unaccessible);
                break;
            case State.Unaccessible:
                spriteRenderer.color = Color.red;
                foreach (var child in children)
                    child.SetState(State.Unaccessible);
                break;
        }
    }
    public void AttackBonus()
    {
        FindAnyObjectByType<DataTransfert>().bonusAttack++;
    }
    public void CooldownBonus()
    {
        FindAnyObjectByType<DataTransfert>().bonusCooldown += 10;
    }
    public void VieBonus()
    {
        FindAnyObjectByType<DataTransfert>().bonusHealth++;
    }
    public void Activate()
    {
        if (currentState == State.Accessible)
        {
            SetState(State.Obtained);
            Obtained.Invoke();
        }
    }
    public void GiveTurretToDataTransfert(Turret unlocked)
    {
        FindAnyObjectByType<DataTransfert>().UnlockerTourelle(unlocked);
    }
    public void DataTransfertIDActivate()
    {
        FindAnyObjectByType<DataTransfert>().nodeActivationf(ID);
    }
    private void OnMouseDown()
    {
       ui.Changer(this);
    }
}
