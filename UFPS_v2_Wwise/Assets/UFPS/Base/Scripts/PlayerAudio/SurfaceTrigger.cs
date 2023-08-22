using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SurfaceTrigger : MonoBehaviour
{
    private BoxCollider _boxCollider;
    public PlayerFootstepBehaviour.Footstep_Surface footstepSurface;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "HeroHDWeapons")
            other.GetComponent<PlayerFootstepBehaviour>().footstepSurface = footstepSurface;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "HeroHDWeapons")
            other.GetComponent<PlayerFootstepBehaviour>().footstepSurface = footstepSurface;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "HeroHDWeapons")
            other.GetComponent<PlayerFootstepBehaviour>().footstepSurface = PlayerFootstepBehaviour.Footstep_Surface.Concrete;
    }
}
