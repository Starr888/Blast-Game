using System;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
	public float oneLifeRecoveryTime;

	public bool runTimer;

	public float timerwheel;

	public Text lifeText;

	public Text timerText;

	private void Start()
	{
		if (!PlayerPrefs.HasKey(Configuration.stringLife))
		{
			Configuration.instance.life = Configuration.instance.maxLife;
			lifeText.text = Configuration.instance.life.ToString();
			timerText.text = Configuration.instance.life + "/" + Configuration.instance.life;
			PlayerPrefs.SetInt(Configuration.stringLife, Configuration.instance.life);
			PlayerPrefs.Save();
		}
		if (!PlayerPrefs.HasKey(Configuration.first_setup_date))
		{
			PlayerPrefs.SetString(Configuration.first_setup_date, DateTime.Now.ToString());
			PlayerPrefs.Save();
		}
		if (!PlayerPrefs.HasKey(Configuration.promo_date))
		{
			PlayerPrefs.SetString(Configuration.promo_date, DateTime.Now.ToString());
			PlayerPrefs.Save();
		}
		if (!PlayerPrefs.HasKey(Configuration.promo_date2))
		{
			PlayerPrefs.SetString(Configuration.promo_date2, DateTime.Now.ToString());
			PlayerPrefs.Save();
		}
		if (!PlayerPrefs.HasKey(Configuration.exit_date_time_wheel))
		{
			PlayerPrefs.SetString(Configuration.exit_date_time_wheel, DateTime.Now.ToString());
			PlayerPrefs.SetFloat(Configuration.wheelTimer, timerwheel);
			PlayerPrefs.Save();
		}
		if (Configuration.instance.timer == 0f)
		{
			Configuration.instance.exitDateTime = PlayerPrefs.GetString(Configuration.exit_date_time, default(DateTime).ToString());
			Configuration.instance.timer = PlayerPrefs.GetFloat(Configuration.stringTimer, 0f);
			Configuration.instance.life = PlayerPrefs.GetInt(Configuration.stringLife, Configuration.instance.maxLife);
		}
		if (Configuration.instance.timerwheel == 0f)
		{
			Configuration.instance.exitDateTimeWheel = PlayerPrefs.GetString(Configuration.exit_date_time_wheel, default(DateTime).ToString());
			Configuration.instance.timerwheel = PlayerPrefs.GetFloat(Configuration.wheelTimer, 0f);
		}
		Configuration.instance.firstSetupDate = PlayerPrefs.GetString(Configuration.first_setup_date, default(DateTime).ToString());
		Configuration.instance.PromoDate = PlayerPrefs.GetString(Configuration.promo_date, default(DateTime).ToString());
		Configuration.instance.PromoDate2 = PlayerPrefs.GetString(Configuration.promo_date2, default(DateTime).ToString());
		DateTime value = DateTime.Parse(Configuration.instance.firstSetupDate);
		Configuration.instance.setupTimeElapsed = (float)DateTime.Now.Subtract(value).TotalSeconds / 60f;
		DateTime value2 = DateTime.Parse(Configuration.instance.PromoDate);
		Configuration.instance.promoTimer = (float)DateTime.Now.Subtract(value2).TotalSeconds / 86400f;
		DateTime value3 = DateTime.Parse(Configuration.instance.PromoDate2);
		Configuration.instance.promoTimer2 = (float)DateTime.Now.Subtract(value3).TotalSeconds / 86400f;
		DateTime value4 = DateTime.Parse(Configuration.instance.exitDateTime);
		Configuration.instance.exitTimeElapsed = (float)DateTime.Now.Subtract(value4).TotalSeconds / 60f;
		Configuration.instance.timerwheel = PlayerPrefs.GetFloat(Configuration.wheelTimer, 0f);
		DateTime value5 = DateTime.Parse(Configuration.instance.exitDateTimeWheel);
		Configuration.instance.timerwheel = (float)DateTime.Now.Subtract(value5).TotalSeconds;
		oneLifeRecoveryTime = (float)Configuration.instance.lifeRecoveryHour * 60f * 60f + (float)Configuration.instance.lifeRecoveryMinute * 60f + (float)Configuration.instance.lifeRecoverySecond;
	}

	private void Update()
	{
		if (!runTimer && Configuration.instance.life < Configuration.instance.maxLife && CheckRecoveryTime())
		{
			runTimer = true;
		}
		if (runTimer)
		{
			CalculateTimer(Time.deltaTime);
		}
		if (Configuration.instance.life < Configuration.instance.maxLife)
		{
			int num = Mathf.FloorToInt(Configuration.instance.timer / 3600f);
			int num2 = Mathf.FloorToInt((Configuration.instance.timer - (float)(num * 3600)) / 60f);
			int num3 = Mathf.FloorToInt(Configuration.instance.timer - (float)(num * 3600) - (float)(num2 * 60));
			if (Configuration.instance.lifeRecoveryHour > 0)
			{
				timerText.text = string.Format("{0:00}:{1:00}:{2:00}", num, num2, num3);
			}
			else
			{
				timerText.text = string.Format("{0:00}:{1:00}", num2, num3);
			}
		}
		else
		{
			lifeText.text = Configuration.instance.life.ToString();
			timerText.text = Configuration.instance.life + "/" + Configuration.instance.life;
			runTimer = false;
			Configuration.instance.timer = 0f;
		}
	}

	private bool CheckRecoveryTime()
	{
		if (Configuration.instance.exitDateTime == default(DateTime).ToString())
		{
			Configuration.instance.exitDateTime = DateTime.Now.ToString();
		}
		DateTime value = DateTime.Parse(Configuration.instance.exitDateTime);
		if (DateTime.Now.Subtract(value).TotalSeconds > (double)(oneLifeRecoveryTime * (float)(Configuration.instance.maxLife - Configuration.instance.life)))
		{
			Configuration.instance.life = Configuration.instance.maxLife;
			lifeText.text = Configuration.instance.life.ToString();
			Configuration.instance.timer = 0f;
			return false;
		}
		CalculateTimer((float)DateTime.Now.Subtract(value).TotalSeconds);
		return true;
	}

	private void CalculateTimer(float duration)
	{
		if (Configuration.instance.timer <= 0f && duration < 1f)
		{
			Configuration.instance.timer = oneLifeRecoveryTime;
		}
		if (Configuration.instance.timer <= duration)
		{
			if (duration < 1f)
			{
				AddLife(1);
				Configuration.instance.timer = oneLifeRecoveryTime;
			}
			else if (duration >= oneLifeRecoveryTime)
			{
				AddLife((int)duration / (int)oneLifeRecoveryTime);
				Configuration.instance.timer -= (int)duration % (int)oneLifeRecoveryTime;
			}
			else
			{
				AddLife(1);
				Configuration.instance.timer = oneLifeRecoveryTime - (duration - Configuration.instance.timer);
			}
		}
		else
		{
			Configuration.instance.timer -= duration;
			lifeText.text = Configuration.instance.life.ToString();
		}
	}

	public void AddLife(int count)
	{
		Configuration.instance.life += count;
		if (Configuration.instance.life > Configuration.instance.maxLife)
		{
			Configuration.instance.life = Configuration.instance.maxLife;
		}
		lifeText.text = Configuration.instance.life.ToString();
	}

	public void ReduceLife(int count)
	{
		int life = Configuration.instance.life;
		Configuration.instance.life = ((life - 1 >= 0) ? (life - 1) : 0);
		lifeText.text = Configuration.instance.life.ToString();
		Configuration.instance.exitDateTime = DateTime.Now.ToString();
	}
}
