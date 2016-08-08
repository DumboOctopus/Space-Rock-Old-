using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public bool canMove;
	public float endOfSroll;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(!player.activeSelf)
		{
			return;
		}

		if(player.transform.position.x > endOfSroll )
		{
			canMove = false;
			return;
		}

//		if(player.transform.position.x >= this.transform.position.x + 5)
//		{
//			Vector3 tmp = this.transform.position;
//			tmp.x += 5;
//			player.transform.position = tmp;
//		}


//		Vector3 pos = transform.position;
//		if(player.transform.position.x >= this.transform.position.x && canMove)
//			pos.x = player.transform.position.x;
//		transform.position = pos;

		Vector3 pos = transform.position;
		if(player.transform.position.x >= this.transform.position.x + 1f && canMove)
			pos.x = player.transform.position.x - 0.99f;
		else if(player.transform.position.x <= this.transform.position.x - 1f && canMove)
			pos.x = player.transform.position.x + 0.99f;
		transform.position = pos;


		//final check to make sure it hasn't been shoved back 
		if(transform.position.x < 0)
		{
			Vector3 tmp = transform.position;
			tmp.x = 0;
			transform.position = tmp;
		}
	}
}
