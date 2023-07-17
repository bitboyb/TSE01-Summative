using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//########################################################
//
//                 Thinkspace Education
//
//########################################################

public class AkStateMultipleColliders : MonoBehaviour
{
    //Wwise State references
    public AK.Wwise.State enterState;
    public AK.Wwise.State exitState;

    //Specified trigger object
    public GameObject triggerObject;
    //In case triggerObject is a child
    private GameObject triggerObjectRootParent;

    //Trigger volume count
    private int triggersEntered;

    private void Start()
    {
        triggersEntered = 0;

        triggerObjectRootParent = triggerObject.transform.root.gameObject;
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject == triggerObject || other.gameObject == triggerObjectRootParent)
        {
            triggersEntered++;
            enterState.SetValue();
        }
    }

    private void OnTriggerExit(UnityEngine.Collider other)
    {
        if (other.gameObject == triggerObject || other.gameObject == triggerObjectRootParent)
        {
            if (triggersEntered == 1)
            {
                exitState.SetValue();
            }

            triggersEntered--;
        }
    }
}
