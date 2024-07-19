using System;
using UnityEngine;

public class Configuration : MonoBehaviour
{
	public static Configuration instance;

	[Header("Configuration")]
	public float swapTime;

	public float destroyTime;

	public float dropTime;

	public float changingTime;

	public float hintDelay;

	[Header("Score Collectable Items")]
	public int scoreItem;

	public int finishedScoreItem;

	[Header("Bonus Stars")]
	public int bonus1Star;

	public int bonus2Star;

	public int bonus3Star;

	[Header("Packages")]
	public int package1Amount;

	public int package2Amount;

	[Header("Powers")]
	public int beginFiveMovesLevel;

	public int beginRainbowLevel;

	public int beginBombBreakerLevel;

	[Header("Moves Cost")]
	public int beginFiveMovesCost1;

	public int beginFiveMovesCost2;

	[Header("Rainbow Cost")]
	public int beginRainbowCost1;

	public int beginRainbowCost2;

	[Header("Bomb Cost")]
	public int beginBombBreakerCost1;

	public int beginBombBreakerCost2;

	[Header("Misc Cost")]
	public int keepPlayingCost;

	public int skipLevelCost;

	[Header("Breaker Cost")]
	public int singleBreakerCost1;

	public int singleBreakerCost2;

	[Header("RB Cost")]
	public int rowBreakerCost1;

	public int rowBreakerCost2;

	[Header("CB Cost")]
	public int columnBreakerCost1;

	public int columnBreakerCost2;

	[Header("Rainbow/B Cost")]
	public int rainbowBreakerCost1;

	public int rainbowBreakerCost2;

	[Header("Oven/B Cost")]
	public int ovenBreakerCost1;

	public int ovenBreakerCost2;

	[Header("Hints")]
	public int plusMoves = 5;

	public bool showHint;

	[Header("Shop")]
	public float product1Price;

	public float product2Price;

	public float product3Price;

	public float product4Price;

	public float product5Price;

	[Header("Product")]
	public int product1Coin;

	public int product2Coin;

	public int product3Coin;

	public int product4Coin;

	public int product5Coin;

	public int watchVideoCoin;

	[Header("Check")]
	public int autoPopup;

	[Header("Begin Options")]
	public bool beginFiveMoves;

	public bool beginRainbow;

	public bool beginBombBreaker;

	[Header("Max Values")]
	public bool touchIsSwallowed;

	public static int maxItems = 6;

	public int maxLevel = 100;

	[Header("Check to disable debug")]
	public bool checkSwap;

	[Header("Encouraging Popup")]
	public int encouragingPopup;

	[Header("Life")]
	public int life;

	public float timer;

	public float timerwheel;

	public float promoTimer;

	public float promoTimer2;

	public string PromoDate;

	public string PromoDate2;

	public string firstSetupDate;

	public float setupTimeElapsed;

	public string exitDateTime;

	public float exitTimeElapsed;

	public string exitDateTimeWheel;

	[Header("Life Timer")]
	public int maxLife;

	public int lifeRecoveryHour;

	public int lifeRecoveryMinute;

	public int lifeRecoverySecond;

	public int recoveryCostPerLife;

	[Header("rate us")]
	public int passLevelCounter;

	public int ShowRateEvery;

	[Header("RandomGifts")]
	public int gift1;

	public int giftsayisi;

	public static string game_data = "items.dat";

	public static string opened_level = "opened_level";

	public static string level_statistics = "level_statistics";

	public static string level_star = "level_star";

	public static string level_score = "level_score";

	public static string level_number = "level_number";

	public static string player_coin = "player_coin";

	public static string single_breaker = "single_breaker";

	public static string row_breaker = "row_breaker";

	public static string column_breaker = "column_breaker";

	public static string rainbow_breaker = "rainbow_breaker";

	public static string oven_breaker = "oven_breaker";

	public static string begin_five_moves = "begin_five_moves";

	public static string begin_rainbow = "begin_rainbow";

	public static string begin_bomb_breaker = "begin_bomb_breaker";

	public static string player_stars = "player_stars";

	public static string player_puan = "player_puan";

	public static string gift_amount = "gift_amount";

	public static string toplam_score = "toplam_score";

	public static string exit_date_time = "string_exit_date_time";

	public static string stringLife = "string_life";

	public static string stringTimer = "string_timer";

	public static string first_setup_date = "string_first_setup_date";

	public static string exit_date_time_wheel = "string_exit_date_time_wheel";

	public static string wheelTimer = "wheel_timer";

	public static string promo_date = "promo_date";

	public static string promo_date2 = "promo_date2";

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	public static string NodePrefab()
	{
		return "Prefabs/Play/Node";
	}

	public static string LightTilePrefab()
	{
		return "Prefabs/Play/TileLayer/LightTile";
	}

	public static string DarkTilePrefab()
	{
		return "Prefabs/Play/TileLayer/DarkTile";
	}

	public static string NoneTilePrefab()
	{
		return "Prefabs/Play/TileLayer/NoneTile";
	}

	public static string TileBorderTop()
	{
		return "Prefabs/Play/TileLayer/TopBorder/";
	}

	public static string TileBorderBottom()
	{
		return "Prefabs/Play/TileLayer/BottomBorder/";
	}

	public static string TileBorderLeft()
	{
		return "Prefabs/Play/TileLayer/LeftBorder/";
	}

	public static string TileBorderRight()
	{
		return "Prefabs/Play/TileLayer/RightBorder/";
	}

	public static string Item1()
	{
		return "Prefabs/items/bluebox";
	}

	public static string Item1Bomb()
	{
		return "Prefabs/items/blue_bomb";
	}

	public static string Item1Column()
	{
		return "Prefabs/items/blue_column";
	}

	public static string Item1Row()
	{
		return "Prefabs/items/blue_row";
	}

	public static string Item1Cross()
	{
		return "Prefabs/items/blue_cross";
	}

	public static string Item2()
	{
		return "Prefabs/items/greenbox";
	}

	public static string Item2Bomb()
	{
		return "Prefabs/items/green_bomb";
	}

	public static string Item2Column()
	{
		return "Prefabs/items/green_column";
	}

	public static string Item2Row()
	{
		return "Prefabs/items/green_row";
	}

	public static string Item2Cross()
	{
		return "Prefabs/items/green_cross";
	}

	public static string Item3()
	{
		return "Prefabs/items/orangebox";
	}

	public static string Item3Bomb()
	{
		return "Prefabs/items/orange_bomb";
	}

	public static string Item3Column()
	{
		return "Prefabs/items/orange_column";
	}

	public static string Item3Row()
	{
		return "Prefabs/items/orange_row";
	}

	public static string Item3Cross()
	{
		return "Prefabs/items/orange_cross";
	}

	public static string Item4()
	{
		return "Prefabs/items/purplebox";
	}

	public static string Item4Bomb()
	{
		return "Prefabs/items/purple_bomb";
	}

	public static string Item4Column()
	{
		return "Prefabs/items/purple_column";
	}

	public static string Item4Row()
	{
		return "Prefabs/items/purple_row";
	}

	public static string Item4Cross()
	{
		return "Prefabs/items/purple_cross";
	}

	public static string Item5()
	{
		return "Prefabs/items/redbox";
	}

	public static string Item5Bomb()
	{
		return "Prefabs/items/red_bomb";
	}

	public static string Item5Column()
	{
		return "Prefabs/items/red_column";
	}

	public static string Item5Row()
	{
		return "Prefabs/items/red_row";
	}

	public static string Item5Cross()
	{
		return "Prefabs/items/red_cross";
	}

	public static string Item6()
	{
		return "Prefabs/items/yellowbox";
	}

	public static string Item6Bomb()
	{
		return "Prefabs/items/yellow_bomb";
	}

	public static string Item6Column()
	{
		return "Prefabs/items/yellow_column";
	}

	public static string Item6Row()
	{
		return "Prefabs/items/yellow_row";
	}

	public static string Item6Cross()
	{
		return "Prefabs/items/yellow_cross";
	}

	public static string ItemColorCone()
	{
		return "Prefabs/items/colorcone";
	}

	public static string Breakable()
	{
		return "Prefabs/Collectable Items/breakable";
	}

	public static string Rocket1()
	{
		return "Prefabs/Collectable Items/rocket_1";
	}

	public static string Rocket2()
	{
		return "Prefabs/Collectable Items/rocket_2";
	}

	public static string Rocket3()
	{
		return "Prefabs/Collectable Items/rocket_3";
	}

	public static string Rocket4()
	{
		return "Prefabs/Collectable Items/rocket_4";
	}

	public static string Rocket5()
	{
		return "Prefabs/Collectable Items/rocket_5";
	}

	public static string Rocket6()
	{
		return "Prefabs/Collectable Items/rocket_6";
	}

	public static string RocketGeneric()
	{
		return "Prefabs/Collectable Items/rocket_generic";
	}

	public static string ToyMine1()
	{
		return "Prefabs/Collectable Items/toymine_1";
	}

	public static string ToyMine2()
	{
		return "Prefabs/Collectable Items/toymine_2";
	}

	public static string ToyMine3()
	{
		return "Prefabs/Collectable Items/toymine_3";
	}

	public static string ToyMine4()
	{
		return "Prefabs/Collectable Items/toymine_4";
	}

	public static string ToyMine5()
	{
		return "Prefabs/Collectable Items/toymine_5";
	}

	public static string ToyMine6()
	{
		return "Prefabs/Collectable Items/toymine_6";
	}

	public static string LegoBox1()
	{
		return "Prefabs/Collectable Items/lego_box_1";
	}

	public static string LegoBox2()
	{
		return "Prefabs/Collectable Items/lego_box_2";
	}

	public static string LegoBox3()
	{
		return "Prefabs/Collectable Items/lego_box_3";
	}

	public static string LegoBox4()
	{
		return "Prefabs/Collectable Items/lego_box_4";
	}

	public static string LegoBox5()
	{
		return "Prefabs/Collectable Items/lego_box_5";
	}

	public static string LegoBox6()
	{
		return "Prefabs/Collectable Items/lego_box_6";
	}

	public static string LegoBoxGeneric()
	{
		return "Prefabs/Collectable Items/lego_box_generic";
	}

	public static string Collectible1()
	{
		return "Prefabs/Collectable Items/toy_1";
	}

	public static string Collectible2()
	{
		return "Prefabs/Collectable Items/toy_2";
	}

	public static string Collectible3()
	{
		return "Prefabs/Collectable Items/toy_3";
	}

	public static string Collectible4()
	{
		return "Prefabs/Collectable Items/toy_4";
	}

	public static string Collectible5()
	{
		return "Prefabs/Collectable Items/toy_5";
	}

	public static string Collectible6()
	{
		return "Prefabs/Collectable Items/toy_6";
	}

	public static string Collectible7()
	{
		return "Prefabs/Collectable Items/toy_7";
	}

	public static string Collectible8()
	{
		return "Prefabs/Collectable Items/toy_8";
	}

	public static string Collectible9()
	{
		return "Prefabs/Collectable Items/toy_9";
	}

	public static string Collectible10()
	{
		return "Prefabs/Collectable Items/toy_10";
	}

	public static string Collectible11()
	{
		return "Prefabs/Collectable Items/toy_11";
	}

	public static string Collectible12()
	{
		return "Prefabs/Collectable Items/toy_12";
	}

	public static string Collectible13()
	{
		return "Prefabs/Collectable Items/toy_13";
	}

	public static string Collectible14()
	{
		return "Prefabs/Collectable Items/toy_14";
	}

	public static string Collectible15()
	{
		return "Prefabs/Collectable Items/toy_15";
	}

	public static string Collectible16()
	{
		return "Prefabs/Collectable Items/toy_16";
	}

	public static string Collectible17()
	{
		return "Prefabs/Collectable Items/toy_17";
	}

	public static string Collectible18()
	{
		return "Prefabs/Collectable Items/toy_18";
	}

	public static string Collectible19()
	{
		return "Prefabs/Collectable Items/toy_19";
	}

	public static string Collectible20()
	{
		return "Prefabs/Collectable Items/toy_20";
	}

	public static string CollectibleBox()
	{
		return "Prefabs/Collectable Items/toy_box";
	}

	public static string BlueBoxExplosion()
	{
        //return "Effects/Bluebox Explosion";		
       return "Effects/Bluebox Explosion";
    }

	public static string GreenBoxExplosion()
	{
        //return "Effects/Greenbox Explosion";
        return "Effects/Greenbox Explosion";
    }

	public static string OrangeCookieExplosion()
	{
        //return "Effects/Orangebox Explosion";
        return "Effects/OrangeBox Explosion";
    }

	public static string PurpleCookieExplosion()
	{
        //return "Effects/Purplebox Explosion";
        return "Effects/PurpleBox Explosion";
    }

    public static string RedCookieExplosion()
	{
        //return "Effects/Redbox Explosion";
        return "Effects/RedBox Explosion";
    }

	public static string YellowCookieExplosion()
	{
        //return "Effects/Yellowbox Explosion";
        return "Effects/YellowBox Explosion";
    }

	public static string BreakerExplosion1()
	{
		return "Effects/Blue Bomb Explosion";
	}

	public static string BreakerExplosion2()
	{
		return "Effects/Green Bomb Explosion";
	}

	public static string BreakerExplosion3()
	{
		return "Effects/Orange Bomb Explosion";
	}

	public static string BreakerExplosion4()
	{
		return "Effects/Purple Bomb Explosion";
	}

	public static string BreakerExplosion5()
	{
		return "Effects/Red Bomb Explosion";
	}

	public static string BreakerExplosion6()
	{
		return "Effects/Yellow Bomb Explosion";
	}

	public static string ColumnRowBreaker()
	{
		return "Prefabs/Collectable Items/column_row_breaker";
	}

	public static string GenericBombBreaker()
	{
		return "Prefabs/Collectable Items/generic_bomb";
	}

	public static string GenericXBreaker()
	{
		return "Prefabs/Collectable Items/generic_cross";
	}

	public static string RainbowExplosion()
	{
		return "Effects/GlowStars";
	}

	public static string RingExplosion()
	{
		return "Effects/Moves Ring";
	}

	public static string ColRowBreaker1()
	{
		return "Effects/Blue Striped";
	}

	public static string BreakableExplosion()
	{
		return "Effects/breakable Explosion";
	}

	public static string MineExplosion()
	{
		return "Effects/toymine Explosion";
	}

	public static string ColRowBreaker2()
	{
		return "Effects/Green Striped";
	}

	public static string ColRowBreaker3()
	{
		return "Effects/Orange Striped";
	}

	public static string ColRowBreaker4()
	{
		return "Effects/Purple Striped";
	}

	public static string ColRowBreaker5()
	{
		return "Effects/Red Striped";
	}

	public static string ColRowBreaker6()
	{
		return "Effects/Yellow Striped";
	}

	public static string BoosterActive()
	{
		return "Effects/Booster Active";
	}

	public static string ColumnBreakerAnimation1()
	{
        //return "Stripes/StripeAnim1";
        return "Stripes/StripeAnim_blue";
    }

	public static string ColumnBreakerAnimation2()
	{
        //return "Stripes/StripeAnim2";
        return "Stripes/StripeAnim_Green";
    }

	public static string ColumnBreakerAnimation3()
	{
        //return "Stripes/StripeAnim3";
        return "Stripes/StripeAnim_Orange";
    }

	public static string ColumnBreakerAnimation4()
	{
        //return "Stripes/StripeAnim4";
        return "Stripes/StripeAnim_Purple";
    }

	public static string ColumnBreakerAnimation5()
	{
        //return "Stripes/StripeAnim5";
        return "Stripes/StripeAnim_Red";
    }

	public static string ColumnBreakerAnimation6()
	{
        //return "Stripes/StripeAnim6";;
        return "Stripes/StripeAnim_Yellow";
    }

	public static string BigColumnBreakerAnimation1()
	{
		Debug.Log("animeBlue.");
		return "Stripes/BigStripeAnim_Blue";
	}

	public static string BigColumnBreakerAnimation2()
	{
        //return "Stripes/BigStripeAnim2";
        return "Stripes/BigStripeAnim_Green";
    }

	public static string BigColumnBreakerAnimation3()
	{
        //	return "Stripes/BigStripeAnim3";
        return "Stripes/BigStripeAnim_Orange";
    }

	public static string BigColumnBreakerAnimation4()
	{
        //return "Stripes/BigStripeAnim4";
        return "Stripes/BigStripeAnim_Purple";
    }

	public static string BigColumnBreakerAnimation5()
	{
        //return "Stripes/BigStripeAnim5";
        return "Stripes/BigStripeAnim_Red";
    }

	public static string BigColumnBreakerAnimation6()
	{
        //return "Stripes/BigStripeAnim6";
        return "Stripes/BigStripeAnim_Yellow";
    }

	public static string Waffle1()
	{
		return "Prefabs/Collectable Items/waffle_1";
	}

	public static string Waffle2()
	{
		return "Prefabs/Collectable Items/waffle_2";
	}

	public static string Waffle3()
	{
		return "Prefabs/Collectable Items/waffle_3";
	}

	public static string Lock1()
	{
		return "Prefabs/Collectable Items/Locked";
	}

	public static string Doll(string name)
	{
		return "Doll/" + name;
	}

	public static string StarGold()
	{
		return "Prefabs/Play/UI/StarGold";
	}

	public static string Mask()
	{
		return "Prefabs/Play/Mask";
	}

	public static string Level1Step1()
	{
		return "Prefabs/Play/Help/Level1Step1";
	}

	public static string Level1Step2()
	{
		return "Prefabs/Play/Help/Level1Step2";
	}

	public static string Level1Step3()
	{
		return "Prefabs/Play/Help/Level1Step3";
	}

	public static string Level2Step1()
	{
		return "Prefabs/Play/Help/Level2Step1";
	}

	public static string Level2Step2()
	{
		return "Prefabs/Play/Help/Level2Step2";
	}

	public static string Level2Step3()
	{
		return "Prefabs/Play/Help/Level2Step3";
	}

	public static string Level3Step1()
	{
		return "Prefabs/Play/Help/Level3Step1";
	}

	public static string Level3Step2()
	{
		return "Prefabs/Play/Help/Level3Step2";
	}

	public static string Level3Step3()
	{
		return "Prefabs/Play/Help/Level3Step3";
	}

	public static string Level3Step4()
	{
		return "Prefabs/Play/Help/Level3Step4";
	}

	public static string Level3Step5()
	{
		return "Prefabs/Play/Help/Level3Step5";
	}

	public static string Level4Step1()
	{
		return "Prefabs/Play/Help/Level4Step1";
	}

	public static string Level4Step2()
	{
		return "Prefabs/Play/Help/Level4Step2";
	}

	public static string Level4Step3()
	{
		return "Prefabs/Play/Help/Level4Step3";
	}

	public static string Level5Step1()
	{
		return "Prefabs/Play/Help/Level5Step1";
	}

	public static string Level5Step2()
	{
		return "Prefabs/Play/Help/Level5Step2";
	}

	public static string Level6Step1()
	{
		return "Prefabs/Play/Help/Level6Step1";
	}

	public static string Level7Step1()
	{
		return "Prefabs/Play/Help/Level7Step1";
	}

	public static string Level7Step2()
	{
		return "Prefabs/Play/Help/Level7Step2";
	}

	public static string Level9Step1()
	{
		return "Prefabs/Play/Help/Level9Step1";
	}

	public static string Level10BeginStep1()
	{
		return "Prefabs/Play/Help/Level10BeginStep1";
	}

	public static string Level12Step1()
	{
		return "Prefabs/Play/Help/Level12Step1";
	}

	public static string Level12Step2()
	{
		return "Prefabs/Play/Help/Level12Step2";
	}

	public static string Level13Step1()
	{
		return "Prefabs/Play/Help/Level13Step1";
	}

	public static string Level13Step2()
	{
		return "Prefabs/Play/Help/Level13Step2";
	}

	public static string Level15Step1()
	{
		return "Prefabs/Play/Help/Level15Step1";
	}

	public static string Level15Step2()
	{
		return "Prefabs/Play/Help/Level15Step2";
	}

	public static string Level16Step1()
	{
		return "Prefabs/Play/Help/Level16Step1";
	}

	public static string Level18Step1()
	{
		return "Prefabs/Play/Help/Level18Step1";
	}

	public static string Level18Step2()
	{
		return "Prefabs/Play/Help/Level18Step2";
	}

	public static string Level20BeginStep1()
	{
		return "Prefabs/Play/Help/Level20BeginStep1";
	}

	public static string Level23BeginStep1()
	{
		return "Prefabs/Play/Help/Level23BeginStep1";
	}

	public static string Level25Step1()
	{
		return "Prefabs/Play/Help/Level25Step1";
	}

	public static string Level25Step2()
	{
		return "Prefabs/Play/Help/Level25Step2";
	}

	public static string Level31Step1()
	{
		return "Prefabs/Play/Help/Level31Step1";
	}

	public static string Level61Step1()
	{
		return "Prefabs/Play/Help/Level61Step1";
	}

	public static string Level76Step1()
	{
		return "Prefabs/Play/Help/Level76Step1";
	}

	public static string LoadingImage()
	{
		return "Prefabs/Map/Loading Image";
	}

	public static string ProgressGoldStar()
	{
		return "Prefabs/Play/UI/StarGold";
	}

	public static string StarExplode()
	{
		return "Effects/StarExplode";
	}

	private void OnApplicationQuit()
	{
		SaveLifeInfo();
	}

	public void SaveLifeInfo()
	{
		PlayerPrefs.SetFloat(stringTimer, timer);
		PlayerPrefs.SetFloat(wheelTimer, timerwheel);
		PlayerPrefs.SetInt(stringLife, life);
		PlayerPrefs.SetString(exit_date_time, DateTime.Now.ToString());
		PlayerPrefs.Save();
	}

	public void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			SaveLifeInfo();
		}
		else if ((bool)GameObject.Find("LifeBar"))
		{
			instance.exitDateTime = PlayerPrefs.GetString(exit_date_time, default(DateTime).ToString());
			instance.exitDateTimeWheel = PlayerPrefs.GetString(exit_date_time_wheel, default(DateTime).ToString());
			instance.timer = PlayerPrefs.GetFloat(stringTimer, 0f);
			instance.timerwheel = PlayerPrefs.GetFloat(wheelTimer, 0f);
			instance.life = PlayerPrefs.GetInt(stringLife, instance.maxLife);
			GameObject.Find("LifeBar").GetComponent<Life>().runTimer = false;
		}
	}
}
