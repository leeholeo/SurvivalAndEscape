using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPosopen : MonoBehaviour
{
    public GameObject Canvas;

    // Start is called before the first frame update
    void Start()
    {
        //Canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {

        Canvas.transform.position = this.transform.position + new Vector3(0f, 0f, 5f);
    }
}
