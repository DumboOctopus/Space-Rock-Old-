using UnityEngine;
using System.Collections;

public class two_Player_Controller : MonoBehaviour {
	
	public float speed;
	public float jump_pow;

	private Animator anim;

	private float horizontal_speed;
	private float vertical_force;
	

	public float hp;
	private float hpMax;

	public GUIText hpText;

	//jumping
//	Vector2 feetpos;
//	public LayerMask QhatIsGround;
	public bool grounded;

	//temp invincablity from getting hit
	public bool Invincable = false;
	private float invincableTime;
	public float invincePeriod;

	public DetectObjectScript rightDetect;
	public DetectObjectScript leftDetect;


	// Use this for initialization
	void Start () 
	{
		anim = GetComponent<Animator> ();
		hpMax = hp;


	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{


//		feetpos.x = transform.position.x;
//		feetpos.y = transform.position.y - 1.1f;

//		grounded = CanJump ();
	


		Move (grounded);
		Animate_ ();

		if(hp <= 0)
		{
			hp = hpMax;
			this.gameObject.SetActive(false);
		}
		if (transform.position.y < -1.5) 
		{
			hp = hpMax;
			this.gameObject.SetActive(false);
		}

		hpText.text = hp.ToString();

		grounded = false;
	}

	void Update()
	{
		if(Invincable)
		{
			invincableTime += Time.deltaTime;
			if(invincableTime > invincePeriod)
			{
				Invincable = false;
			}
			if(invincableTime == 0)
			{
				this.gameObject.GetComponent<Renderer>().material.color = new UnityEngine.Color(1f, 1f, 1f, 1f);
			} else
			{
				this.gameObject.GetComponent<Renderer>().material.color = new UnityEngine.Color(1f, 1f, 1f, 0.25f * Mathf.Sin (2*Mathf.PI/invincePeriod * invincableTime * 3) + 0.75f);
			}
			
		} else
		{
			this.gameObject.GetComponent<Renderer>().material.color = new UnityEngine.Color(1f, 1f, 1f, 1f);

		}
	}

	void Move (bool touchingGround)
	{
		if(Input.GetAxis("Horizontal") != 0f)
		{
			horizontal_speed = speed * Input.GetAxis("Horizontal");
		} else
		{
			horizontal_speed = 0f;
		}


		if( (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) )&& touchingGround)
		{
			vertical_force = jump_pow;
		} else
		{
			vertical_force = 0f;
		}
		if(transform.position.x < Camera.main.transform.position.x - 7.5 || leftDetect.InArea)
		{
			if(horizontal_speed < 0)
			{
				horizontal_speed = 0;
			}
		}
		if(transform.position.x > Camera.main.transform.position.x + 7.5 || rightDetect.InArea)
		{
			if(horizontal_speed > 0)
			{
				horizontal_speed = 0;
			}
		}


		GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, vertical_force));
		GetComponent<Rigidbody2D>().velocity = new Vector2 (horizontal_speed, GetComponent<Rigidbody2D>().velocity.y);
	}

	void Animate_ ()
	{
		anim.SetFloat("Velocity", Input.GetAxis("Horizontal"));
		//left(xsize: 1, xcenter = -0.1) right(1, 1)
	}
	
		             
	void OnCollisionStay2D(Collision2D other) 
	{
		GetHurt (other.gameObject);
	}
	
//	void OnTriggerStay2D (Collider2D other)
//	{
	//	if(other.gameObject.layer == 11) // && !Invincable)
	//	{
	//		this.gameObject.SetActive(false);
	//	}
//	}

	void OnTriggerEnter2D (Collider2D other)
	{
		GetHurt (other.gameObject);
		if(other != null && other.gameObject.layer == 11) // && !Invincable)
		{
			this.gameObject.SetActive(false);
		}

	}

	void GetHurt (GameObject other)
	{
		if((other != null && other.gameObject.tag == "HurtsPlayer")|| other.gameObject.tag == "boss")
		{
			ShotData data = other.GetComponent<ShotData>();



			if(data != null)
			{
				if(!Invincable)
				{
					hp -= data.damage;
					Invincable = true;
					invincableTime = 0;
				}
			}
		}
		if(hp <= 0)
		{
			gameObject.SetActive(false);
		}
	}
}
