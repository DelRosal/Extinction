using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;
    

    IEnumerator Play()
    {
        yield return new WaitForSeconds(0.45f);
        SceneManager.LoadScene("Map");
    }

    public void ListenButton()
    {
        StartCoroutine(Play());
        //We play the sound when the button is pressed
        buttonSound.Play();         

    }
}
