using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            if (_instance != null) return _instance;
            else
            {
                Debug.LogError("Audio Manager not found");
                return null;
            }
        }
    }

    [SerializeField] AudioClip _bgMusic, _atmosphereClip;
    [SerializeField] AudioSource _musicSource, _atmosphereSource, _sfxSource;

    private bool _endLevelClip;

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        _endLevelClip = false;

        PlayMusic(_bgMusic);
        PlayAtmosphere(_atmosphereClip);
    }

    private void Update()
    {
        if (_endLevelClip)
        {
            _musicSource.Stop();
            _atmosphereSource.Stop();
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    private void PlayAtmosphere(AudioClip clip)
    {
        if (clip == null) return;
        _atmosphereSource.clip = clip;
        _atmosphereSource.Play();
    }

    public void PlaySFX(AudioClip _clip)
    {
        _sfxSource.clip = _clip;
        _sfxSource.pitch = Random.Range(0.65f, 0.95f);
        _sfxSource.volume = 0.75f;
        _sfxSource.Play();
    }

    public void PlayEndLevelSFX(AudioClip _clip)
    {
        _endLevelClip = true;
        _sfxSource.clip = _clip;
        _sfxSource.volume = 1f;
        _sfxSource.Play();
    }
}
