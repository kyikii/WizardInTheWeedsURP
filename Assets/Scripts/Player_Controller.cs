using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] Transform playerCam, PlayerGround;
    CharacterController controller; 
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 3.5f;
    [SerializeField] float gravity = -13.0f;
    float velocityY, GroundDist = 0.4f;
    [SerializeField] bool lockCursor = true;
    [SerializeField][Range(0.0f,0.5f)] float SmoothMove = 0.3f;
    [SerializeField][Range(0.0f,0.5f)] float SmoothMouse = 0.03f;
    [SerializeField]float JumpHeight = 2f;
    public Vector2 currentDirVelocity;
    private Vector2 currentMouseDelta, currentMouseDeltaVelocity, currentDir;
    public bool CamLock;
    bool Grounded;

    public LayerMask GroundMask;

    float cameraPitch = 0f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if(lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!CamLock)
        {
            UpdateMouseMovement();
            CheckJump();
            UpdateKeyboardMovement();
        } 
    }

    void UpdateMouseMovement()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, SmoothMouse);

        if(controller.isGrounded)
        {
            velocityY = 0.0f;
        }
        velocityY += gravity * Time.deltaTime;

        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        playerCam.localEulerAngles = Vector3.right * cameraPitch; 

        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }
    void UpdateKeyboardMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, SmoothMove);

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x)* walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }

    
    void CheckJump()
    {
        Grounded = Physics.CheckSphere(PlayerGround.position,GroundDist,GroundMask);

        if(Grounded && velocityY <0)
        {
            velocityY = -2f;
        }

        if(Input.GetButtonDown("Jump")&& Grounded)
        {
            velocityY +=Mathf.Sqrt(JumpHeight * -2f * gravity);
            Debug.Log("Jumped");
        }
    }
    public void LockCam()
    {
        CamLock = true;
        
        Cursor.lockState = CursorLockMode.None;
    }

    public void unlockCam()
    {
        CamLock  = false;
        currentDirVelocity = new Vector2(0,0);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
