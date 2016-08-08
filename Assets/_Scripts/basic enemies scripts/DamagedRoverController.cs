using UnityEngine;
using System.Collections;

public class DamagedRoverController : MonoBehaviour {

	private Animator anim;
	public float speed = 5;

	//heal and respawn
	public int hp;

	float x_vel;

	//vars for move
	int direction = 1;



	//for stopping sparking
	bool IsSparking = false;
	bool SparkMem = false;
	float sparkClock = 0;
	
	
	//------------------------------------------------------------------------------//
	
	// Use this for initialization
	void Start () 
	{
		anim = GetComponent< Animator > ();
		anim.speed = 4;
	}
	
	//------------------------------------------------------------------------------//
	
	void FixedUpdate () 
	{

		if(OutOfBounds())
		{
			Destroy(this.gameObject);
		}
		
		
		//moving
		Move2 ();
		
		//keep animations in snyc with velocity;
		anim.SetFloat ("Velocity", direction*speed);

		
		//die
		if(hp <= 0)
		{
			Destroy(this.gameObject);
		}

		if(IsSparking)
		{
			sparkClock += Time.deltaTime;
			if(sparkClock > 0.5)
			{
				sparkClock = 0;
				IsSparking = false;
			}
		}

		if(IsSparking != SparkMem)
		{
			if(anim != null)
			{
				anim.SetBool("Spark",IsSparking);
			}
		}

		SparkMem = IsSparking;

	}
	
	//------------------------------------------------------------------------------//

	

	
	
	//------------------------------------------------------------------------------//

	void Move2 ()
	{
		Vector2 move;

		Vector2 positive_offset = new Vector2 (transform.position.x + 1f, transform.position.y);
		Vector2 nega_offset = new Vector2 (transform.position.x - 1f, transform.position.y);

//		if(Physics2D.Raycast(nega_offset, -Vector2.right, 0.2f))
//		{
//			direction = 1; 
//		} else
//		if(Physics2D.Raycast(positive_offset, Vector2.right, 0.2f))
//		{
//			direction = -1;
//		}

		Debug.DrawRay (nega_offset, -Vector2.right, UnityEngine.Color.white);
		Debug.DrawRay (positive_offset, Vector2.right, UnityEngine.Color.white);

		move = new Vector2 ( speed * direction + x_vel,  GetComponent<Rigidbody2D>().velocity.y);

		DecayVelocity ();

		print (direction);

		GetComponent<Rigidbody2D>().velocity = move;

	}

	void DecayVelocity ()
	{
		if(x_vel > 0)
		{
			x_vel -= Time.deltaTime * 5;
		}
		if(x_vel < 0)
		{
			x_vel += Time.deltaTime * 5;
		}
	}
	
	//------------------------------------------------------------------------------//
	
	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Shot")//&& other.gameObject.name != "GreenShot(Clone)"
		{
			ShotData s = other.GetComponent<ShotData>();

			hp -= s.damage;
			
//			x_vel = 4 * (transform.position.x - other.transform.position.x);
			x_vel = s.damage/4f + this.GetComponent<Rigidbody2D>().velocity.x/2;


			IsSparking = true;
		}
		if(other.gameObject.layer == 11)
		{
			Destroy(this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.gameObject.layer == 8)
		{
			hp -= 1;
			x_vel = 2 * (transform.position.x - other.transform.position.x);

			IsSparking = true;
		}
		if(other.gameObject.layer == 0 && other.gameObject.layer != 9)
		{
			direction *= -1;
		}

	}

	//------------------------------------------------------------------------------//
	
	bool OutOfBounds ()
	{
		if (transform.position.y < -2)
			return true;
		else
			return false;
	}

	//----------------------------------------------------------------------------//


}
