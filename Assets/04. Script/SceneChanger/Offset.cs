using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Offset : MonoBehaviour
{
    private new Renderer renderer;
    ///public Player player;

    public float speed = 1;
    public float offset;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ///if(player.IsDead == true){
        ///    return;
        ///}

        offset = Time.time * speed * (-1);
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, -1));
        //renderer.material.SetColor ("_Color", Color.white);
    }
}

