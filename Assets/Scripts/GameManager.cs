using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    MainMenu,
    Play
}
public class GameManager : MonoBehaviour
{
    public delegate void OnChangeStateDelegate(GameStates state);
    public event OnChangeStateDelegate OnChangeState;
    public GameStates State { get; private set; }
    static GameManager _instance;
    public static GameManager Instance => _instance ?? throw new System.NullReferenceException("GameManager is Missing!");
    public float AlienSpawnRate { get; set; } = 1;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void GoToPlay()
    {
        State = GameStates.Play;
        SceneManager.LoadScene("SampleScene 1");
        OnChangeState?.Invoke(GameStates.Play);
    }
}
