using UnityEngine;

namespace EasyMobile.Demo
{
	[RequireComponent(typeof(AudioSource))]
	public class SoundManager : MonoBehaviour
	{
		public AudioClip button;

		private AudioSource _audioSource;

		public static SoundManager Instance { get; private set; }

		public AudioSource AudioSource
		{
			get
			{
				if (_audioSource == null)
				{
					_audioSource = GetComponent<AudioSource>();
				}
				return _audioSource;
			}
		}

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
				Object.DontDestroyOnLoad(base.gameObject);
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}

		public void PlaySound(AudioClip sound)
		{
			AudioSource.PlayOneShot(sound);
		}
	}
}
