using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChoicePanel : MonoBehaviour
{
    [SerializeField] private int levelCount;
    [SerializeField] private GameObject buttonPrefab;

    private List<LevelButton> buttons = new List<LevelButton>();

    private void Awake()
    {
        InitButtons();
    }

    private void InitButtons()
    {
        int _currentLevel = PlayerPrefs.GetInt("CurrentLevelIndex");
        for (int i = 0; i < levelCount; i++)
        {
            if (i >= buttons.Count)
            {
                GameObject go = Instantiate(buttonPrefab, transform);
                LevelButton button = go.GetComponent<LevelButton>();
                button.OnButtonClick += LoadLevel;
                buttons.Add(button);
            }
            buttons[i].SetFrame(i, (i+1).ToString(),_currentLevel >= i);
        }
    }

    private void LoadLevel(int id)
    {
        PlayerPrefs.SetInt("LoadingLevelIndex", id);
        SceneManager.LoadScene("Game");
    }
}
