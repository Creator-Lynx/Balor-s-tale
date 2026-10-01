using UnityEngine;

public class TorchLightDancer : MonoBehaviour
{
    [Header("Смещение (Пляска теней)")]
    public float positionOffsetRange = 0.15f; // Насколько далеко свет может отходить от центра
    public float positionSpeed = 4f;         // Скорость танца пламени

    [Header("Микро-пульсация яркости (Мягкая)")]
    public float baseIntensity = 4.81f;
    public float intensityVariance = 0.1f;    // Совсем небольшой разброс, чтобы не слепило глаза
    public float intensitySpeed = 8f;

    private Vector3 startLocalPosition;
    private Light torchLight;

    void Start()
    {
        torchLight = GetComponent<Light>();
        startLocalPosition = transform.localPosition;
    }

    void Update()
    {
        // 1. Заставляем свет плавно "гулять" по осям X и Z
        float xOffset = (Mathf.PerlinNoise(Time.time * positionSpeed, 0f) - 0.5f) * 2f;
        float zOffset = (Mathf.PerlinNoise(0f, Time.time * positionSpeed) - 0.5f) * 2f;
        
        Vector3 targetOffset = new Vector3(xOffset, 0f, zOffset) * positionOffsetRange;
        transform.localPosition = startLocalPosition + targetOffset;

        // 2. Деликатно меняем яркость (почти незаметно для глаза)
        float noiseIntensity = Mathf.PerlinNoise(Time.time * intensitySpeed, 10f);
        torchLight.intensity = baseIntensity + (noiseIntensity - 0.5f) * intensityVariance;
    }
}