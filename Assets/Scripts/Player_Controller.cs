using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] Transform playerCam;
    CharacterController controller;
    [SerializeField] float mouseSensitivity = 3.5f;
    [SerializeField] float walkSpeed = 3.5f;
    [SerializeField] float gravity = -13.0f;
    float velocityY;
    [SerializeField] bool lockCursor = true;
    [SerializeField] [Range(0.0f, 0.5f)] float SmoothMove = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float SmoothMouse = 0.03f;
    Vector2 currentDir;
    public Vector2 currentDirVelocity;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;
    public bool CamLock;

    float cameraPitch = 0f;


    // Alex here! I wanted to add a quick jump to help with navigating levels during tests because currently any elevation causes a problem

    public Rigidbody rb;
    Vector3 movement;
    public float jumpForce = 1500;







    //----------------------------------------------------------------------------------------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!CamLock)
        {
            UpdateMouseMovement();
            UpdateKeyboardMovement();
        }
        Jump();

        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.velocity = movement * walkSpeed;
    }

    void UpdateMouseMovement()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, SmoothMouse);

        if (controller.isGrounded)
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

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }



    // Allexs Jumping attempt 

    void FixedUpdate()
    {
       
    }

    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jumping");
        }

    }


    //-------------------------
}
