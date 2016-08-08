using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

	

	public int[] numbers;

	public GameObject[] things;

	public Vector2 offset;

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.layer == 8)
		{
			SpawnStuff();
			this.gameObject.SetActive(false);
		}
	}

	void SpawnStuff ()
	{
		Vector3 pos = this.transform.position;

		pos.y += offset.y;
		pos.x += offset.x;

		for(int i = 0; i < numbers.Length; i++)
		{
			for(int j = 0; j < numbers[i]; j++)
			{
				Instantiate(things[i], pos, Quaternion.identity);

			}
		}
	}
}
