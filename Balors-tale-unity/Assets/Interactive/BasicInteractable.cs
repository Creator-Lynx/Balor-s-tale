using UnityEngine;
using UnityEngine.Events;

public class BasicInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent OnInteract;
    public void Interact()
    {
        OnInteract.Invoke();
    }
}
