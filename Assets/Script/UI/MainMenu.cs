using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] buttons;
    public DataTransfert data;
    private void Awake()
    {
        data = FindAnyObjectByType<DataTransfert>();
        foreach (var button in buttons)
        {
            data.niveauButton.Add(button);
        }

        data.verifierNiveau();
    }
}
