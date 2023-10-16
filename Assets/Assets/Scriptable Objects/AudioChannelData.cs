using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Channel Data", menuName = "Audio/Audio Channel Data")]
public class AudioChannelData : ScriptableObject
{
    public float volume = 1.0f;
    public bool isMuted = false;
}

