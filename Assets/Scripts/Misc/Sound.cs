using UnityEngine;

[System.Serializable]
public class Sound{
    public string name;
    public AudioClip clip;
    public SoundClip soundClip;

    public bool musica;
    public bool som;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 3f)]
    public float pitch;
    public bool loop;
    public bool awake;

    [HideInInspector]
    public AudioSource source;
}
