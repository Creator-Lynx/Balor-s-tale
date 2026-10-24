using UnityEngine;

public class ViscousCameraController : MonoBehaviour
{

    [Header("Ограничения камеры по углам")]
    [SerializeField]float comfortYaw = 30f;
    [SerializeField]float maxYaw = 45f;

    [SerializeField]float comfortPitch = 30f;
    [SerializeField]float maxPitch = 45f;


    float currentYaw = 0f;
    float currentPitch = 0f;
    void Update()
    {
        ApplyViscousResistance();
    }

   
    void HandleViscousLook()
    {
        
    }
    
    float ApplyViscousResistance()
    {
        return 0;
    }
}
