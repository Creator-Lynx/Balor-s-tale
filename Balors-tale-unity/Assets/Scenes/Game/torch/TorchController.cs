using UnityEngine;
using UnityEngine.InputSystem;

public class TorchController : MonoBehaviour
{
    InputAction fireAction;

    [SerializeField] AudioClip matchClip;
    [SerializeField] AudioSource matchAudioSource;
    [SerializeField] AudioSource burnupAudiosource;

    Animator torchAnimator;

    void Awake()
    {
        fireAction = InputSystem.actions.FindAction("Interact");
    }
    int countToFire = 3;
    void Update()
    {
        
    }

    void Matching()
    {
        matchAudioSource.PlayOneShot(matchClip);
    }

    void BurnUp()
    {
        torchAnimator.SetBool("IsLight", true);
        burnupAudiosource.Play();
    }
}
