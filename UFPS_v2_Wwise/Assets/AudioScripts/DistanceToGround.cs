using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;


public class DistanceToGround : MonoBehaviour
{
    public AK.Wwise.RTPC heightRTPC;

    public Transform raycastOrigin;

    private float checkCooldown = 0.2f;
    private float localCheckCooldown;

    private float roundedRTPCvalue;

    private void Start()
    {
        heightRTPC.SetGlobalValue(0);
    }


    private void Update()
    {
        if (Time.time - checkCooldown >= localCheckCooldown)
        {
            localCheckCooldown = Time.time;
            GroundSurfaceCheck();
        } 
    }


    private void GroundSurfaceCheck()
    {
        //Debug.DrawLine(centre.position, centre.position + Vector3.down * 200.0f, Color.blue);

        //Hits only Ground layer, rounds number to nearest then sets RTPC
        RaycastHit hit;
        if(Physics.Raycast (raycastOrigin.position, Vector3.down, out hit, 200.0f, 1<<20))
            if (hit.collider)
            {
                roundedRTPCvalue = UnityEngine.Mathf.Round(hit.distance);

                heightRTPC.SetGlobalValue(roundedRTPCvalue);
            }

        //Debug.Log("Current Height =" + roundedRTPCvalue);
    }
}
