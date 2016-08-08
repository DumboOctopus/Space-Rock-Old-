using UnityEngine;
using System.Collections;

public class New_BlueShot_Contoller : MonoBehaviour {
	

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other != null && !other.CompareTag("PassiveToShot"))
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			Destroy (this.gameObject, 0.05f);
		}
	}
}
