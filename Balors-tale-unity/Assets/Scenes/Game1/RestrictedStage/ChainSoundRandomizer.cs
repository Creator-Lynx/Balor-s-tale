using UnityEngine;

public class ChainSoundRandomizer : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] float pitchRandomRange = 0.15f;

    public void Play()
    {
        audioSource.pitch = 1 + Random.Range(-pitchRandomRange, pitchRandomRange);
        audioSource.PlayOneShot( audioClips[Random.Range(0, audioClips.Length) ]);
    }


}
