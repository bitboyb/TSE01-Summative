/////////////////////////////////////////////////////////////////////////////////
//
//	vp_PlatformSwitch.cs
//	© Opsive. All Rights Reserved.
//	https://twitter.com/Opsive
//	http://www.opsive.com
//
//	description:	This class allows the player to interact with vp_MovingPlatform.
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vp_PlatformSwitch : vp_Interactable
{
	
	public float SwitchTimeout = 0;										// time in seconds before next switch can occur
	public vp_MovingPlatform Platform = null;							// platform to control
	public Vector2 SwitchPitchRange = new Vector2(1.0f, 1.5f);
    //[FMODUnity.EventRef]
	public string SwitchSounds;		// list of sounds to randomly play when switched
	
	protected bool m_IsSwitched = false; // is this object in switched mode
	protected float m_Timeout = 0;
	
	
	protected override void Start()
	{
		
		base.Start();
		
	}
	
	
	/// <summary>
	/// try to press this platform switch. this will have effect
	/// in singleplayer, but only in multiplayer if we're the
	/// master / server. sounds will always play and timers will
	/// be maintained in case of a master client handover
	/// </summary>
	public override bool TryInteract(vp_PlayerEventHandler player)
	{

		if(Platform == null)
			return false;
		
		if(m_Player == null)
			m_Player = player;
		
		if(Time.time < m_Timeout)
			return false;

		PlaySound();

		// only try to actually operate the platform if we're the master.
		// if we're just a client the master should detect the trigger enter
		// too, and should activate the platform remotely
		if (vp_Gameplay.IsMaster)
			Platform.SendMessage("GoTo", Platform.TargetedWaypoint == 0 ? 1 : 0, SendMessageOptions.DontRequireReceiver);
		else if (InteractType == vp_InteractType.Normal)
			this.SendMessage("ClientTryInteract");
		
		m_Timeout = Time.time + SwitchTimeout;
		
		m_IsSwitched = !m_IsSwitched;
		
		return true;
		
	}


	/// <summary>
	/// 
	/// </summary>
	public virtual void PlaySound()
	{
	    if (SwitchSounds == string.Empty)
        {
            return;
        }
        //FMODUnity.RuntimeManager.PlayOneShotAttached(SwitchSounds, gameObject);
	}
	
	
	/// <summary>
	/// this is triggered when an object enters the collider and
	/// InteractType is set to trigger
	/// </summary>
	protected override void OnTriggerEnter(Collider col)
	{
		
		// only do something if the trigger is of type Trigger
		if (InteractType != vp_InteractType.Trigger)
			return;

		// see if the colliding object was a valid recipient
		foreach(string s in RecipientTags)
		{
			if(col.gameObject.tag == s)
				goto isRecipient;
		}
		return;
	isRecipient:

		m_Player = col.transform.root.GetComponent<vp_PlayerEventHandler>();

		if (m_Player == null)
			return;

		if((m_Player.Interactable.Get() != null) && (m_Player.Interactable.Get().GetComponent<Collider>() == col))
			return;
		
		// calls the TryInteract method which is hopefully on the inherited class
		TryInteract(m_Player);
		
	}

}
