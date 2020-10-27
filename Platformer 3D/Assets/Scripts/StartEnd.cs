using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEnd : MonoBehaviour
{
    public GameObject[] endMessage;
    public Animation anim;
    public Animation fade;

    private void Start()
    {
        endMessage[0].SetActive(false);
        endMessage[1].SetActive(false);
        endMessage[2].SetActive(false);
    }
    IEnumerator message()
    {
        //first message
        endMessage[0].SetActive(true);
        yield return new WaitForSeconds(2);
        endMessage[0].SetActive(false);
        //start cinematic
        anim.Play();
        yield return new WaitForSeconds(18);
        //score
        endMessage[1].SetActive(true);
        yield return new WaitForSeconds(6);
        endMessage[1].SetActive(false);
        //Fade
        yield return new WaitForSeconds(3);
        fade.Play();
        //thanks
        yield return new WaitForSeconds(5);
        endMessage[2].SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
        private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(message());
        

    }
}
