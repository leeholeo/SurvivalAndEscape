using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class FirstPersonController : MonoBehaviour
{
    private InputDevice rightDevice;
    private InputDevice leftDevice;
    private bool curActiveStat = false;
    private bool curObjEnter = false;



    public bool CanMove = true;
    public bool IsSprinting => (canSprint && Input.GetKey(sprintKey) )  ||
        (canSprint && (rightDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool triggerButtonValue) && triggerButtonValue)  );  //(canSprint && Input.GetButton("Fire2")) ||
    public bool shouldJump => (Input.GetKeyDown(jumpKey) && characterController.isGrounded) ||
       (leftDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButtonValue) && gripButtonValue) && characterController.isGrounded ; //|| (Input.GetButton("Fire1") && characterController.isGrounded)

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool useFootSteps = true;
    
    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)]private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)]private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)]private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)]private float lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Footstep Parameters")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float sprintStepMultipler = 0.6f;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip[] grassClips = default;
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [SerializeField] private AudioClip[] concreteClips = default;
    [SerializeField] private AudioClip[] dirtClips = default;
    [SerializeField] private AudioClip[] waterClips = default;

    private float footstepTimer = 0;
    private float GetCurrentOffest => IsSprinting ? baseStepSpeed * sprintStepMultipler : baseStepSpeed;
    

    private Camera playerCamera;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector2 currentInput;
    private float rotationX = 0;
    
     void Start()
    {
        //rightdevice
        List<InputDevice> rdevices = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, rdevices);

        if (rdevices.Count > 0)
        {
            rightDevice = rdevices[0];
        }

        //leftdevice
        List<InputDevice> ldevices = new List<InputDevice>();
        InputDeviceCharacteristics leftControllerCharacteristics =
            InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, ldevices);

        if (ldevices.Count > 0)
        {
            leftDevice = ldevices[0];
        }



    }
    // Start is called before the first frame update
    void Awake()
    {
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        
        // 커서 숨기기
        // Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if(canJump){
                HandleJump();
            }

            if(useFootSteps){
                HandleFootSteps();
            }
            //vr occulus
            /*targetDevice.TryGetFeatureValue(CommonUsages.gripButton, out bool primaryButtonValue);
            if (primaryButtonValue)
            {
                moveDirection.y = jumpForce;
            }*/
            

            ApplyFinalMovements();
        }
        
    }

    private void HandleMovementInput(){
        currentInput = new Vector2((IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), walkSpeed * Input.GetAxis("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook(){
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
    }

    private void ApplyFinalMovements(){
        if(!characterController.isGrounded){
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleJump(){
        if(shouldJump){
            moveDirection.y = jumpForce;
        }
    }

    private void HandleFootSteps(){
        if(!characterController.isGrounded) return; // 공중에서는 소리나지 않음
        if(currentInput == Vector2.zero) return; // 버튼을 누르지 않으면 소리나지 않음

        // 타이머가 0에 도달하면 현재 바닥을 확인하고 소리를 출력한다
        footstepTimer -= Time.deltaTime; 

        if(footstepTimer <= 0){
            // 자기 자신의 아래의 지면의 재질을 확인하기 위해, 바닥으로 raycast한다.
            // 가끔 자기 자신의 몸통이 부딛치는 경우가 있어, layerMask를 통해 Player 레이어를 제외한 모든 레이어에 laycast한다.
            int layerMask = (-1) - (1 << LayerMask.NameToLayer("Player")); 

            if(Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 3, layerMask)){
                // 바닥면의 태그를 확인함
                // Debug.Log(hit.collider.tag);
                switch(hit.collider.tag){
                    case "Footsteps/GRASS":
                        if(grassClips.Length > 0){
                            footstepAudioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length - 1)]);
                        }
                        break;

                    case "Footsteps/WOOD":
                        if(woodClips.Length > 0){
                            footstepAudioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                        }
                        break;

                    case "Footsteps/METAL":
                        if(metalClips.Length > 0){
                            footstepAudioSource.PlayOneShot(metalClips[Random.Range(0, metalClips.Length - 1)]);
                        }
                        break;

                    case "Footsteps/CONCRETE":
                        if(concreteClips.Length > 0){
                            footstepAudioSource.PlayOneShot(concreteClips[Random.Range(0, concreteClips.Length - 1)]);
                        }
                        break;

                    case "Footsteps/DIRT":
                        if(dirtClips.Length > 0){
                            footstepAudioSource.PlayOneShot(dirtClips[Random.Range(0, dirtClips.Length - 1)]);
                        }
                        break;

                    case "Footsteps/WATER":
                        if(waterClips.Length > 0){
                            footstepAudioSource.PlayOneShot(waterClips[Random.Range(0, waterClips.Length - 1)]);
                        }
                        break;

                    default :
                        if(dirtClips.Length > 0){
                            footstepAudioSource.PlayOneShot(dirtClips[Random.Range(0, dirtClips.Length - 1)]);
                        }
                        break;
                }
            }
            footstepTimer = GetCurrentOffest;
        }
    }
}
