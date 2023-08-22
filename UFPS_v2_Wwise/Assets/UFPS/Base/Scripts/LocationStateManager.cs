using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LocationStateManager : MonoBehaviour
{
    private enum LocationSize
    {
        Small,
        Medium,
        Large,
    }

    private enum CeilingHeight
    {
        Low,
        Medium,
        High
    }

    [SerializeField] private CeilingHeight _ceilingHeight;
    [SerializeField] private LocationSize _locationSize;
    private float _lastSize;

    public bool isOutside = true;
    public float  mediumSizeThreshold = 800, largeSizeThreshold = 1000;
    public float smallCeilingThreshold = 11, mediumCeilingThreshold = 20;

    public void SetLocationSize(float size)
    {
        if (size < mediumSizeThreshold)
            _locationSize = LocationSize.Small;
        else if (size > mediumSizeThreshold && size < largeSizeThreshold)
            _locationSize = LocationSize.Medium;
        else if (size > largeSizeThreshold)
            _locationSize = LocationSize.Large;

        AkSoundEngine.SetState("LocationSize", _locationSize.ToString());
    }

    public void SetCeilingHeight(float height)
    {
        if (height < smallCeilingThreshold)
            _ceilingHeight = CeilingHeight.Low;
        else if (height > smallCeilingThreshold && height < mediumCeilingThreshold)
            _ceilingHeight = CeilingHeight.Medium;
        else if (height > mediumCeilingThreshold)
            _ceilingHeight = CeilingHeight.High;

        AkSoundEngine.SetState("CeilingHeight", _ceilingHeight.ToString());
    }
}
