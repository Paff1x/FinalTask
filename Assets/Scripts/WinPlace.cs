using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlace : MonoBehaviour
{
    public static WinPlace Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
}
