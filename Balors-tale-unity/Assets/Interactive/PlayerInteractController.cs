using UnityEngine;
using UnityEngine.Events;

public class PlayerInteractController : MonoBehaviour
{
    [SerializeField] float rayCastDistance;
    [SerializeField] LayerMask interactiveLayerMask;

    IInteractable interactable = null;

    void FixedUpdate()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.forward, out raycastHit, rayCastDistance, interactiveLayerMask))
        {
            if(interactable == null)
            {
                interactable = raycastHit.collider.GetComponent<IInteractable>();
                OnInteractionReady.Invoke();
            }
        }
        else
        {
            interactable = null;
            OnInteractionNotReady.Invoke();
        }
    }

    public void Interact()
    {
        if(interactable != null)
        {
            OnInteractionProceed.Invoke();
            interactable.Interact();
        }
    }




    [Space(20)]
    [Header("EVENTS")]
    [SerializeField] UnityEvent OnInteractionReady;
    [SerializeField] UnityEvent OnInteractionProceed;
    [SerializeField] UnityEvent OnInteractionNotReady;
}
