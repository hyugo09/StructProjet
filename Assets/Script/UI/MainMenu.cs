using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] buttons;
    private void Awake()
    {
        var data = FindAnyObjectByType<DataTransfert>();
        foreach (var button in buttons)
        {
            data.niveauButton.Add(button);
        }
    }
}
