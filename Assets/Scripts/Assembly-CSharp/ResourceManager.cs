using System.Collections;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
	public MyTexture[] ReplacementTextures;

	private void Awake()
	{
		StartCoroutine("ReplaceTextures");
	}

	private IEnumerator ReplaceTextures()
	{
		string prefix = string.Empty;
		if (Screen.height < 700)
		{
			prefix = "360p/";
		}
		for (int i = 0; i < ReplacementTextures.Length; i++)
		{
			ResourceRequest request = Resources.LoadAsync<TextAsset>(prefix + ReplacementTextures[i].TextAssetPath);
			yield return request;
			if (request != null)
			{
				yield return ReplacementTextures[i].TextureRef.LoadImage((request.asset as TextAsset).bytes);
			}
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
}
