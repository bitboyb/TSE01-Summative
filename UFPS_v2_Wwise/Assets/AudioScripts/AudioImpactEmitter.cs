using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioImpactEmitter : MonoBehaviour
{
    public void PlayWwiseEvent(AK.Wwise.Event wwiseEvent)
    {
        wwiseEvent.Post(gameObject);
    }
}
