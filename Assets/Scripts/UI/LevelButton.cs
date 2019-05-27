using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    [SerializeField] private Text text;
    [SerializeField] private Button button;

    private int Id;

    public delegate void ButtonClick(int id);
    public event ButtonClick OnButtonClick;

    private void Start()
    {
        button.onClick.AddListener(onBtnClick);
    }

    private void onBtnClick()
    {
        OnButtonClick?.Invoke(Id);
    }

    public void SetFrame(int id,string text, bool isAvailable)
    {
        Id = id;
        this.text.text = text;
        button.interactable = isAvailable;
    }
}
