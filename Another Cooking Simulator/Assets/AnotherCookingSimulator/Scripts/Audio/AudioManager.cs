using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;
    private Sound s;

    public AudioMixerGroup[] audioMixerGroups;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if (s.name == "LobbyMusic") s.source.outputAudioMixerGroup = audioMixerGroups[0];
            if (s.name == "GameMusic") s.source.outputAudioMixerGroup = audioMixerGroups[0];
            if (s.name.Contains("FX-"))
            {
                if (s.name == "FX-Fry") s.source.outputAudioMixerGroup = audioMixerGroups[1];
                if (s.name == "FX-Ring") s.source.outputAudioMixerGroup = audioMixerGroups[2];
                if (s.name == "FX-Tap") s.source.outputAudioMixerGroup = audioMixerGroups[3];
                if (s.name == "FX-ButtonClick") s.source.outputAudioMixerGroup = audioMixerGroups[4];
            }
        }
    }
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("LobbyMusic");
    }
    public void PlayInPosition(string name, Vector3 position)
    {
        s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        //AudioSource.PlayClipAtPoint(s.clip, transform.position = position);
        s.source.Play();
    }
    
    public void SwapLobbyMusicToGameMusic(string lobbyMusic, string gameMusic)
    {
        FindObjectOfType<AudioManager>().Stop(lobbyMusic);
        FindObjectOfType<AudioManager>().Play(gameMusic);
    }
    public void Play(string name)
    {
        s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }

    public bool sigueSonando(/*string name*/)
    {
        bool sigueSonando;
        //s = Array.Find(sounds, sound => sound.name == name);
        sigueSonando = s.source.isPlaying;
        return sigueSonando;
    }

    public void Stop(string name)
    {
        s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Stop();
    }
}
