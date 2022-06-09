using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(Rigidbody))]
public class TempPlayer : MonoBehaviour {
    // props
    // public float moveSpeed = 3f;
    // public float lookSpeed = 2f;
    // public float jumpForce;
    // private float xRotate = 0.0f;
    // private bool isJumpBtnDown;
    public InventoryObject inventory;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            Debug.Log(_item.Id);
            inventory.AddItem(_item, 1);
            Destroy(other.gameObject);
        }
    }
    
    // // start
    // void Start()
    // {
    //     jumpForce = 5f;
    //     isJumpBtnDown = false;
    // }

    // // update
    // void Update()
    // {
    //     // float yRotateSize = Input.GetAxis("Mouse X") * lookSpeed;
    //     // float yRotate = transform.eulerAngles.y + yRotateSize;
    //     // float xRotateSize = Input.GetAxis("Mouse Y") * lookSpeed;
    //     // xRotate = Mathf.Clamp(xRotate - xRotateSize, -75, 75);
    //     // transform.eulerAngles = new Vector3(xRotate, yRotate, 0f);

    //     // Vector3 moveDirection = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
    //     // transform.position += moveDirection * moveSpeed * Time.deltaTime;

    //     // if(Input.GetButtonDown("Jump") && isJumpBtnDown == false)
    //     // {
    //     //     isJumpBtnDown = true;
    //     //     GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //     // } else if (!Input.GetButtonUp("Jump")){
    //     //     isJumpBtnDown = false;
    //     // }
    // }
}