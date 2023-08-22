using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreSpecificCollisions : MonoBehaviour
{
    public string layerToIgnore = "IgnoreBullets";

    private void Start()
    {
        int layerIndex = LayerMask.NameToLayer(layerToIgnore);
        Physics.IgnoreLayerCollision(gameObject.layer, layerIndex, true);
    }
    private void OnDestroy()
    {
        int layerIndex = LayerMask.NameToLayer(layerToIgnore);
        Physics.IgnoreLayerCollision(gameObject.layer, layerIndex, false);
    }
}
