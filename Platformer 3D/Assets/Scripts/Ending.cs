using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Ending : MonoBehaviour
{
    public Camera MainCam;
    public Camera EndCam;
    public Light mainlight;
    public TextMeshProUGUI countTime;
    private bool end;

    private void Start()
    {
        MainCam.enabled = true;
        EndCam.enabled = false;
        end = true;
        SetCountTime();
    }

    private void SetCountTime()
    {
        countTime.text = "Time : " + Time.time.ToString("f0") + " sec";
    }

    private void Update()
    {
        if (end)
        {
            SetCountTime();
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.transform.parent = transform;
            EndCam.enabled = true;
            MainCam.enabled = false;
            other.gameObject.SetActive(false);
            end = false;
        }
        
    }
}
