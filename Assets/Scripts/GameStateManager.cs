using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStates
{
    MainMenu,
    Play
}
public class GameStateManager : MonoBehaviour
{
    public delegate void OnChangeStateDelegate(GameStates state);
    public event OnChangeStateDelegate OnChangeState;
    public GameStates State { get; private set; }
    // Start is called before the first frame update
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
