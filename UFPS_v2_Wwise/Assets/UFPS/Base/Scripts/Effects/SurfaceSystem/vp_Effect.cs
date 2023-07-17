/////////////////////////////////////////////////////////////////////////////////
//
//	vp_Effect.cs
//	© Opsive. All Rights Reserved.
//	https://twitter.com/Opsive
//	http://www.opsive.com
//
//	description:	effect spawning info base class. contains the data references
//					and logic for a simple one-shot effect spawning a bunch of random
//					prefabs and playing a random sound from a list. this can be extended
//					for more complex types of effects
//
/////////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class vp_Effect : ScriptableObject
{

#if UNITY_EDITOR
    [vp_Separator]
	public vp_Separator s1;
#endif

	public SoundSection Sound = new SoundSection();
	public ObjectSection Objects = new ObjectSection();

    private GameObject lastSpawnedGameObject;

	protected int m_LastPlayFrame = 0;		// prevents excessive volume

    private GameObject rootParent;

    ////////////// 'Objects' section ////////////////

    [System.Serializable]
	public struct ObjectSection
	{
		public List<ObjectSpawnInfo> m_Objects;
#if UNITY_EDITOR
		[vp_HelpBox("All prefabs in this list will attempt to spawn simultaneously, success depending on their respective 'Probability'. Perfect for particle fx and rubble prefabs!", UnityEditor.MessageType.None, null, null, false, vp_PropertyDrawerUtility.Space.Nothing)]
		public float objectsHelp;
#endif
	}

	////////////// 'Sounds' section ////////////////

	[System.Serializable]
	public struct SoundSection
	{      
        //Wwise event
        public AK.Wwise.Event m_Sound;

#if UNITY_EDITOR
		[vp_HelpBox("• When the SurfaceEffect triggers, one AudioClip from 'Sounds' will be randomly chosen and played with a random pitch between 'MinPitch' and 'MaxPitch'.\n\n• For effects that may trigger many times at once (such as shotgun pellets) you should tick 'Max Once Per Frame' to avoid excessive sound volume on impact.\n", UnityEditor.MessageType.None, null, null, false, vp_PropertyDrawerUtility.Space.Nothing)]
		public float soundsHelp;
#endif
	}


	[System.Serializable]
	public struct ObjectSpawnInfo
	{
		public ObjectSpawnInfo(bool init)
		{
			Prefab = null;
			Probability = 1.0f;
		}
		public GameObject Prefab;
		[Range(0.0f, 1.0f)]
		public float Probability;
	}


	/// <summary>
	/// sets some default values because structs cannot have field initializers.
	/// NOTE: this is never called at runtime. only once, by the external creator
	/// of the ScriptableObject. by default: vp_UFPSMenu.cs
	/// </summary>
	public virtual void Init()
	{
		//Sound.MinPitch = 1.0f;
		//Sound.MaxPitch = 1.0f;
		Objects.m_Objects = new List<ObjectSpawnInfo>();
		Objects.m_Objects.Add(new vp_Effect.ObjectSpawnInfo(true));
	}

    /// <summary>
    /// spawns fx objects according to their probabilities and plays a
    /// random sound at the hit point and using the audiosource. if
    /// audiosource is omitted or null, no sound will be played
    /// </summary>
    public void Spawn(RaycastHit hit, GameObject audioSource)
	{
        SpawnObjects(hit);

        //if gameobject isn't provided create one from the pool and post sound on it
        if (audioSource == null)
        {
            GameObject audioImpactClone = ObjectPooler.Instance.SpawnFromPool("AudioImpactEmitter", hit.point);

            //Post Wwise sound on newly created gameobject
            //Sound.m_Sound.Post(audioImpactClone);
            audioImpactClone.GetComponent<AudioImpactEmitter>().PlayWwiseEvent(Sound.m_Sound);

            return;
        }

        //post Wwise event
        else
        {
            rootParent = audioSource.transform.root.gameObject;

            Sound.m_Sound.Post(rootParent);
        }

    }

	/// <summary>
	/// spawns one instance each of any objects from the object list that
	/// manage a die roll against their spawn probability
	/// </summary>
	void SpawnObjects(RaycastHit hit)
	{

		for (int v = 0; v < Objects.m_Objects.Count; v++)
		{
			if (Objects.m_Objects[v].Prefab == null)
				continue;
			if (Random.value < Objects.m_Objects[v].Probability)
				vp_Utility.Instantiate(Objects.m_Objects[v].Prefab, hit.point, Quaternion.LookRotation(hit.normal));

		}


    }


	/// <summary>
	/// plays a randomly chosen sound from the sound list. if there is more
	/// than one sound, makes sure never to play the same sound twice in a
	/// row
	/// </summary>
	public virtual void PlaySound(GameObject audioSource)
	{
        //post Wwise event

        Sound.m_Sound.Post(audioSource);
    }


}

