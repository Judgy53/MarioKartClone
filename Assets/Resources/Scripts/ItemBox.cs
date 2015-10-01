using UnityEngine;
using System.Collections;

public class ItemBox : MonoBehaviour {

	[SerializeField]
	protected float RotSpeed = 20f;

	[SerializeField]
	protected float MovingMaxHeight = 1.5f;
	[SerializeField]
	protected float MovingMinHeight = 0f;
	[SerializeField]
	protected float MovingSpeed = 0.5f;

	protected float OrigHeight;

	protected bool MovingUp = true;

	protected float ColorCursor = 0f;
	protected float ColorDir = 1f;

	protected virtual void Start () {
		Setup ();
	}

	protected virtual void Update () {

		transform.Rotate (0f, RotSpeed * Time.deltaTime, 0f);

		Vector3 pos = transform.position;
		if (MovingUp) 
			pos.y = Mathf.MoveTowards(pos.y, OrigHeight + MovingMaxHeight, MovingSpeed * Time.deltaTime);
		else
			pos.y = Mathf.MoveTowards(pos.y, OrigHeight + MovingMinHeight, MovingSpeed * Time.deltaTime);

		transform.position = pos;

		if (pos.y >= OrigHeight + MovingMaxHeight || pos.y <= OrigHeight + MovingMinHeight) 
		{
			MovingUp = !MovingUp;
		}

		Color c = GetColor ();
		c.b = (128f + ColorCursor) / 256f;
		c.g = (256f - ColorCursor) / 256f;

		ColorCursor += ColorDir;

		if (ColorCursor >= 128f || ColorCursor <= 0f) 
			ColorDir *= -1f;

		SetColor(c);
	}

	protected void Setup() // Separate from Start to allow delayed init
	{
		OrigHeight = transform.position.y;
		ColorCursor = Random.Range (0, 128);
		ColorDir = Random.Range (0, 2);
		
		MovingUp = Random.Range (0, 2) == 1 ? true : false;
		
		transform.Rotate(Random.value * 45f, Random.value * 360.0f, Random.value * 45f);
		transform.Translate(0f, Random.Range (MovingMinHeight, MovingMaxHeight), 0f);
	}
	
	protected void Explode()
	{
		GameObject prefabExplode = Resources.Load ("Prefabs/ItemBoxExplode") as GameObject;

		GameObject GaO = Instantiate (prefabExplode) as GameObject;
		GaO.transform.position = transform.position;
		GaO.transform.rotation = Quaternion.identity;

		ItemBoxExplode box = GaO.GetComponent<ItemBoxExplode> ();

		box.SetColor (GetComponent<Renderer> ().material.color);
	}

	public void SetColor(Color color)
	{
		GetComponent<Renderer> ().material.color = color;
	}

	public Color GetColor()
	{
		return GetComponent<Renderer> ().material.color;
	}
}
