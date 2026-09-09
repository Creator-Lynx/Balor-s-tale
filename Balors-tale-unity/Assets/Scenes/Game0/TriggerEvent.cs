using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    [SerializeField] UnityEvent OnTrigger;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            OnTrigger.Invoke();
        }
    }
}
