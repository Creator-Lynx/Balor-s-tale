using UnityEngine;
using UnityEngine.Playables;

public class TimelineWarmUp : MonoBehaviour
{
    void Awake() 
    {
        GetComponent<PlayableDirector>().RebuildGraph();
    }
}
