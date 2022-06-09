using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysOnCam : MonoBehaviour {

    public GameObject Cam;

    void Start()
    {
        
    }

    void Update()
    {
        transform.rotation = Cam.transform.rotation;
    }
}