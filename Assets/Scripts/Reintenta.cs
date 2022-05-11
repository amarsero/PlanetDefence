using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reintenta : MonoBehaviour
{
    public void Reintentar()
    {
        SceneManager.LoadScene("Game");
    }
}
