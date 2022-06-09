using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPos : MonoBehaviour
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

        Canvas.transform.position = this.transform.position + new Vector3(0f, 3f, 5f);
    }
}
