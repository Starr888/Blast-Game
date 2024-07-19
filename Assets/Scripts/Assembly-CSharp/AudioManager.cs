using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	public bool isSoundEnabled = true;

	public bool isMusicEnabled = true;

	public AudioSource audioSource;

	public AudioClip SFX_ButtonClick;

	public AudioClip SFX_BlockPlace;

	public AudioClip SFX_GameOver;

	[Header("Play")]
	public AudioClip tap;

	public AudioClip tapBck;

	public AudioClip itemCrush;

	public AudioClip bomb;

	public AudioClip bombExplode;

	public AudioClip colRowBreaker;

	public AudioClip colRowBreakerExplode;

	public AudioClip rainbow;

	public AudioClip rainbowExplode;

	public AudioClip drop;

	public AudioClip coinPay;

	public AudioClip coinAdd;

	public AudioClip UIPopupLevelSkipped;

	public AudioClip waffleExplode;

	public AudioClip collectTarget;

	public AudioClip cageExplode;

	public AudioClip gingerbreadExplode;

	public AudioClip gingerbread;

	public AudioClip marshmallowExplode;

	public AudioClip collectibleExplode;

	public AudioClip chocolateExplode;

	public AudioClip rockCandyExplode;

	public AudioClip amazing;

	public AudioClip exellent;

	public AudioClip great;

	public AudioClip star1;

	public AudioClip star2;

	public AudioClip star3;

	[Header("UI")]
	public AudioClip Click;

	public AudioClip Target;

	public AudioClip completed;

	public AudioClip Win;

	public AudioClip Lose;

	public AudioClip NoMatch;

	public AudioClip Gift;

	public AudioClip giftgiris;

	public AudioClip giftvar;

	public AudioClip completedMusic;

	[Header("Booster")]
	public AudioClip singleBooster;

	public AudioClip rowBooster;

	public AudioClip columnBooster;

	public AudioClip rainbowBooster;

	public AudioClip ovenBooster;

	[Header("Check")]
	public bool playingCookieCrush;

	public bool playingBomb;

	public bool playingBombExplode;

	public bool playingColRowBreaker;

	public bool playingColRowBreakerExplode;

	public bool playingRainbow;

	public bool playingRainbowExplode;

	public bool playingDrop;

	public bool playingWaffleExplode;

	public bool playingCageExplode;

	public bool playingMarshmallowExplode;

	public bool playingChocolateExplode;

	public bool playingRockCandyExplode;

	private float delay = 0.01f;

	private static AudioManager _instance;

	public static AudioManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = UnityEngine.Object.FindObjectOfType<AudioManager>();
			}
			return _instance;
		}
	}

	public static event Action<bool> OnSoundStatusChangedEvent;

	public static event Action<bool> OnMusicStatusChangedEvent;

	private void Awake()
	{
		if (_instance != null && _instance.gameObject != base.gameObject)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		else
		{
			_instance = UnityEngine.Object.FindObjectOfType<AudioManager>();
		}
	}

	private void OnEnable()
	{
		initAudioStatus();
	}

	public void initAudioStatus()
	{
		isSoundEnabled = ((PlayerPrefs.GetInt("isSoundEnabled", 0) == 0) ? true : false);
		isMusicEnabled = ((PlayerPrefs.GetInt("isMusicEnabled", 0) == 0) ? true : false);
		if (!isSoundEnabled && AudioManager.OnSoundStatusChangedEvent != null)
		{
			AudioManager.OnSoundStatusChangedEvent(isSoundEnabled);
		}
		if (!isMusicEnabled && AudioManager.OnMusicStatusChangedEvent != null)
		{
			AudioManager.OnMusicStatusChangedEvent(isMusicEnabled);
		}
	}

	public void ToggleSoundStatus(bool state)
	{
		isSoundEnabled = state;
		PlayerPrefs.SetInt("isSoundEnabled", (!isSoundEnabled) ? 1 : 0);
		if (AudioManager.OnSoundStatusChangedEvent != null)
		{
			AudioManager.OnSoundStatusChangedEvent(isSoundEnabled);
		}
	}

	public void ToggleMusicStatus(bool state)
	{
		isMusicEnabled = state;
		PlayerPrefs.SetInt("isMusicEnabled", (!isMusicEnabled) ? 1 : 0);
		if (AudioManager.OnMusicStatusChangedEvent != null)
		{
			AudioManager.OnMusicStatusChangedEvent(isMusicEnabled);
		}
	}

	public void PlayButtonClickSound()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(SFX_ButtonClick);
		}
	}

	public void ButtonClickAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(Click);
		}
	}

	public void PlayOneShotClip(AudioClip clip)
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(clip);
		}
	}

	public void SwapBackAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(tapBck);
		}
	}

	public void SwapAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(tap);
		}
	}

	public void CookieCrushAudio()
	{
		if (!playingCookieCrush && isSoundEnabled)
		{
			playingCookieCrush = true;
			audioSource.PlayOneShot(itemCrush);
			StartCoroutine(ResetCookieCrushAudio());
		}
	}

	private IEnumerator ResetCookieCrushAudio()
	{
		if (isSoundEnabled)
		{
			yield return new WaitForSeconds(delay);
			playingCookieCrush = false;
		}
	}

	public void BombBreakerAudio()
	{
		if (!playingBomb && isSoundEnabled)
		{
			playingBomb = true;
			audioSource.PlayOneShot(bomb);
			StartCoroutine(ResetBombAudio());
		}
	}

	private IEnumerator ResetBombAudio()
	{
		yield return new WaitForSeconds(delay);
		playingBomb = false;
	}

	public void BombExplodeAudio()
	{
		if (!playingBombExplode && isSoundEnabled)
		{
			playingBombExplode = true;
			audioSource.PlayOneShot(bombExplode);
			StartCoroutine(ResetBombExplodeAudio());
		}
	}

	private IEnumerator ResetBombExplodeAudio()
	{
		yield return new WaitForSeconds(delay);
		playingBombExplode = false;
	}

	public void ColRowBreakerAudio()
	{
		if (!playingColRowBreaker && isSoundEnabled)
		{
			playingColRowBreaker = true;
			audioSource.PlayOneShot(colRowBreaker);
			StartCoroutine(ResetColRowBreakerAudio());
		}
	}

	private IEnumerator ResetColRowBreakerAudio()
	{
		yield return new WaitForSeconds(delay);
		playingColRowBreaker = false;
	}

	public void ColRowBreakerExplodeAudio()
	{
		if (!playingColRowBreakerExplode && isSoundEnabled)
		{
			playingColRowBreakerExplode = true;
			audioSource.PlayOneShot(colRowBreakerExplode);
			StartCoroutine(ResetColRowBreakerExplodeAudio());
		}
	}

	private IEnumerator ResetColRowBreakerExplodeAudio()
	{
		yield return new WaitForSeconds(delay);
		playingColRowBreakerExplode = false;
	}

	public void RainbowAudio()
	{
		if (!playingRainbow && isSoundEnabled)
		{
			playingRainbow = true;
			audioSource.PlayOneShot(rainbow);
			StartCoroutine(ResetRainbowAudio());
		}
	}

	private IEnumerator ResetRainbowAudio()
	{
		yield return new WaitForSeconds(delay);
		playingRainbow = false;
	}

	public void RainbowExplodeAudio()
	{
		if (!playingRainbowExplode && isSoundEnabled)
		{
			playingRainbowExplode = true;
			audioSource.PlayOneShot(rainbowExplode);
			StartCoroutine(ResetRainbowExplodeAudio());
		}
	}

	private IEnumerator ResetRainbowExplodeAudio()
	{
		yield return new WaitForSeconds(delay);
		playingRainbowExplode = false;
	}

	public void DropAudio()
	{
		if (!playingDrop && isSoundEnabled)
		{
			playingDrop = true;
			audioSource.PlayOneShot(drop);
			StartCoroutine(ResetDropAudio());
		}
	}

	private IEnumerator ResetDropAudio()
	{
		yield return new WaitForSeconds(delay);
		playingDrop = false;
	}

	public void WaffleExplodeAudio()
	{
		if (!playingWaffleExplode && isSoundEnabled)
		{
			playingWaffleExplode = true;
			audioSource.PlayOneShot(waffleExplode);
			StartCoroutine(ResetWaffleExplodeAudio());
		}
	}

	private IEnumerator ResetWaffleExplodeAudio()
	{
		yield return new WaitForSeconds(delay);
		playingWaffleExplode = false;
	}

	public void CollectTargetAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(collectTarget);
		}
	}

	public void CageExplodeAudio()
	{
		if (!playingCageExplode && isSoundEnabled)
		{
			playingCageExplode = true;
			audioSource.PlayOneShot(cageExplode);
			StartCoroutine(ResetCageExplodeAudio());
		}
	}

	private IEnumerator ResetCageExplodeAudio()
	{
		yield return new WaitForSeconds(delay);
		playingCageExplode = false;
	}

	public void GingerbreadExplodeAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(gingerbreadExplode);
		}
	}

	public void GingerbreadAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(gingerbread);
		}
	}

	public void MarshmallowExplodeAudio()
	{
		if (!playingMarshmallowExplode && isSoundEnabled)
		{
			playingMarshmallowExplode = true;
			audioSource.PlayOneShot(marshmallowExplode);
			StartCoroutine(ResetMarshmallowExplodeAudio());
		}
	}

	private IEnumerator ResetMarshmallowExplodeAudio()
	{
		yield return new WaitForSeconds(delay);
		playingMarshmallowExplode = false;
	}

	public void ChocolateExplodeAudio()
	{
		if (!playingChocolateExplode && isSoundEnabled)
		{
			playingChocolateExplode = true;
			audioSource.PlayOneShot(chocolateExplode);
			StartCoroutine(ResetChocolateExplodeAudio());
		}
	}

	private IEnumerator ResetChocolateExplodeAudio()
	{
		yield return new WaitForSeconds(delay);
		playingChocolateExplode = false;
	}

	public void CollectibleExplodeAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(collectibleExplode);
		}
	}

	public void RockCandyExplodeAudio()
	{
		if (!playingRockCandyExplode && isSoundEnabled)
		{
			playingRockCandyExplode = true;
			audioSource.PlayOneShot(rockCandyExplode);
			StartCoroutine(ResetRockCandyExplodeAudio());
		}
	}

	private IEnumerator ResetRockCandyExplodeAudio()
	{
		yield return new WaitForSeconds(delay);
		playingRockCandyExplode = false;
	}

	public void PopupTargetAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(Target);
		}
	}

	public void PopupCompletedAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(completed);
		}
	}

	public void PopupCompletedMusicAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(completedMusic);
		}
	}

	public void PopupWinAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(Win);
		}
	}

	public void PopupLoseAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(Lose);
		}
	}

	public void CoinPayAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(coinPay);
		}
	}

	public void CoinAddAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(coinAdd);
		}
	}

	public void PopupLevelSkippedAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(UIPopupLevelSkipped);
		}
	}

	public void PopupNoMatchesAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(NoMatch);
		}
	}

	public void giftsound()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(Gift);
		}
	}

	public void giftbuton()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(giftvar);
		}
	}

	public void SingleBoosterAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(singleBooster);
		}
	}

	public void RowBoosterAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(rowBooster);
		}
	}

	public void ColumnBoosterAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(columnBooster);
		}
	}

	public void RainbowBoosterAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(rainbowBooster);
		}
	}

	public void OvenBoosterAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(ovenBooster);
		}
	}

	public void amazingAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(amazing);
		}
	}

	public void exellentAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(exellent);
		}
	}

	public void greatAudio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(great);
		}
	}

	public void Star1Audio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(star1);
		}
	}

	public void Star2Audio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(star2);
		}
	}

	public void Star3Audio()
	{
		if (isSoundEnabled)
		{
			audioSource.PlayOneShot(star3);
		}
	}
}
