using UnityEngine;
using System.Collections;

public class Turret_Controller : MonoBehaviour {

	//aiming
	private bool bCanSee; //if the turret can see the player
	private GameObject player; //the player gameObject reference
	public GameObject arm; //the arm of the turret
	public float turnRate; //deg / second
	public LayerMask whatToSee; //what the turret sees. Anything other than the player is considered not see through
	private bool aimed = false; //if the turret has aimed yet

	//shoting
	public GameObject shot; //the shot the current instance is using
	public float shotRate; // how fast the turret is firing (measured deg/sec)
	public Transform shotSpawn; //the point were the shot is spawns 

	//health stuff
	public int hp; //hit points of the turret

	//dying
	public GameObject body; //the body of the turret

	void Start()
	{
		player = GameObject.Find ("Player"); //getting the player gameObject
	}

	// Update is called once per frame
	void FixedUpdate () {
		bCanSee = CanSee (); //updating bCanSee


		//if the turret can see the player, it will start aiming at the player
		if(bCanSee)
		{
			Aim();
		}
		//the aim() will set aimed to true if it has aimed
		if(aimed)
		{
			Shot();
		}

		aimed = false; //reset aimed
	}

	//dealling with aiming
	bool CanSee () 
	{
		//dir is the vector that represents the direction to the player
		Vector2 dir = player.transform.position - this.transform.position;
		dir.Normalize ();


		//see what lies between the turret and player
		RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir, 10f, whatToSee);



		if(hit.collider != null)
		{
			if(hit.collider.gameObject.layer == 8) //if the object is in the player layer, then can see
			{
				return true;
			}
		}


		return false;
	} //raycast, if hit player, then can aim
	
	void Aim ()
	{
		float target;
		int dir;

		Vector2 tmp = player.transform.position - this.transform.position;

		target = Mathf.Atan (tmp.y / tmp.x);
		if(target != 0)
		{
			target *= Mathf.Rad2Deg;
		}

		if(player.transform.position.x < this.transform.position.x)
		{
			target += 180;
		}
		if(target < 0)
		{
			target += 360;
		}
		Debug.Log (target);


		if(target - arm.transform.eulerAngles.z > 0)
		{
			dir = 1;
			if(Mathf.Abs(target - arm.transform.eulerAngles.z) > 180)
			{
				dir = -1;
			}
		} else
		{
			dir = -1;

		}

		//increment angle
		Vector3 angle = arm.transform.eulerAngles;

		angle.z += dir * Time.deltaTime * turnRate;


		//check if done aiming
		if(Mathf.Abs(angle.z - target) < (Time.deltaTime * turnRate) )
		{
			angle.z = target;
			aimed = true;
		} 

		arm.transform.eulerAngles = angle;
	}


	//shoting

	float timer = 0;

	void Shot ()
	{
		timer += Time.deltaTime;

		if(timer > shotRate)
		{
			timer = 0;
			Instantiate(shot, shotSpawn.transform.position, arm.transform.rotation);
		}
	}



	//dealling with hitting
	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("asdf");
		if(other.gameObject.tag == "Shot")//&& other.gameObject.name != "GreenShot(Clone)"
		{
			ShotData s = other.gameObject.GetComponent<ShotData>();

			if(s != null)
			{
				Debug.Log ("exists");
			}
			hp -= s.damage;
		}
		if(hp <= 0)
		{
			Destroy(arm, 0.05f);
			Destroy (body, 0.05f);
			Destroy (this.GetComponent<Collider2D>(), 0.05f);
			Destroy(this, 0.05f);
		}
	}
}
