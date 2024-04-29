using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompetenceUI : MonoBehaviour
{
    public Image Image;
    public TextMeshProUGUI text;
    private SkillNode currentNode;
    public Button Button;
    private void Start()
    {

    }
    public void ActiverNodeSelectionner()
    {
        //if(FindAnyObjectByType<>)
        currentNode.Activate();
        Button.enabled = false;
    }
    public void Changer(SkillNode newCurrentNode)
    {
        currentNode = newCurrentNode;
        Image.color = currentNode.couleur;
        Image.sprite = currentNode.sprite;
        text.text = currentNode.description;
        if(currentNode.currentState == State.Accessible)
        {
            Button.enabled = true;
        }
        else
        {
            Button.enabled = false;
        }
    }
    
}
