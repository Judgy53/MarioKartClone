using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemDisplay : MonoBehaviour {

	private Player player = null;
	private Image img = null;

	private Sprite[] sprites;

	// Use this for initialization
	void Start () {
		img = GetComponent<Image> ();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

		sprites = Resources.LoadAll<Sprite> ("Sprites/Items");
	}

	
	// Update is called once per frame
	void Update () {
		if (player.CurrentItem == null)
			img.enabled = false;
		else 
		{
			img.enabled = true;

			SpriteIndex spriteIndex = player.CurrentItem.GetSpriteIndex();
			img.sprite = sprites [(int)spriteIndex];
		}
	}
}

public enum SpriteIndex : int
{
	Banana = 0,
	BobOmb,
	DoubleMushroom,
	GreenShell,
	Mushroom,
	RedShell,
	Star,
	TrappedCube,
	TripleBanana,
	TripleGreenShell,
	TripleMushroom,
	TripleRedShell
}