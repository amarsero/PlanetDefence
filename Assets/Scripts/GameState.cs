using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        OnChangeState?.Invoke(GameStates.Play);
    }
}
