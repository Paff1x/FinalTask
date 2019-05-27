using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject winPanel;

    private void Start()
    {
        GameManager.Instance.GameWinAction += EndGame;
    }
    public void OnPauseButtonClick()
    {
        Time.timeScale = 0f;
        menuPanel.SetActive(true);
    }

    public void OnToMenuButtonClick()
    {
        SceneManager.LoadScene("Menu");
    }
    public void OnPlayButtonClick()
    {
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
    }
    public void OnRestartButtonClick()
    {
        GameManager.Instance.Restart();
        losePanel.SetActive(false);
    }

    private void EndGame(bool flag)
    {
        winPanel.SetActive(flag);
        losePanel.SetActive(!flag);
    }
}
