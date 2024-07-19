using UnityEngine;

public class newToyspopup : MonoBehaviour
{
	private SpriteRenderer current_Bgsprite;

	public Sprite[] levels_Bgsprite;

	private void Start()
	{
		current_Bgsprite = GetComponent<SpriteRenderer>();
		changeBGSprite();
	}

	private void changeBGSprite()
	{
		int gift = Configuration.instance.gift1;
		current_Bgsprite.sprite = levels_Bgsprite[gift];
	}

	public void zipla()
	{
		AudioManager.instance.DropAudio();
	}
}
