using UnityEngine;
using System.Collections;

public class New_GreenShot_Controller : MonoBehaviour {

//	public float speed;
	private bool exploded;
	private float ExplosionTime;
	private bool bExpanded = false;

	private Animator anim;
	private CircleCollider2D coll;
	
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		coll = GetComponent<CircleCollider2D> ();

	}

	// destorys if out of bounds
	void Update () {
		if (exploded)
		{
			anim.SetBool("Exploded", true);
		}
	}
	
	void FixedUpdate ()
	{
		if(exploded)
		{
			transform.localScale = new Vector3(1.5f, 1.5f, 1);
			coll.enabled = false;
			if(bExpanded)
			{
				Destroy(this.gameObject, 0.05f);
			}
			if(ExplosionTime > 0.4f)
			{
				coll.enabled = true;
				coll.radius = 1.17f;
				bExpanded = true;
			}
//			coll.radius += 0.025f;

			ExplosionTime += Time.deltaTime;

		}
		transform.rotation = Quaternion.identity;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other != null && !other.CompareTag("PassiveToShot"))
		{
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			transform.rotation = Quaternion.identity;
			exploded = true;

			Debug.Log (other.gameObject);
		}
	}


}

