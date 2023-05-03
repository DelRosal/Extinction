using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalEgg : MonoBehaviour
{
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject transitionImage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision");
        if (!other.CompareTag("Player") || !transitionImage) return;
        anim.SetBool("Broke", true);
    }

    public void StartTrasition()
    {
        transitionImage.SetActive(true);
        transitionImage.GetComponent<Animator>().SetBool("Play", true);
    }
}
