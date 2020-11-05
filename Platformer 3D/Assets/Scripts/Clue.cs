using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clue : MonoBehaviour
{
    private Renderer rend;
    [SerializeField] 
    private Material Avant;
    [SerializeField]
    private Material Apres;
    // Start is called before the first frame update
    IEnumerator Apparition()
    {
        GameObject[] blocs = GameObject.FindGameObjectsWithTag("GoodOne");
        // We can now freely iterate through our array of enemies
        foreach (GameObject bloc in blocs)
        {
            rend = bloc.GetComponent<Renderer>();
            rend.material = Apres;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            
        }   
    }
    IEnumerator Disparition()
    {
        GameObject[] blocs = GameObject.FindGameObjectsWithTag("GoodOne");
        // We can now freely iterate through our array of enemies
        foreach (GameObject bloc in blocs)
        {
            rend = bloc.GetComponent<Renderer>();
            rend.material = Avant;
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Apparition());
    }
    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(Disparition());
    }
}
