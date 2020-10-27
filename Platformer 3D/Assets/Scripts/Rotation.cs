using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        // Rotate the game object 
        transform.Rotate(new Vector3(20, 90, 0) * Time.deltaTime);
    }
}
