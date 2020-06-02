using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private Sound s;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    public void PlayInPosition(string name, Vector3 position)
    {
        s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        AudioSource.PlayClipAtPoint(s.clip, transform.position = position);
    }
    public bool sigueSonando(/*string name*/)
    {
        bool sigueSonando;
        //s = Array.Find(sounds, sound => sound.name == name);
        sigueSonando = s.source.isPlaying;
        return sigueSonando;
    }
    public void hacerSonar()
    {

    }
    public Sound pedirSource(string name)
    {
        Sound sonido = Array.Find(sounds, sound => sound.name == name);
        return sonido;
    }
    public void pararDeSonar()
    {

    }
}
