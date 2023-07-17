/////////////////////////////////////////////////////////////////////////////////
//
//	vp_RandomSpawner.cs
//	© Opsive. All Rights Reserved.
//	https://twitter.com/Opsive
//	http://www.opsive.com
//
//	description:	spawns a random object from a user populated list
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;

public class vp_RandomSpawner : MonoBehaviour
{

	// sound
	public AudioClip Sound = null;
	public float SoundMinPitch = 0.8f;
	public float SoundMaxPitch = 1.2f;
	public bool RandomAngle = true;

	public List<GameObject> SpawnObjects = null;

	/// <summary>
	/// 
	/// </summary>
	void Awake()
	{

		if (SpawnObjects == null)
			return;

		int i = (int)Random.Range(0, (SpawnObjects.Count));

		if(SpawnObjects[i] == null)
			return;

		GameObject obj = (GameObject)vp_Utility.Instantiate(SpawnObjects[i], transform.position, transform.rotation);

		obj.transform.Rotate(Random.rotation.eulerAngles);

	}


}

