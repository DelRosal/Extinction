using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private AudioSource buttonSound;

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.45f); 
        SceneManager.LoadScene("Level4");
    }

    public void ListenButtonResume()
    {
        StartCoroutine(Reset());
        //We play the sound when the button is pressed
        buttonSound.Play();
    }
}
