using UnityEngine;
using System.Collections;

public class NextLevel_TMP : MonoBehaviour {

	public GameObject boss;
	private float timer = 0;
	
	// Update is called once per frame
	void Update () {
		if(!boss.activeSelf)
		{
			timer += Time.deltaTime;
			if(timer >= 1)
			{
				Application.LoadLevel(0);
			}
		}
	}
}
