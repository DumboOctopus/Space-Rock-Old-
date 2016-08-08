using UnityEngine;
using System.Collections;

public class Grounded_Script : MonoBehaviour {

	public two_Player_Controller playerScript;

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.gameObject.layer != 11 && other.gameObject.layer != 8 && !other.isTrigger) // &&other.gameObject.layer != 10
		{
			playerScript.grounded = true;
		}

	}
}
