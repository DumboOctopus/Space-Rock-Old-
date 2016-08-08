using UnityEngine;
using System.Collections;

public class WalkerBossController : MonoBehaviour {

	public BossHealth Health; 

	private float stage = -2f; //set back to private
	private float maxHealth;
	private Animator anim;

	public DetectObjectScript right;
	public DetectObjectScript left;
	public DetectObjectScript playerDetect;
	private bool playerInArea = false;

	void Start()
	{
		maxHealth = Health.hp;
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

		anim.speed = Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x);  
		if(playerDetect.InArea)
		{
			playerInArea = true;
		}

		if(playerInArea)
		{
			stage = 3 * (1-Health.hp/maxHealth);
		}



		switch(Mathf.FloorToInt (stage))
		{
			case 0:
				Stage1();
				break;
			case 1:
				Stage2();
				break;
			case 2:
				Health.overrideHp = true;
				Stage3();
				break;	
		}	
	}

	float state = 0; //0 = walking around, 1 equals charging, 2 = upsidedown
	float timer = 5;
	int dir = -1;

	void Stage1 ()
	{
		switch(Mathf.FloorToInt(state))
		{
			case 0:
				if(timer <= 0)
				{
					timer = 3 + Random.Range (-2, 4);
					state = 1;
				} else
				{
					timer -= Time.deltaTime;
				}
				
				if(timer < 1)
				{
					state += Time.deltaTime;
				}
				Vector2 _force = new Vector2( (4f + 12*state) * Mathf.Sin ( (2 + state) * Time.time ), 0);
				GetComponent<Rigidbody2D>().AddForce(_force);
				break;
			case 1:
				_force = new Vector2( dir * 12, 0);
				GetComponent<Rigidbody2D>().AddForce(_force);
				
				if((left.InArea && dir == -1) || (right.InArea && dir == 1))
				{
					dir *= -1;
					state = 0;
					timer = 3 + Random.Range (-2, 4);
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				} 
				
				break;
		}
	}

	int wallHits = 0;

	void Stage2 ()
	{
		switch(Mathf.FloorToInt(state))
		{
		case 0:
			if(timer <= 0)
			{
				timer = 3 + Random.Range (-2, 4);
				state = 1;
			} else
			{
				timer -= Time.deltaTime;
			}
			
			if(timer < 1)
			{
				state += Time.deltaTime;
			}
			Vector2 _force = new Vector2( (4f + 12*state) * Mathf.Sin ( (2 + state) * Time.time ), 0);
			GetComponent<Rigidbody2D>().AddForce(_force);
			break;
		case 1:
			_force = new Vector2( dir * 12, 0);
			GetComponent<Rigidbody2D>().AddForce(_force);
			
			if((left.InArea && dir == -1) || (right.InArea && dir == 1))
			{
				dir *= -1;
				GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				wallHits ++;
				if(wallHits >= 2)
				{
					wallHits = 0;
					state = 0;
					timer = 3 + Random.Range (-2, 4);
				}
			} 
			
			break;
		}
	}

	void Stage3 ()
	{
		Vector2 _force;
		switch(Mathf.FloorToInt(state))
		{
			case 0:
				if(timer <= 0)
				{
					timer = 2 + Random.Range (-2, 4);
					state = 1;
				} else
				{
					timer -= Time.deltaTime;
				}
				
				if(timer < 1)
				{
					state += Time.deltaTime;
				}
				//causes oclation movement
//				Vector2 _force = new Vector2( (4f + 12*state) * Mathf.Sin ( (2 + state) * Time.time ), 0);
//				rigidbody2D.AddForce(_force);
				break;
			case 1:
				_force = new Vector2( dir * 12, 0);
				
				
				if((left.InArea && dir == -1) || (right.InArea && dir == 1))
				{
					dir *= -1;
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					wallHits ++;
					if(wallHits >= 2)
					{
						wallHits = 0;
						state = 0;
						timer = 2 + Random.Range (-2, 4);
					}
				} 
				if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
				{
					_force.y = 300;
				} else
				{
					_force.y = 0;
				}

				GetComponent<Rigidbody2D>().AddForce(_force);
				break;
		}
	}
}
