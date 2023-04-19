using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_Button : MonoBehaviour
{


    public void ListenButton()
    {
        print("Button pressed");
        //There are two ways of changing scenes
        //When changing scenes if we want the scene to not be destroyed, we have to use the DontDestroyOnLoad method on the start method
        //Else, the object will be destroyed
        //This can be used for keeping the same lives, score, etc in other scenes, else we will have 0
        SceneManager.LoadScene("Map");
        

    }

    public void ListenButtonQuit()
    {

        SceneManager.LoadScene("Start-Play");
    }

    public void ListenButtonResume()
    {
        SceneManager.LoadScene("Map");
    }

    public void ListenButtonPause()
    {

        SceneManager.LoadScene("Pause Scene");
    }

   
}
