using UnityEngine;
using System.Collections;

public class BackToMainMenu : MonoBehaviour {

	public GameObject boss;
	private float timer = 3f;
	/**
	 * This script deals with going back to the main menu from a level
	 * this occurs when the back to main menu button is pressed or when one defeats the boss of any 
	 * level except the last level (10)
	 */
	

	// Update is called once per frame
	void Update () {

		if(!boss.activeSelf)
		{
			timer -= Time.deltaTime;
			if(timer <= 0)
			{
				Application.LoadLevel(0);
			}

		}
	}

	void OnGUI ()
	{
		if(GUI.Button(new Rect(385, 10, 45, 20), "Menu"))
		{
			Application.LoadLevel(0);
		}
	}
}
