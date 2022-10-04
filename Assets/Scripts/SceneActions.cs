using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneActions : MonoBehaviour
{
    private string gameScene = "Scene 1";

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }
}
