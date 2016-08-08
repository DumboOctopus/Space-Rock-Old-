using UnityEngine;
using System.Collections;

public class New_LaserShot_Controller : MonoBehaviour {

	private float speed;
	private int bounces;
	public int bounceMax;
	
	private ShotData s;
	private int maxDam;

	public LayerMask whatIsReflective;
	public LayerMask Notitself;

	
	// Use this for initialization
	void Start () {
		s = GetComponent<ShotData> ();
		maxDam = s.damage;
		speed = gameObject.GetComponent<GeneralShotScript> ().speed;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other != null)
		{
			if(bounces < bounceMax && !other.CompareTag("PassiveToShot") && other.gameObject.layer != 11 && other.gameObject.layer != 10 && other.gameObject.layer != 8 && !other.CompareTag("boss"))
			{
				//--
				Vector2 dir = GetComponent<Rigidbody2D>().velocity;
				dir.Normalize();
				RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, Notitself);
//				Debug.Log (Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x) - 90);


				//---
				//set angle
				NewReflect(Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x) - 90);
				//set pos
//				transform.position = hit.point;

				SetVelocity(transform.eulerAngles.z);
				bounces += 1;
				s.damage = Mathf.CeilToInt (maxDam * bounces/bounceMax);
				if(s.damage <= 0)
				{
					s.damage = 1;
				}
			} else
			if (!other.CompareTag("PassiveToShot"))
			{
				
				Destroy (this.gameObject, 0.05f);
			}
		}

	}



	void NewReflect(float angle)
	{
		float z = transform.eulerAngles.z;

		Vector3 FinalAngle = transform.eulerAngles;

		FinalAngle.z = 2 * angle - z;


		transform.eulerAngles = FinalAngle;


	}

	bool RaycastReflective (Vector2 direction, Vector2 point_of_contact)
	{
		bool foo = Physics2D.Raycast (point_of_contact, direction, 0.5f, whatIsReflective);
		return foo;
	}

	void SetVelocity (float AngleInDeg)
	{
		Vector2 angle = new Vector2 (Mathf.Cos (Mathf.Deg2Rad * AngleInDeg), Mathf.Sin (Mathf.Deg2Rad * AngleInDeg));
		GetComponent<Rigidbody2D>().velocity = speed * angle;
	}
}
