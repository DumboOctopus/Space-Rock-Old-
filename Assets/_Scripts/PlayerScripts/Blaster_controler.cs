using UnityEngine;
using System.Collections;


[System.Serializable]
public class Ammo 
{
	public float greenAmmo, blueAmmo, laserAmmo, yellowAmmo;
}

[System.Serializable]
public class Shots
{
	//shots
	public GameObject blueShot;
	public GameObject greenShot;
	public GameObject laser;
	public GameObject yellow;
}


[System.Serializable]
public class Blasters
{
	public Sprite blueBlaster, greenBlaster, laserBlaster, yellowBlaster;
}

public class Blaster_controler : MonoBehaviour {
	
	public int shotNumber;
	public int maxShotnumber;
	public Ammo ammo;
	private float blueAmmo, greenAmmo, laserAmmo, yellowAmmo;
	private float clock;

	//changing shot fired
	private float shotClock = 0;
	public float shotChangeSpeed = 0.3f;

	//shooting
	public float FireRate;
	public Transform shotSpawn;
	private float FireClock;

	//shots
	public Shots shots;

	//shot recharge rates
	public float greenRefuel, blueRefuel, laserRefuel, yellowRefuel;

	//blasters
	public Blasters blasters;
	private SpriteRenderer rend;
	public two_Player_Controller playerScript;

	void Start ()
	{
		rend = GetComponent<SpriteRenderer> ();
		blueAmmo = ammo.blueAmmo;
		greenAmmo = ammo.greenAmmo;
		laserAmmo = ammo.laserAmmo;
		yellowAmmo = ammo.yellowAmmo;
	}


	// Update is called once per frame
	void Update () {
		SetAngle2 ();

		if(Input.GetKey(KeyCode.LeftShift))
		{
			SetShot ();
			SetBlaster ();
		}


		if(Time.time > FireClock + 0.01f)
		{
			Refuel();
		}


		if(AllowedtoFire())
		{
			switch (shotNumber)
			{
				case 0:
					if(ammo.blueAmmo >= 1f)
					{
						FireBlue();
					}
					break;
				case 1:
					if(ammo.laserAmmo >= 1f)
					{
						FireLaser();
					}
					break;
				case 2:
					if(ammo.greenAmmo >= 1f )
					{
						FireGreen();
					}
					break;
				case 3:
					if(ammo.yellowAmmo >= 1f )
					{
						FireYellow();
					}
					break;
			}

		}
	}

	//handmade functions//


	//functions for poiting at mouse;
	float Slope (float y_1, float y_2, float x_1, float x_2)
	{
		float slope = (y_1 - y_2) / (x_1 - x_2);
		
		return slope;
	}

	//-------------------------------------------------------------//

//	void SetAngle ()
//	{
//		float foo = 0;
//		
//		if ((2 * Input.mousePosition.x / Screen.width - 1f) < 0)
//			foo = 180;
//		else
//			foo = 0;
//
//		float z_rot = Mathf.Atan ( 
//		    Slope (
//			    2*(transform.position.y + 0.4f)/10 - 1f, 		//so it has a range of -1 to 1 and = -1 when -4 (min on cam) = 1 when 6 (max on cam)
//				2 * Input.mousePosition.y/Screen.height - 1f, 	//so it has a range of -1 to 1
//				0, 												//this doesn't change (in the mouse's perspective)
//				2 * Input.mousePosition.x/Screen.width - 1f 	//so it has a range of -1 to 1 
//			)
//		) * Mathf.Rad2Deg + foo;
//
//		transform.eulerAngles = new Vector3 (0, 0, z_rot);
//	}

	//-------------------------------------------------------------------------//

	void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.gameObject.layer == 10)
		{
			playerScript.hp -= 1;
			
		}
	}

	void SetAngle2 ()
	{
		Vector3 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition);

		float foo = 0;
		
		if (mouse.x < transform.position.x)
			foo = 180;
		else
			foo = 0;

		float z_rot = foo + Mathf.Rad2Deg * Mathf.Atan
		(
			Slope 
			(
				mouse.y,
				transform.position.y,
				mouse.x,
				transform.position.x
			)
		);

		transform.eulerAngles = new Vector3 (0, 0, z_rot);
	}

	//-------------------------------------------------------------//

	void SetShot ()
	{
		if(Input.GetKey(KeyCode.LeftShift) && shotClock < Time.time)
		{
			if(Input.GetKey(KeyCode.D) && shotNumber < maxShotnumber)
			{
				shotNumber += 1;
				shotClock = Time.time + shotChangeSpeed;
			}
			
			if(Input.GetKey(KeyCode.A) && shotNumber > 0.5f)
			{
				shotNumber -= 1;
				shotClock = Time.time + shotChangeSpeed;
			}
		}
	}

	//-------------------------------------------------------------//

	void SetBlaster ()
	{

		switch(shotNumber)
		{
			case 0:
				rend.sprite = blasters.blueBlaster;
				break;
			case 1:
				rend.sprite = blasters.laserBlaster;
				break;
			case 2:
				rend.sprite = blasters.greenBlaster;
				break;
			case 3:
				rend.sprite = blasters.yellowBlaster;
				break;
		}


	}

	//-------------------------------------------------------------//
	void Refuel ()
	{
		if(ammo.greenAmmo < greenAmmo)
		{
			ammo.greenAmmo += Time.deltaTime*greenRefuel/10f;
		}
		if(ammo.blueAmmo < blueAmmo)
		{
			ammo.blueAmmo += Time.deltaTime*blueRefuel/10f;
		}
		if(ammo.laserAmmo < laserAmmo)
		{
			ammo.laserAmmo += Time.deltaTime*laserRefuel/10f;
		}
		if(ammo.yellowAmmo < yellowAmmo)
		{
			ammo.yellowAmmo += Time.deltaTime*yellowRefuel/10f;
		}

		//if it is over
		if(ammo.greenAmmo > greenAmmo)
		{
			ammo.greenAmmo = greenAmmo;
		}
		if(ammo.blueAmmo > blueAmmo)
		{
			ammo.blueAmmo = blueAmmo;
		}
		if(ammo.laserAmmo > laserAmmo)
		{
			ammo.laserAmmo = laserAmmo;
		}
		if(ammo.yellowAmmo > yellowAmmo)
		{
			ammo.yellowAmmo = yellowAmmo;
		}

	}

	//-------------------------------------------------------------//

	//firing functions

	bool AllowedtoFire ()
	{
		bool foo = Input.GetMouseButton (0) && Time.time > FireClock;
		
		return foo;
	}

	void FireBlue () 
	{
		FireClock = Time.time + FireRate;
		
		Instantiate(shots.blueShot, shotSpawn.position, this.transform.rotation);
		
		ammo.blueAmmo -= 1;
		
	}
	
	void FireGreen()
	{
		FireClock = Time.time + FireRate;

		Instantiate(shots.greenShot, shotSpawn.position, this.transform.rotation);
		
		ammo.greenAmmo -= 1;
	}
	
	void FireLaser()
	{
		FireClock = Time.time + FireRate;
		
		Instantiate(shots.laser, shotSpawn.position, this.transform.rotation);
		
		ammo.laserAmmo -= 1;
	}

	void FireYellow()
	{
		FireClock = Time.time + FireRate;
		
		Instantiate(shots.yellow, shotSpawn.position, this.transform.rotation);

		ammo.yellowAmmo -= 1;
	}
}
