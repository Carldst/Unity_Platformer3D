using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DeadZone : MonoBehaviour
{
    public Transform spawnpoint;

    void Start()
    {
        Assert.IsNotNull(GetComponent<Collision>());
    }
    
    void OnCollisionEnter (Collision co)
    {
        if (co.gameObject.tag == "Player")
        {
            Debug.Log("collision enter");
            co.gameObject.transform.position = spawnpoint.position;
        }
        else { Destroy(co.gameObject); }
    }
}
