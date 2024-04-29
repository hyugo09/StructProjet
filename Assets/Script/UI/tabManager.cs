using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tabManager : MonoBehaviour
{
    private GameObject currentTab;

    public void ChangeTab(GameObject newTab)
    {
        if(currentTab != null)
        currentTab.SetActive(false);
        currentTab = newTab;
        newTab.SetActive(true);
    }
}
