using UnityEngine;
using System.Collections;

public class GreenBossController : MonoBehaviour {

//	public GUIText init;
	public Transform PlayerPos;
	public int hp;

	private Animator anim;

	private int phase = 0;


	public int speed = 5;

	int mode = 0; //0 = idle, 1 = attack left,  3 = hit, 4 = missed
	float time = 0;
	float lim = 6;

	float t = 0f;



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(t < 3.14)
		{
//			init.color = new UnityEngine.Color(0, 0, 0, 1);
			if(PlayerPos.position.x > 100)
			{
				t = 5;
				time = Time.time;
			}
		} else
		{
			anim.speed = Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x);
			Move ();
		}

	}

//	UnityEngine.Color FadeInAndOutText ()
//	{
//		if (PlayerPos.position.x > 100) {
//
////			UnityEngine.Color textCol = init.color;
////			textCol.a = Mathf.Sin (t);
//					
//			if(t > 1.5 && t < 2)
//			{
//				t += 0.05f;
//			} else
//			{
//				t += Time.deltaTime/2;
//			}
//				
//			if(t > 3.14)
//			{
//				t = 3.14f;
//			}
//				
//			return textCol;
//		} else{
//			return new UnityEngine.Color(0, 0, 0, 0);
//		}
//
//	}



	void Move ()
	{
		time += Time.deltaTime;
		if (phase == 0) {

//			transform.position = new Vector3(speed * Mathf.Sin (3*time) * Mathf.Cos(time/2) + 112, transform.position.y, 0);
//			print(12);

			if(mode == 0)
			{
				transform.position = new Vector3(
					transform.position.x + Mathf.Sin (4.45f*time)/10f, 
					transform.position.y, 
					transform.position.z
				);

				anim.speed = 2;

				if(time > lim)
				{
					lim = Random.Range(5, 7);
					time = 0;
					if(transform.position.x > 106)
					{
						mode = 1;
					} else
					{
						mode = 2;
					}
				}
			} else
			if(mode == 1)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
				if(transform.position.x < 98) //106 is mid
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
					mode = 0;
				}
			} else
			if( mode == 2)
			{
				GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
				if(transform.position.x > 112)
				{
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					mode = 0;
				}

			} else
			if(mode == 3)
			{
				if(transform.position.x > 112)
				{
					GetComponent<Rigidbody2D>().velocity = Vector2.zero;
					mode = 0;
				}
				if(transform.position.x < 98) //106 is mid
				{
					GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
					mode = 0;
				}
			}
		}

	}

	void OnTriggerEnter2D  (Collider2D other)
	{
		if(other.tag == "Shot")
		{
			hp --;
		} 
	}
	void OnCollisionEnter2D (Collision2D other)
	{
		if(other.gameObject.layer == 8)
		{
			mode = 3;
			GetComponent<Rigidbody2D>().velocity *= -1;
		}
	}
}
