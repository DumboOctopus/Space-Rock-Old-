using UnityEngine;
using System.Collections;

public class New_YellowShotController : MonoBehaviour {

	private GameObject target;
	private GeneralShotScript movement;
	private DetectObjectScript[] child;
	private Animator anim;

	private float timer = 0.5f;

	public float turnRate = 30; //deg/sec
	public LayerMask whatIsTarget;

	private float dir = 0;

	void Start ()
	{
		target = null;
		movement = gameObject.GetComponent<GeneralShotScript> ();
		child = gameObject.GetComponentsInChildren<DetectObjectScript> ();
		anim = gameObject.GetComponent<Animator> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.layer == 10)
		{
			target = other.gameObject;
		}
	}
	

	void Update ()
	{
		if(child[0].InArea)
		{
			Destroy(this.gameObject);
		}

		if(target != null)
		{
			if(timer <= 0)
			{
				float angle = Mathf.Atan( (target.transform.position.y - this.transform.position.y)/(target.transform.position.x - this.transform.position.x));
				if(angle != 0) //so no multiplying by zero
				{
					angle *= Mathf.Rad2Deg;
				}

				if(target.transform.position.x < this.transform.position.x) //dealing with limited domain of atan
				{
					angle += 180;
				}

				if(angle < 0) //if the angle is negative, turn it positive
				{
					angle += 360;
				}

				if(angle - this.transform.eulerAngles.z > 0)
				{
					dir = 1;
					if(Mathf.Abs(angle - this.transform.eulerAngles.z) > 180)
					{
						dir = -1;
					}
				} else
				{
					dir = -1;
					
				}


				Vector3 tmp = this.transform.eulerAngles;
				if(Mathf.Abs ( tmp.z - angle) < turnRate*Time.deltaTime)
				{
					tmp.z = angle;
					dir = 0;
				}else
				{
					tmp.z += turnRate*Time.deltaTime*dir;
				}
				Debug.Log (tmp.z);

				transform.eulerAngles = tmp;
				movement.SetAngleAndSpeed();


			} else
			{
				timer -= Time.deltaTime;
			}


		} else
		{
			dir = 0;
		}


		anim.SetInteger ("Dir", Mathf.Abs ( (int)dir) );
	}

}
