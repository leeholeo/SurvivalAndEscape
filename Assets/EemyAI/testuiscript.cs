using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testuiscript : MonoBehaviour
{
    public Text uitext;
    // Start is called before the first frame update
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    public void testbutton() {
        uitext.text = "button click!";
    }
}
