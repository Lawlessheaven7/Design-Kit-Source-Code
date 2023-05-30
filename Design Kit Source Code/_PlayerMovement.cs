using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class _PlayerMovement : MonoBehaviour
{
    private float currentmoveSpeed;
    [Header("Character Movement")]
    [Tooltip("Controls Character walking Speed")]
    [SerializeField] private float walkSpeed;

    [Tooltip("Controls Character running Speed")]
    [SerializeField] private float sprintSpeed;

    [Tooltip("Controls how high the character jumps")]
    [SerializeField] private float jumpForce;

    [Tooltip("Controls how intense is the gravity in the level")]
    [SerializeField] private float gravityScale = 5f;

    private float yvelocity;

    private Vector3 moveDirection;

    [SerializeField] private CharacterController charController;

    private Camera theCam;
    private CharacterController rb;

    [Header("Unity Events")]
    public UnityEvent respawn;

    [Header("Player Models & Rotation & Animator")]
    [SerializeField] private GameObject playerModel;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private Animator anim;

    private bool isKnocking;

    [Header("Trap Interaction")]
    [Tooltip("Controls how far the trap knock the player back")]
    [SerializeField] private float knockBackLength = .5f;
    
    [Tooltip("Controls the Knock back power on the X and Y axis")]
    [SerializeField] private Vector2 knockbackPower;

    private float knockbackCounter;

    private bool stopMove;

    // Start is called before the first frame update
    void Start()
    {
        theCam = Camera.main;
        rb = GetComponent<CharacterController>();
        currentmoveSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        yvelocity = rb.velocity.y;

        Movement();
        //Allow cursor to appear once escape is clicked in the build version.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;

        }
        //Allow cursor to go back to invisible if the player clikc their left mouse key.
        else if (Cursor.lockState == CursorLockMode.None)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (yvelocity < -40)
        {
            death();
        }
    }

    public void Movement()
    {
        if (!isKnocking && !stopMove)
        {
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection.Normalize();
            moveDirection = moveDirection * currentmoveSpeed;
            moveDirection.y = yStore;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentmoveSpeed = sprintSpeed;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                currentmoveSpeed = walkSpeed;
            }


            moveDirection.y = yStore;


            if (charController.isGrounded)
            {
                moveDirection.y = -1f;

                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                    //Debug.Log("Jump");
                }
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, theCam.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }




        if (isKnocking)
        {
            knockbackCounter -= Time.deltaTime;

            float yStore = moveDirection.y;
            moveDirection = playerModel.transform.forward * -knockbackPower.x;
            moveDirection.y = yStore;

            if (charController.isGrounded)
            {
                moveDirection.y = 0f;
            }

            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;

            charController.Move(moveDirection * Time.deltaTime);

            if (knockbackCounter <= 0)
            {
                isKnocking = false;
            }

            currentmoveSpeed = walkSpeed;
        }

        if (stopMove)
        {
            moveDirection = Vector3.zero;
            moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            charController.Move(moveDirection);
        }

        anim.SetFloat("Speed", Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.z));
        anim.SetBool("Grounded", charController.isGrounded);
    }

    public void Knockback()
    {
        isKnocking = true;
        knockbackCounter = knockBackLength;
        //Debug.Log("Knocked Back");
        moveDirection.y = knockbackPower.y;
        charController.Move(moveDirection * Time.deltaTime);
    }


    public void death()
    {
   
      respawn.Invoke();
      yvelocity = 0;
        
    }
}

