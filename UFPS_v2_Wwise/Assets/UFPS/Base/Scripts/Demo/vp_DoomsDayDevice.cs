﻿/////////////////////////////////////////////////////////////////////////////////
//
//	vp_DoomsDayDevice.cs
//	© Opsive. All Rights Reserved.
//	https://twitter.com/Opsive
//	http://www.opsive.com
//
//	description:	funky demo script to breathe life into the 'DoomsDayDevice'
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class vp_DoomsDayDevice : MonoBehaviour
{

	protected vp_FPPlayerEventHandler m_Player = null;

	protected bool Initiated = false;	// whether or not self destruction sequence has been initiated

	protected vp_PulsingLight m_PulsingLight = null;

	protected Vector3 m_OriginalButtonPos;
	protected Color m_OriginalButtonColor;
	protected float m_OriginalPulsingLightMaxIntensity;

	protected Renderer m_ButtonRenderer = null;
	protected Renderer ButtonRenderer
	{
		get
		{
			if (m_ButtonRenderer == null && (Button != null) && !TriedFindButtonRenderer)
			{
				m_ButtonRenderer = Button.GetComponent<Renderer>();
				TriedFindButtonRenderer = true;
			}
			return m_ButtonRenderer;
		}
	}

	protected GameObject m_Button = null;
	protected GameObject Button
	{
		get
		{
			if (m_Button == null && !TriedFindButton)
			{
				m_Button = GameObject.Find("ForbiddenButton");
				TriedFindButton = true;
			}
			return m_Button;
		}
	}

	bool TriedFindButton = false;
	bool TriedFindButtonRenderer = false;


	/// <summary>
	/// 
	/// </summary>
	protected virtual void Awake()
	{

		m_Player = GameObject.FindObjectOfType(typeof(vp_FPPlayerEventHandler)) as vp_FPPlayerEventHandler;

		if (Button != null)
		{
			m_OriginalButtonPos = Button.transform.localPosition;
			m_OriginalButtonColor = ButtonRenderer.material.color;
			m_PulsingLight = Button.GetComponentInChildren<vp_PulsingLight>();
		}

		if (m_PulsingLight != null)
			m_OriginalPulsingLightMaxIntensity = m_PulsingLight.m_MaxIntensity;

	}


	/// <summary>
	/// registers this component with the event handler (if any)
	/// </summary>
	protected virtual void OnEnable()
	{

		if (m_Player != null)
			m_Player.Register(this);

		if (Button != null)
		{
			Button.transform.localPosition = m_OriginalButtonPos;
			ButtonRenderer.material.color = m_OriginalButtonColor;
		}

		if (m_PulsingLight != null)
			m_PulsingLight.m_MaxIntensity = m_OriginalPulsingLightMaxIntensity;
		
	}


	/// <summary>
	/// unregisters this component from the event handler (if any)
	/// </summary>
	protected virtual void OnDisable()
	{

		if (m_Player != null)
			m_Player.Unregister(this);

	}


	/// <summary>
	/// 
	/// </summary>
	protected virtual void Update()
	{

		if (Initiated)
		{

			// slowly lower button color intensity
			if (Button != null)
				ButtonRenderer.material.color = Color.Lerp(ButtonRenderer.material.color, (m_OriginalButtonColor * 0.2f), Time.deltaTime * 1.5f);

			// cap max intensity of the pusling light
			if (m_PulsingLight != null)
				m_PulsingLight.m_MaxIntensity = 2.5f;

		}

	}


	/// <summary>
	/// 
	/// </summary>
	protected virtual void InitiateDoomsDay()
	{

		if (Initiated)	// prevent spam-clicking on the button
			return;

		Initiated = true;	// initiate self destruction sequence

		// depress the button a little
		if (Button != null)
			Button.transform.localPosition += Vector3.down * 0.02f;
		
		// play a rumbling sound on the player audiosource
		// (we do this for higher volume + to make the sound be 'everywhere')
		//if (EarthQuakeSound != string.Empty)
        //{

        //}

		m_Player.CameraEarthQuake.TryStart(new Vector3(0.05f, 0.05f, 10.0f));	// start the earthquake camera effect

		vp_Timer.In(3, delegate()
		{
			SendMessage("Die");	// make doomsday device explode in 3 seconds
		});

		vp_Timer.In(3, delegate() { Initiated = false; });	// re-enable button when device explodes

	}

}