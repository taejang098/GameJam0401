using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

	private AudioSource _bgmSource;
	private Transform _sfxSources;

	private Dictionary<string, AudioClip> _bgmClips = new Dictionary<string, AudioClip>();
	private Dictionary<string, AudioClip> _sfxClips = new Dictionary<string, AudioClip>();
	private void Start()
	{
		var bgmList = ResourceManager.LoadAll<AudioClip>("Sound/Bgm");
		var sfxList = ResourceManager.LoadAll<AudioClip>("Sound/Sfx");

		if (bgmList != null)
        {
			foreach (AudioClip s in bgmList)
			{
				_bgmClips.Add(s.name, s);
			}
		}

		if (sfxList != null)
		{
			foreach (AudioClip s in sfxList)
			{
				_sfxClips.Add(s.name, s);
			}
		}


        _bgmSource = new GameObject("BgmSource").AddComponent<AudioSource>();
        _bgmSource.loop = true;
        _bgmSource.volume = 0.5f;

        _sfxSources = new GameObject("SfxSources").transform;

        _bgmSource.transform.SetParent(transform);
        _sfxSources.SetParent(transform);



        for (int i = 0; i < 5; i++)//sfxSource 자식으로 5개만들기
        {
            GameObject temp = new GameObject("sfxSource" + (i + 1));
            temp.AddComponent<AudioSource>();
            temp.transform.SetParent(_sfxSources);
            temp.GetComponent<AudioSource>().volume = 0.5f;
        }

    }

    public void Clear()
	{
		// 재생기 전부 재생 스탑, 음반 빼기

		_bgmSource.clip = null;
		_bgmSource.Stop();

		foreach (AudioSource es in _sfxSources)
		{
			es.clip = null;
			es.Stop();
		}

		// 효과음 Dictionary 비우기
		_bgmClips.Clear();
		_sfxClips.Clear();
	}

	public void Play(Define.AudioType type, string name)
	{

		if (type == Define.AudioType.Bgm)
		{
			if (_bgmSource.isPlaying)
				_bgmSource.Stop();

			AudioClip audioClip = FindClip(type, name);

			_bgmSource.clip = audioClip;
			_bgmSource.Play();
		}
		else
		{
			AudioClip audioClip = FindClip(type, name);
			//print(FindClip(type, name));
			foreach (Transform source in _sfxSources)
			{
				AudioSource temp = source.GetComponent<AudioSource>();
				if (!temp.isPlaying)
				{
					temp.clip = audioClip;
					temp.Play();
					return;
				}
			}

			//SFXSource가 부족할 시 생성 후 실행
			GameObject sfx = new GameObject("SfxSource" + (_sfxSources.childCount + 1));
			sfx.AddComponent<AudioSource>();
			sfx.transform.SetParent(_sfxSources);

			sfx.GetComponent<AudioSource>().clip = audioClip;
			sfx.GetComponent<AudioSource>().Play();
		}


	}

	public float GetVolume(Define.AudioType type)
	{
		if (type == Define.AudioType.Bgm)
		{
			if (_bgmSource == null)
				return -1;

			return _bgmSource.volume;

		}
		else
		{
			if (_sfxSources == null)
				return -1;

			return _sfxSources.GetChild(0).GetComponent<AudioSource>().volume;

		}

	}

	public void SetVolume(Define.AudioType type, float volume = 1.0f)
	{
		if (type == Define.AudioType.Bgm)
		{
			if (_bgmSource == null)
				return;

			_bgmSource.volume = volume;

		}
		else
		{
			if (_sfxSources == null)
				return;
			foreach (Transform sfx in _sfxSources)
			{
				sfx.GetComponent<AudioSource>().volume = volume;
			}

		}

	}

	public bool GetMute(Define.AudioType type)
	{
		if (type == Define.AudioType.Bgm)
		{
			return _bgmSource.mute;
		}
		else
		{
			return _sfxSources.GetChild(0).GetComponent<AudioSource>().mute;
		}

	}

	public void SetMute(Define.AudioType type, bool value)
	{
		if (type == Define.AudioType.Bgm)
		{
			if (_bgmSource == null)
				return;

			_bgmSource.mute = value;

		}
		else
		{
			if (_sfxSources == null)
				return;
			foreach (Transform sfx in _sfxSources)
			{
				sfx.GetComponent<AudioSource>().mute = value;
			}

		}

	}

	public void SetPitch(Define.AudioType type, float pitch = 1.0f)
	{
		if (type == Define.AudioType.Bgm)
		{
			if (_bgmSource == null)
				return;

			_bgmSource.pitch = pitch;

		}
		else
		{
			if (_sfxSources == null)
				return;
			foreach (Transform sfx in _sfxSources)
			{
				sfx.GetComponent<AudioSource>().pitch = pitch;
			}

		}

	}

	private AudioClip FindClip(Define.AudioType type, string name)
	{
		AudioClip audioClip = null;
		if (type == Define.AudioType.Bgm)
		{
			if (_bgmClips.TryGetValue(name, out audioClip))
			{
				return audioClip;
			}
		}
		else
		{

			if (_sfxClips.TryGetValue(name, out audioClip))
			{
				return audioClip;
			}
		}
		return audioClip;
	}


}
