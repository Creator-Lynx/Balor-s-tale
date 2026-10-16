using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class RestrictedCameraController : MonoBehaviour
{

    InputAction lookAction;
    void Awake()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        cameraTransform = transform;
    }

    [SerializeField] Transform cameraTransform;

    
    [Header("Player Look Settings (global)")]
    [SerializeField] float lookSpeed = 0.1f;
    public static float globalLookSpeedModifier = 1f;
    public static bool enableMouseSmoothing = true;
    [SerializeField] float lookSmoothing = 0.005f;
    public static float globalLookSmoothingModifier = 1f;

 



    void Update()
    {
        Looking();
    }
    



    float cameraCurrentRotationX = 0f;
    float cameraCurrentRotationY = 0f;
    Vector2 lookVector2Input;
    Vector2 smoothVelocity4Look;
    Vector2 currentLookVector2;
    void Looking()
    {
        //getting input
        lookVector2Input = lookAction.ReadValue<Vector2>();

        currentLookVector2 =
                Vector2.SmoothDamp(
                    currentLookVector2, lookVector2Input, ref smoothVelocity4Look, lookSmoothing);


        currentLookVector2 *= Time.timeScale;

        //player Y rotating
        float deltaRotationPlayerY = currentLookVector2.x * lookSpeed * globalLookSpeedModifier;
        //transform.Rotate(deltaRotationPlayerY * Vector3.up);
        cameraCurrentRotationY -= deltaRotationPlayerY;
        cameraCurrentRotationY = Mathf.Clamp(cameraCurrentRotationY, -90f, 90f);

        //camera X rotating. get the delta
        float deltaRotationCameraX = currentLookVector2.y * lookSpeed * globalLookSpeedModifier;
        //camera vertical clamping
        cameraCurrentRotationX -= deltaRotationCameraX;
        cameraCurrentRotationX = Mathf.Clamp(cameraCurrentRotationX, -90f, 90f);

        
        //apply rotation
        cameraTransform.localRotation = Quaternion.Euler(cameraCurrentRotationX, cameraCurrentRotationY, 0f);
    }


   
}
