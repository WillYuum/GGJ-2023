using System.Collections.Generic;
using UnityEngine;
using Utils.GenericSingletons;
using AudioClasses;
using DG.Tweening;

public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
    [SerializeField] private List<AudioConfig> _sfxConfigs;
    [SerializeField] private List<AudioConfig> _bgmConfigs;
    private List<Audio> _sfx;
    private List<Audio> _bgm;


    public void Load()
    {
        Debug.Log("Loading AudioManager");

        _sfx = new List<Audio>();
        _bgm = new List<Audio>();


        void LoadAudioConfigs(List<AudioConfig> configs, List<Audio> audios)
        {
            configs.ForEach(audioConfig =>
           {
               GameObject spawnedAudioObject = Instantiate(new GameObject(), transform);
               spawnedAudioObject.name = audioConfig.Name;
               AudioSource audioSource = spawnedAudioObject.AddComponent<AudioSource>();

#if UNITY_EDITOR
               if (audioSource == null) Debug.LogError("audioSource is null");
               if (audioConfig == null) Debug.LogError("audioConfig is null");
#endif

               audios.Add(new Audio(audioSource, audioConfig));
           });
        }

        LoadAudioConfigs(_sfxConfigs, _sfx);
        LoadAudioConfigs(_bgmConfigs, _bgm);
    }

    public void PlaySFX(string name) => Play(name, _sfx);

    public void PlayBGM(string name)
    {
        // StopAllBGM();
        Play(name, _bgm);
    }

    public void StopBGM(string name)
    {
        Audio audio = _bgm.Find(x => x.Name == name);
        audio.Stop();
    }

    public void StopAllBGM()
    {
        _bgm.ForEach(audio => audio.Stop());
    }


    private bool _isTweening = false;
    [SerializeField] private float _bgmFadeDuration = 1.0f;
    public void setBGMMode(float closestEnemyPosition)
    {
        if (_isTweening) return;

        Vector2 endpoints = new Vector2(0.17f, -1.68f);
        float threatIncrement = Mathf.Abs(endpoints.x - endpoints.y) / 3.0f;
        float threatBase = Mathf.Min(endpoints.x, endpoints.y);

        Audio audioLow = _bgm.Find(x => x.Name == "Drums");
        Audio audioMid = _bgm.Find(x => x.Name == "Strings");
        Audio audioHigh = _bgm.Find(x => x.Name == "WindsAndStrings");

        if (closestEnemyPosition < threatBase + threatIncrement)
        {
            audioLow.SetVolume(0.9f, _bgmFadeDuration);
            audioMid.SetVolume(0.9f, _bgmFadeDuration);
            audioHigh.SetVolume(0.9f, _bgmFadeDuration);
            _isTweening = true;
        }
        else if (closestEnemyPosition < threatBase + 2 * threatIncrement)
        {
            audioLow.SetVolume(0.9f, _bgmFadeDuration);
            audioMid.SetVolume(0.9f, _bgmFadeDuration);
            audioHigh.SetVolume(0.0f, _bgmFadeDuration);
            _isTweening = true;
        }
        else
        {
            audioLow.SetVolume(0.9f, _bgmFadeDuration);
            audioMid.SetVolume(0.0f, _bgmFadeDuration);
            audioHigh.SetVolume(0.0f, _bgmFadeDuration);
            _isTweening = true;
        }

        if (_isTweening)
        {
            Invoke(nameof(StoppedTweening), _bgmFadeDuration);
        }

        print(closestEnemyPosition < threatBase + threatIncrement);
        print(closestEnemyPosition < threatBase + 2 * threatIncrement);
        print("---");

    }

    private void StoppedTweening()
    {
        _isTweening = false;
    }


    private void Play(string audioName, List<Audio> audios)
    {
        Audio audio = audios.Find(x => x.Name == audioName);

#if UNITY_EDITOR
        if (audio == null)
        {
            Debug.LogError("Aduio not found: " + audioName);
            return;
        }
#endif

        audio.Play();
    }
}




namespace AudioClasses
{

    [System.Serializable]
    public class AudioConfig
    {
        [SerializeField] public string Name = "";
        [SerializeField] public AudioClip Clip;
        [SerializeField][Range(0f, 1.0f)] public float Volume = 1.0f;
        [SerializeField] public bool Loop = false;
        [SerializeField] public PitchProps PitchProps;
    }

    [System.Serializable]
    public class PitchProps
    {
        [SerializeField] public bool RandPitch = false;
        [SerializeField] public float Min = 0.8f;
        [SerializeField] public float Max = 1.1f;
    }



    public class Audio
    {
        private string _name;
        private AudioSource _source;


        public string Name { get => _name; }
        public bool isPlaying { get => _source.isPlaying; }

        private PitchProps _pitchProps;

        public Audio(AudioSource source, AudioConfig audioConfig)
        {
            _source = source;

            _name = audioConfig.Name;
            _source.volume = audioConfig.Volume;
            _source.clip = audioConfig.Clip;
            _source.loop = audioConfig.Loop;
            _pitchProps = audioConfig.PitchProps;
        }

        public void SetVolume(float volume, float duration, bool instant = false)
        {
            if (instant)
            {
                _source.volume = volume;
            }
            else
            {
                _source.DOFade(volume, 0.5f);
            }
        }

        public void Play()
        {
            if (_pitchProps.RandPitch)
            {
                _source.pitch = Random.Range(_pitchProps.Min, _pitchProps.Max);
            }

            _source.Play();
        }

        public void Stop()
        {
            _source.Stop();
        }
    }
}