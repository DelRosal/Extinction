using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI_Button : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;

    // we add the audio source component to the button

    [SerializeField] private AudioSource buttonSound;



    public void ListenButtonQuit()
    {

        StartCoroutine(Quit());
        //We play the sound when the button is pressed
        buttonSound.Play();   
        
    }

    IEnumerator Quit()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("Start-Play");
    }

   

    //We activate the pause menu with true and deactivate it with false

    public void Pause()
    {
        //We create a condition to check if the game is paused or not
        //If the game is paused, we set the time scale to 0, so the game will stop
        //Else, we set the time scale to 1, so the game will continue
        if (Time.timeScale == 1f)
        {
            pauseButton.SetActive(false);
            pauseMenu.SetActive(true);

            // We play the sound when the button is pressed
            buttonSound.Play();
            
        }
        else
        {
            Time.timeScale = 1f;
            pauseButton.SetActive(true);
            pauseMenu.SetActive(false);
            // We play the sound when the button is pressed
            buttonSound.Play();
            
        }
        
    }




   public void Resume()
   {    
        //We create a condition to resume all the animations from all the game scenes when the resume button is pressed
        //We get all the related animator components and set the speed to 1
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false); 
        buttonSound.Play(); 

   }

   public void PauseLevel()

   {
        //We create a condition to pause all the animations from all the game scenes when the pause button is pressed
        //We get all the related animator components and set the speed to 0
        //We set the time scale to 0, so the game will stop
        //Else, we set the time scale to 1, so the game will continue
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            pauseButton.SetActive(false);
            pauseMenu.SetActive(true);
            // We play the sound when the button is pressed
            buttonSound.Play();
            
        }
        else
        {
            Time.timeScale = 1f;
            pauseButton.SetActive(true);
            pauseMenu.SetActive(false);
            // We play the sound when the button is pressed
            buttonSound.Play();
            
        }
        


   }
}
