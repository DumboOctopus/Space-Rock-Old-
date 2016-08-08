using UnityEngine;
using System.Collections;

public class CloseBossRoom : MonoBehaviour {

	public DetectObjectScript detect;
	public Vector2 change;
	public float time;

	private bool move;
	private bool stop = false;

	private float timer = 0;

	private Vector3 initPosition;
	
	// Update is called once per frame
	void Start ()
	{
		initPosition = transform.position;
	}

	void Update () {


		if(move && !stop)
		{

			Vector3 pos = initPosition;

			pos.x = Mathf.Lerp(initPosition.x, change.x + initPosition.x, timer/time);
			pos.y = Mathf.Lerp(initPosition.y, change.y + initPosition.y, timer/time);

			Debug.Log (move);

			transform.position = pos;

			timer += Time.deltaTime;
			if(timer >= time)
			{
				stop = true;
			}
		} else if(detect.InArea)
		{
			move = true;
		}
	}
}
