using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject levelChoicePanel;
    public void OnPlayButtonClick()
    {
        menuPanel.SetActive(false);
        levelChoicePanel.SetActive(true);
    }
    public void OnBackButtonClick()
    {
        menuPanel.SetActive(true);
        levelChoicePanel.SetActive(false);
    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
}
