using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PxAudioSourceManager : MonoBehaviour
{
    private const int NUMBER_OF_SFX_CHANNELS = 8;
    private static PxAudioSourceManager _instance;
    
    public List<AudioSource> SfxPlayers => _sfxPlayers;
    public AudioSource BgmPlayer => _bgmPlayer;

    [SerializeField] private List<AudioSource> _sfxPlayers;
    [SerializeField] private AudioSource _bgmPlayer;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }
    public static PxAudioSourceManager CreateInstance()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Cannot create more than one instance of PxAudioSourceManager!");
            return null;
        }
        var audioManager = new GameObject("AudioMangaer", typeof(PxAudioSourceManager)).GetComponent<PxAudioSourceManager>();
        audioManager.StartCoroutine(IECreateInstance());
        return audioManager;
    }
    private static List<AudioSource> CreateSfxAudioSources()
    {
        var sfxChannels = new List<AudioSource>();
        for(int i = 0; i < NUMBER_OF_SFX_CHANNELS; i++)
        {
            var audioSource = _instance.gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
            sfxChannels.Add(audioSource);
        }
        return sfxChannels;
    }
    private static AudioSource CreateBgmAudioSource()
    {
        var audioSource = _instance.gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        return audioSource;
    }
    private static IEnumerator IECreateInstance()
    {
        _instance._sfxPlayers = CreateSfxAudioSources();
        yield return null;
        _instance._bgmPlayer = CreateBgmAudioSource();
        yield return null;
        PxAudioPlayer.AssignAudioSources(_instance._sfxPlayers, _instance._bgmPlayer);
    }
}
