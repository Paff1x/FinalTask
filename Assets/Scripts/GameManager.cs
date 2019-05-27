using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Canvas canvas;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject targetCircle;
    [SerializeField] private GameObject soundObjectPrefab;
    [SerializeField] private float minTimeScale;
    [Space]
    [Header("Pool:")]
    [SerializeField] private LevelConfig[] levels;
    [SerializeField] private PoolManager pools;
    [SerializeField] private PoolPrespawnData[] prespawns;
    [System.Serializable]
    public class PoolPrespawnData
    {
        public GameObject Prefab;
        public int Count;
    }

    public Action<bool> GameWinAction;
    public Action RoundEndAction;

    public static GameManager Instance { get; private set; }
    public Canvas MainCanvas { get { return canvas; } }
    public HealthBar HealthBar { get { return healthBar; } }
    public GameObject TargetCircle { get { return targetCircle; } }
    public PoolManager Pools { get { return pools; }}

    internal List<GameObject> Enemies = new List<GameObject>();

    private int levelIndex;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < prespawns.Length; ++i)
        {
            var prespawn = prespawns[i];
            Pools.Prespawn(prespawn.Prefab, prespawn.Count);
        }
    }
    private void Update()
    {
        Time.timeScale = TimeScale();
        if (Enemies.Count == 0)
        {
            RoundEndAction();
            if (WinPlace.Instance)
                if (Vector3.Distance(WinPlace.Instance.transform.position, Player.Instance.transform.position) < 3f)
                {
                    LevelComplete();
                }
        }
    }

    public SoundObject CreateSoundObject()
    {
        var go = Pools.Spawn(soundObjectPrefab,transform.position,Quaternion.identity);
        return go.GetComponent<SoundObject>();
    }

    private void OnPlayerDead(Damagable damagable)
    {
        GameWinAction(false);
    }

    private void Start()
    {
        levelIndex = PlayerPrefs.GetInt("LoadingLevelIndex");
        InitLevel(levelIndex);
        Player.Instance.GetComponent<Damagable>().DieEvent += OnPlayerDead;
    }

    private void LevelComplete()
    {
        levelIndex++;
        if (levelIndex < levels.Length)
        {
            PlayerPrefs.SetInt("CurrentLevelIndex", levelIndex);
            ClearLevel();
            InitLevel(levelIndex);
        }
        else
            GameWinAction(true);
    }
    private void InitLevel(int index)
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(levels[index].NavMeshData);
        foreach (var obj in levels[index].Objects)
        {
            GameObject go = Pools.Spawn(obj.Prefab, obj.Position, obj.Rotation/*, transform*/);
            go.transform.localScale = obj.Scale;
        }
    }
    private void ClearLevel()
    {
        for (int i = 0; i < Pools.transform.childCount; i++)
        {
            Pools.transform.GetChild(i).gameObject.DestroyOrMoveToPool();
        }
    }

    public void Restart()
    {
        ClearLevel();
        InitLevel(levelIndex);
    }


    private float TimeScale()
    {
        float time = Mathf.Abs(Joystick.Horizontal) + Mathf.Abs(Joystick.Vertical);
        if (time > minTimeScale)
            return time;
        else
            return minTimeScale;
    }
}

