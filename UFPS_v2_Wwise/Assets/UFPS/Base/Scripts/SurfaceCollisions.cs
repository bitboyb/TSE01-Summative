using UnityEngine;
using System.Collections;

public class SurfaceCollisions : MonoBehaviour {

    public bool isWood;

    //hard coded Wwise collision events
    public const uint WoodSound = 1054012614;
    public const uint MetalSound = 4026866594;

    void OnCollisionEnter(Collision collision)
    {
        if (isWood)
        {
            AkSoundEngine.PostEvent(WoodSound, gameObject);
        }
        else if (!isWood)
        {
            AkSoundEngine.PostEvent(MetalSound, gameObject);
        }
    }
}
