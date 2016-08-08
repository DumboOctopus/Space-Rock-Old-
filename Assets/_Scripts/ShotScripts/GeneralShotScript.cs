using UnityEngine;
using System.Collections;

public class GeneralShotScript : MonoBehaviour {

	public float speed;
	public bool destroyOnTime = true;
	public float destroyTime;
	
	// Use this for initialization
	void Start () {
		//set angle and speed
		SetAngleAndSpeed ();
		if(destroyOnTime)
		{
			Destroy (this.gameObject, Mathf.Abs(destroyTime));
		}

	}

	// destorys if out of bounds
	void Update () {
		if (OutOfBounds ())
			Destroy (this.gameObject);
	}
	
	public void SetAngleAndSpeed ()
	{
		Vector2 angle = new Vector2 (Mathf.Cos (Mathf.Deg2Rad * transform.eulerAngles.z), Mathf.Sin (Mathf.Deg2Rad * transform.eulerAngles.z));
		GetComponent<Rigidbody2D>().velocity = speed * angle;
	}
	
	//checks if out of bounds ( above or below the veiw of camera
	public bool OutOfBounds ()
	{
		if (transform.position.y > 11 || transform.position.y < -2)
			return true;
		else
			return false;
	}
}
