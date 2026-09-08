using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    CharacterController characterController;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction lookAction;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        lookAction = InputSystem.actions.FindAction("Look");
    }

    [SerializeField] Transform cameraTransform;

    
    [Header("Player Look Settings (global)")]
    [SerializeField] float lookSpeed = 0.1f;
    public static float globalLookSpeedModifier = 1f;
    public static bool enableMouseSmoothing = true;
    [SerializeField] float lookSmoothing = 0.005f;
    public static float globalLookSmoothingModifier = 1f;

    [Header("Player Movement Settings (GD only)")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float movementSmooth = 0.05f;
    [Header("In air movement")]
    [SerializeField] float airControl = 0.2f;
    [SerializeField] float gravityValue = -30f;
    [Header("Jump settings")]
    [SerializeField] float jumpHeight = 1.2f;
    [SerializeField] float jumpDelay = 0.2f;


    void Update()
    {
        if (jumpAction.WasPressedThisFrame()) StartCoroutine(JumpInputCorutine());
        Looking();
        Moving();
    }
    
    bool isWanna2Jump = false;
    IEnumerator JumpInputCorutine()
    {
        isWanna2Jump = true;
        yield return new WaitForSeconds(jumpDelay);
        isWanna2Jump = false;
    }



    float cameraCurrentRotationX = 0f;
    Vector2 lookVector2Input;
    Vector2 smoothVelocity4Look;
    Vector2 currentLookVector2;
    void Looking()
    {
        //getting input
        lookVector2Input = lookAction.ReadValue<Vector2>();

        //smoothing input vector if needed
        if (enableMouseSmoothing)
            currentLookVector2 =
                Vector2.SmoothDamp(
                    currentLookVector2, lookVector2Input, ref smoothVelocity4Look, lookSmoothing);
        else
            currentLookVector2 = lookVector2Input;

        currentLookVector2 *= Time.timeScale;

        //player Y rotating
        float deltaRotationPlayerY = currentLookVector2.x * lookSpeed * globalLookSpeedModifier;
        transform.Rotate(deltaRotationPlayerY * Vector3.up);

        //camera X rotating. get the delta
        float deltaRotationCameraX = currentLookVector2.y * lookSpeed * globalLookSpeedModifier;
        //camera vertical clamping
        cameraCurrentRotationX -= deltaRotationCameraX;
        cameraCurrentRotationX = Mathf.Clamp(cameraCurrentRotationX, -90f, 90f);
        //apply rotation
        cameraTransform.localRotation = Quaternion.Euler(cameraCurrentRotationX, 0f, 0f);
    }



    float _yAxisVelocity;
    Vector2 moveVector2Input;
    Vector2 originMovement4Jump = Vector2.up;
    Vector2 smoothVelocity4Movement;

  

    void Moving()
    {
        if (characterController.isGrounded)
        {
            _yAxisVelocity = -1f;
            //moveVector2Input = moveAction.ReadValue<Vector2>();
            moveVector2Input = Vector2.SmoothDamp(
                moveVector2Input, moveAction.ReadValue<Vector2>(),
                ref smoothVelocity4Movement, movementSmooth);
            originMovement4Jump = moveVector2Input;


            //landing sound
            //if(inAirTime > inAirTimeThreshold) _sounds.SetLanded();
        }
        else
        {
            moveVector2Input = originMovement4Jump;
            if (moveAction.IsInProgress())
                moveVector2Input = Vector2.Lerp(
                    originMovement4Jump, moveAction.ReadValue<Vector2>(), airControl);


            //landing sound
            //inAirTime += Time.deltaTime;
        }

        //horizontal moving calculated
        Vector3 movementVector =
            transform.forward * moveVector2Input.y * movementSpeed * Time.deltaTime +
            transform.right * moveVector2Input.x * movementSpeed * Time.deltaTime;

        

        if (isWanna2Jump && characterController.isGrounded)
        {
            _yAxisVelocity = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            originMovement4Jump = moveVector2Input;
            isWanna2Jump = false;

        }

        if (!characterController.isGrounded)
            _yAxisVelocity += gravityValue * Time.deltaTime;
        movementVector.y = _yAxisVelocity * Time.deltaTime;

        characterController.Move(movementVector);
    }
}
