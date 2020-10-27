using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    //paramaters of the script

    // the renderer used for changing materials
    private Renderer rend;
    [SerializeField]
    private Material CheckColors;
    

    // Start is called before the first frame update
    void Start()
        {
            //get the renderer we want to change
            rend = GetComponent<Renderer>();
        }
    

    void OnTriggerEnter(Collider other)
        {
            UnityEngine.Debug.Log("trigger enter");
            rend.material = CheckColors;
            
        }
        
    
}
