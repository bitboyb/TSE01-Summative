using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAudio : MonoBehaviour
{
    private Dictionary<float, float> _timerDictionary;
    [SerializeField] private float _hurtTimer = 0f;
    private bool _isHurtEvent = false;
    public float hurtEventTimerLength = 3.0f;

    public void PlayEventWithString(string eventName)
    {
        AkSoundEngine.PostEvent(eventName, gameObject);
    }

    public void PlayHurtEvent()
    {
        if (!_isHurtEvent)
        {
            AkSoundEngine.PostEvent("Play_Player_Hurt", gameObject);
            _isHurtEvent = true;
        }
    }

    private void Update()
    {
        if (_isHurtEvent)
        {
            _hurtTimer += Time.deltaTime;

            if (_hurtTimer > hurtEventTimerLength)
            {
                _hurtTimer = 0f;
                _isHurtEvent = false;
            }
        }
    }
}
