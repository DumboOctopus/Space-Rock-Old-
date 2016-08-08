using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour {

	public float hp;
	public float knockBack;

	private float hitFlashTimer = 0;
	public float hitPeriod;
	public int flashTimes;

	public bool overrideHp = false;

	void OnTriggerEnter2D (Collider2D other)	
	{
		if(other.gameObject != null && other.gameObject.CompareTag("Shot") && !overrideHp)
		{
			hitFlashTimer = hitPeriod;

			hp --;

			if(hp <= 0)
			{
				this.gameObject.SetActive(false);
			}

			Vector2 dir = other.gameObject.transform.position - transform.position;
			dir.Normalize();

			dir *= knockBack;
			this.gameObject.GetComponent<Rigidbody2D>().AddForce(dir);


		}
	}



	void Update ()
	{
		if(hitFlashTimer < 0)
		{
			gameObject.GetComponent<Renderer>().material.color = new UnityEngine.Color(1f, 1f, 1f, 1f);
		} else
		{
			hitFlashTimer -= Time.deltaTime;
			gameObject.GetComponent<Renderer>().material.color = new UnityEngine.Color(1f, 1f, 1f, 0.25f * Mathf.Cos(2*Mathf.PI/hitPeriod * hitFlashTimer * flashTimes) + 0.75f);
		}
	}
}
