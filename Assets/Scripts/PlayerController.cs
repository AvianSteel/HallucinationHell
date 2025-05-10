/*****************************************************************************
// File Name : PlayerController.cs
// Author : Bryson Welch
// Creation Date : March 21, 2025
//
// Brief Description : Player Movement, Collisions, and Triggers
*****************************************************************************/
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    private float rotationValue;
    [SerializeField] private GameObject parasol;
    [SerializeField] private GameObject staff;

    [SerializeField]  private bool isGrounded;

    private Rigidbody rb;
    private Vector3 playerMovement;
    private Vector3 vel;

    private InputAction forward;
    private InputAction backward;
    private InputAction left;
    private InputAction right;
    private InputAction horizontalCamera;
    private InputAction pause;
    private InputAction umbrella;
    private InputAction attack;

    private bool isMovingForward;
    private bool isMovingBackward;
    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isPaused;

    private Vector3 forwardValue;
    private Vector3 rightValue;
    private Vector3 leftValue;
    private Vector3 backwardValue;

    [SerializeField] private InsanityController insanityController;

    private int insanityLevel;
    private int health;

    private Vector3 temprbv;

    [SerializeField] private GameObject pausePanel;

    static private bool HAS_UMBRELLA = false;
    static private bool HAS_STAFF = false;
    private bool isFloat;

    private float floatFall;
    public bool IsPaused { get => isPaused; set => isPaused = value; }

    [SerializeField] private LevelEndController levelEndController;
    [SerializeField] private GameObject upgradeSpotlight;
    [SerializeField] private Level1Controller level1Controller;
    [SerializeField] private Level2Controller level2Controller;

    /// <summary>
    /// Sets up player input and important variables
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerInput.currentActionMap.Enable();
        forward = playerInput.currentActionMap.FindAction("Forward");
        backward = playerInput.currentActionMap.FindAction("Backward");
        left = playerInput.currentActionMap.FindAction("Left");
        right = playerInput.currentActionMap.FindAction("Right");
        horizontalCamera = playerInput.currentActionMap.FindAction("HorizontalCamera");
        pause = playerInput.currentActionMap.FindAction("Pause");
        umbrella = playerInput.currentActionMap.FindAction("HoldUmbrella");
        attack = playerInput.currentActionMap.FindAction("Attack");
        

        forward.started += Forward_started;
        backward.started += Backward_started;
        left.started += Left_started;
        right.started += Right_started;
        horizontalCamera.started += HorizontalCamera_started;
        pause.started += Pause_started;
        umbrella.started += Umbrella_started;
        attack.started += Attack_started;

        forward.canceled += Forward_canceled;
        backward.canceled += Backward_canceled;
        left.canceled += Left_canceled;
        right.canceled += Right_canceled;
        umbrella.canceled += Umbrella_canceled;

        isMovingForward = false;
        isMovingBackward = false;
        isMovingLeft = false;
        isMovingRight = false;

        isGrounded = true;
        IsPaused = false;

        pausePanel.gameObject.SetActive(false);


        parasol.gameObject.SetActive(false);
        staff.gameObject.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;

    }




    //Looks for the level controller that sets the bools of the movement options, and stat modifiers

    #region Actions Cancel

    /// <summary>
    /// Stops movement in direction not held
    /// </summary>
    /// <param name="obj"></param>

    private void Right_canceled(InputAction.CallbackContext obj)
    {
        
        rightValue = new Vector3(0, rb.velocity.y, 0);
        isMovingRight = false;
    }

    private void Left_canceled(InputAction.CallbackContext obj)
    {
        leftValue = new Vector3(0, rb.velocity.y, 0);
        isMovingLeft = false;
    }

    private void Backward_canceled(InputAction.CallbackContext obj)
    {
        backwardValue = new Vector3(0, rb.velocity.y, 0);
        isMovingBackward = false;
    }

    private void Forward_canceled(InputAction.CallbackContext obj)
    {
        forwardValue = new Vector3(0, rb.velocity.y, 0);
        isMovingForward = false;
    }
    private void Umbrella_canceled(InputAction.CallbackContext context)
    {
        if (!isGrounded && HAS_UMBRELLA)
        {
            isFloat = false;
            parasol.gameObject.SetActive(false);
        }
    }
    #endregion

    #region Actions Start
    private void Umbrella_started(InputAction.CallbackContext obj)
    {
        if (!isGrounded && HAS_UMBRELLA)
        {
            isFloat = true;
            parasol.gameObject.SetActive(true);
        }
    }    
    private void Attack_started(InputAction.CallbackContext obj)
    {
        if (HAS_STAFF)
        {
            staff.gameObject.SetActive(true);
            StartCoroutine(AttackTimer());
        }

    }
    private void Pause_started(InputAction.CallbackContext obj)
    {
        IsPaused = !IsPaused;
        if (IsPaused)
        {
            Time.timeScale = 0;
            pausePanel.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            IsPaused = true;

        }
        else if (!IsPaused)
        {
            Time.timeScale = 1;
            pausePanel.gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            IsPaused = false;
        }
    }

    /// <summary>
    /// Rotates camera horizontally around player when mouse moves left or right
    /// </summary>
    /// <param name="obj"></param>
    private void HorizontalCamera_started(InputAction.CallbackContext obj)
    {
        if (!IsPaused)
        {
             //rotate around the player position
            rotationValue = horizontalCamera.ReadValue<float>();
            transform.RotateAround(transform.position,Vector3.up, 0.05f * rotationValue);
        }
       
    }

    /// <summary>
    /// Starts movement in directions currently pressed
    /// </summary>
    /// <param name="obj"></param>
    private void Right_started(InputAction.CallbackContext obj)
    {
        isMovingRight = true;
    }

    private void Left_started(InputAction.CallbackContext obj)
    {
        isMovingLeft = true;
    }

    private void Backward_started(InputAction.CallbackContext obj)
    {
        isMovingBackward = true;
    }

    private void Forward_started(InputAction.CallbackContext obj)
    {
        isMovingForward = true;    
    }
    #endregion

    /// <summary>
    /// Adds force to the player to act as a jump
    /// </summary>
    void OnJump()
    {
        if (isGrounded)
        {
            //rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            rb.AddForce(0, jumpHeight, 0);
            isGrounded = false;
        }
    }

    IEnumerator AttackTimer()
    {
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(1);
        }
        staff.gameObject.SetActive(false);
    }
    /// <summary>
    /// Breaks down each input into various vectors of different directions, then adds them togeter to allow
    /// movement based on the camera position, and is still 8-way
    /// </summary>
    private void FixedUpdate()
    {
        vel = rb.velocity;
        if (isMovingForward)
        {
            forwardValue = new Vector3(transform.forward.x, rb.velocity.y ,
                transform.forward.z) * moveSpeed * Time.deltaTime;
        }
        if (isMovingRight)
        {
            rightValue = new Vector3(transform.right.x, rb.velocity.y,
                transform.right.z) * moveSpeed * Time.deltaTime;
        }
        if (isMovingLeft)
        {
            leftValue = new Vector3(transform.right.x * -1, rb.velocity.y,
                transform.right.z * -1) * moveSpeed * Time.deltaTime;
        }
        if (isMovingBackward)
        {
            backwardValue = new Vector3(transform.forward.x * -1, rb.velocity.y,
                transform.forward.z * -1) * moveSpeed * Time.deltaTime;
        }

        if (isFloat)
        {
            if (vel.y < 0)
            {
                temprbv = new Vector3(0, floatFall, 0);
                rb.velocity = new Vector3(forwardValue.x + rightValue.x + backwardValue.x + leftValue.x,
                rb.velocity.y, forwardValue.z + rightValue.z + backwardValue.z + leftValue.z).normalized * 6;
                rb.velocity = new Vector3(rb.velocity.x, temprbv.y, rb.velocity.z); 
            }
            
        }
        else
        {
            temprbv = new Vector3 (0, rb.velocity.y, 0);
            rb.velocity = new Vector3(forwardValue.x + rightValue.x + backwardValue.x + leftValue.x,
            rb.velocity.y, forwardValue.z + rightValue.z + backwardValue.z + leftValue.z).normalized * 6;
            rb.velocity = new Vector3(rb.velocity.x, temprbv.y, rb.velocity.z);
        }

        if (!isPaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    #region Collision and Triggers
    /// <summary>
    /// Refreshes jump ability
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isFloat = false;
            floatFall = -0.75f;
            parasol.gameObject.SetActive(false);
        }
        
    }

    /// <summary>
    /// Makes sure the player cannot jump mid air
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        
    }

    /// <summary>
    /// Checks triggered object tag and performs functions based on the tag
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Miasma"))
        {
            insanityController.IncreaseInsanity(50);
        }
        else if (other.gameObject.CompareTag("Flowers"))
        {
            insanityController.DecreaseInsanity(50);
        }
        else if (other.gameObject.CompareTag("Upgrade"))
        {
            insanityController.IncreaseInsanity(250);
            UpgradeCheck(other);
        }else if (other.gameObject.CompareTag("LevelEnd"))
        { 
            levelEndController.NextLevel();
        }
    }
    #endregion
    /// <summary>
    /// Checks for what the upgrade is and performs an action based on what it is
    /// </summary>
    /// <param name="other"></param>
    private void UpgradeCheck(Collider other)
    {
        if(other.gameObject.name == "Umbrella")
        {
            HAS_UMBRELLA = true;
            Destroy(other.gameObject);
            Destroy(upgradeSpotlight.gameObject);
            level1Controller.UmbrellaTutorial();
            
        }else if(other.gameObject.name == "StaffUpgrade")
        {
            HAS_STAFF = true;
            Destroy(other.gameObject);
            Destroy(upgradeSpotlight.gameObject);
            level2Controller.StaffTutorial();
        }
    }
    /// <summary>
    /// Releases Temporary memory
    /// </summary>
    private void OnDestroy()
    {
        forward.started -= Forward_started;
        backward.started -= Backward_started;
        left.started -= Left_started;
        right.started += Right_started;
        horizontalCamera.started -= HorizontalCamera_started;
        pause.started -= Pause_started;
        umbrella.started -= Umbrella_started;

        forward.canceled -= Forward_canceled;
        backward.canceled -= Backward_canceled;
        left.canceled -= Left_canceled;
        right.canceled -= Right_canceled;
        umbrella.canceled -= Umbrella_canceled;
        IsPaused = false;
    }
}
