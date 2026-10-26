using UnityEngine;

public class ChainRestrictedPlayerController : MonoBehaviour
{
    [SerializeField] GameObject mainPlayer;
    [SerializeField] GameObject restrictedCamera;


    void Awake()
    {
        mainPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
