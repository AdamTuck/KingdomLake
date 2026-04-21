using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerInput input;
    CharacterController characterController;

    private float playerVerticalLock;

    [Header("Player Properties")]
    [SerializeField] float playerWalkSpeed;
    [SerializeField] public float playerAnimateMoveSpeed;
    [SerializeField] private float gravity = -9.81f;

    [Header("Player Turn")]
    [SerializeField] private float turnSpeed;
    [SerializeField] private bool invertMouse;

    [Header("Positions")]
    //[HideInInspector] public Transform freeRoamStart;
    public Transform freeRoamSpawn;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckDistance;
    private bool isGrounded;

    [Header("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform pickupAttachPoint;
    //[SerializeField] private Transform pickupAttachUpperPos;
    //[SerializeField] private Transform pickupAttachLowerPos;

    private PlayerState currentState;

    public GameObject currentlyHeldObj;

    // Singleton
    public static PlayerController instance;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(instance);
            return;
        }

        instance = this;
    }

    void Start()
    {
        input = PlayerInput.GetInstance();
        characterController = GetComponent<CharacterController>();

        //Default state (Menu)
        currentState = new PlayerMenuState(this);
        currentState.OnStateEnter();

        //freeRoamStart.position = freeRoamSpawn.position;
        //freeRoamSpawn.rotation = freeRoamSpawn.rotation;
    }

    void Update()
    {
        currentState.OnStateUpdate();
    }

    public void ChangeState(PlayerState state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    public void TiltCamDown()
    {
        mainCamera.transform.localRotation = Quaternion.Euler(15, 0, 0);
    }

    public void UntiltCamera()
    {
        mainCamera.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public PlayerInput GetInput()
    {
        return input;
    }

    public float GetTurnSpeed()
    {
        return turnSpeed;
    }

    public bool GetInvertMouse()
    {
        return invertMouse;
    }

    public Camera GetMainCamera()
    {
        return mainCamera;
    }

    public float GetWalkSpeed()
    {
        return playerWalkSpeed;
    }

    public float GetGravity()
    {
        return gravity;
    }

    /*public void MoveUpAttachPoint()
    {
        pickupAttachPoint.position = pickupAttachUpperPos.position;
    }

    public void MoveDownAttachPoint()
    {
        pickupAttachPoint.position = pickupAttachLowerPos.position;
    }*/

    public CharacterController GetCharacterController()
    {
        return characterController;
    }

    public string CurrentStateString()
    {
        return currentState.ToString();
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    public Transform GetGroundCheckTransform()
    {
        return groundCheckTransform;
    }

    public LayerMask GetGroundLayerMask()
    {
        return groundLayerMask;
    }

    public float GetGroundCheckDistance()
    {
        return groundCheckDistance;
    }

    public void SetGrounded(bool groundedState)
    {
        isGrounded = groundedState;
    }
}