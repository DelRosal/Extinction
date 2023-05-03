using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    public void EndScene()
    {
        SceneManager.LoadScene(4);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }
}