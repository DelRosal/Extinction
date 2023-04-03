using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_Button : MonoBehaviour
{
    public void ListenButton()
    {
        print("Button pressed");
        SceneManager.LoadScene("Map");

    }
}
