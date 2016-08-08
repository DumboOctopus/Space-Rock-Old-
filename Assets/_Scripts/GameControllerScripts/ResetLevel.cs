using UnityEngine;
using System.Collections;

public class ResetLevel : MonoBehaviour {


	public GameObject player;
	private float timer = 0;
	

	// Update is called once per frame
	void Update () {
		if(!player.activeSelf)
		{
			timer += Time.deltaTime;
			if(timer > 3)
			{
				Application.LoadLevel(Application.loadedLevel);
			}

		}
	}
}
