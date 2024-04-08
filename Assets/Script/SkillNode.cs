using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum State
{
    Obtained,
    Accessible,
    Unaccessible
}
public class SkillNode : MonoBehaviour
{
    [SerializeField] SkillNode nodeParent;
    [SerializeField] SpriteRenderer spriteRenderer;
    LineRenderer lineRenderer;
    State state;
    List<SkillNode> children = new List<SkillNode>();

    private void Awake()
    {

        lineRenderer = GetComponent<LineRenderer>();

        if (nodeParent != null)
        {
            nodeParent.children.Add(this);
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, nodeParent.transform.position);
            SetState(State.Unaccessible);
        }
        else
        {
            lineRenderer.enabled = false;
            SetState(State.Accessible);
        }
        
    }
    private void SetState(State newState)
    {
        state = newState;
        switch (state)
        {
            case State.Obtained:
                spriteRenderer.color = Color.green;
                foreach(SkillNode child in children)
                {
                    child.SetState(State.Accessible);
                }
                break;
            case State.Accessible:
                spriteRenderer.color = Color.yellow;
                break;
            case State.Unaccessible:
                spriteRenderer.color = Color.red;
                break;
        }
    }
    private void OnMouseDown()
    {
        if (state == State.Accessible)
        {
            spriteRenderer.color = Color.green;
            SetState(State.Obtained);
        }
    }
}
