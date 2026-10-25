using UnityEngine;
using UnityEngine.InputSystem;


public class RestrictedCameraController : MonoBehaviour
{

    InputAction lookAction;
    void Awake()
    {
        lookAction = InputSystem.actions.FindAction("Look");
        cameraTransform = transform;
    }

    [SerializeField] Transform cameraTransform;

    
    [Header("Player Look Settings")]
    [SerializeField] float lookSpeed = 0.1f;
    public static bool enableMouseSmoothing = true;
    [SerializeField] float lookSmoothing = 0.005f;
    [SerializeField] float restrictionXAngle = 90f;
    [SerializeField] float restrictionYAngle = 90f;



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
        float deltaRotationPlayerY = currentLookVector2.x * lookSpeed * PlayerController.globalLookSpeedModifier;
        //transform.Rotate(deltaRotationPlayerY * Vector3.up);
        cameraCurrentRotationY += deltaRotationPlayerY;
        cameraCurrentRotationY = Mathf.Clamp(cameraCurrentRotationY, -restrictionYAngle, restrictionYAngle);

        //camera X rotating. get the delta
        float deltaRotationCameraX = currentLookVector2.y * lookSpeed * PlayerController.globalLookSpeedModifier;
        //camera vertical clamping
        cameraCurrentRotationX -= deltaRotationCameraX;
        cameraCurrentRotationX = Mathf.Clamp(cameraCurrentRotationX, -restrictionXAngle, restrictionXAngle);

        
        //apply rotation
        cameraTransform.localRotation = Quaternion.Euler(cameraCurrentRotationX, cameraCurrentRotationY, 0f);
    }


   
}
