using UnityEngine;
using System.Collections;



public class BlasterFade : MonoBehaviour {
	
	//color
	public UnityEngine.Color c = new UnityEngine.Color(1, 1, 1, 1);


	//sprite
	SpriteRenderer rend;
	public Sprite GreenGlow, BlueGlow, LaserGlow, YellowGlow;

	//class instances
	public Blaster_controler controller; 


	//start
	void Start ()
	{
		rend = GetComponent<SpriteRenderer> ();
	}


	void Update ()
	{

		SetOpacity (controller.shotNumber);
		SetSprite (controller.shotNumber);


		transform.GetComponent<Renderer>().material.color = c;
	}

	void SetOpacity(int weapon_num)
	{
//		if(weapon_num == 0)
//		{
//			c.a = ((controller.ammo.blueAmmo)) / 2f + 0.5f;
//		} else if(weapon_num == 2)
//		{
//			c.a = ((controller.ammo.greenAmmo)) / 2f + 0.5f;
//		} else if(weapon_num == 1)
//		{
//			c.a = ( (controller.ammo.laserAmmo) ) / 2f + 0.5f;
//		}

		switch(weapon_num)
		{
			case 0:
				c.a = ((controller.ammo.blueAmmo)) / 2f + 0.5f;
				break;
			case 1:
				c.a = ( (controller.ammo.laserAmmo) ) / 2f + 0.5f;
				break;
			case 2:
				c.a = ((controller.ammo.greenAmmo)) / 2f + 0.5f;
				break;
			case 3:
				c.a = ((controller.ammo.yellowAmmo)) / 2f + 0.5f;
				break;
		}
	}

	void SetSprite(int weapon_num)
	{
		Vector2 pos = this.transform.localPosition;
//		if(weapon_num == 0)
//		{
//			pos.x = 0;
//			rend.sprite = BlueGlow;
//		} else if(weapon_num == 2)
//		{
//			pos.x = 0.6f;
//			rend.sprite = GreenGlow;
//		} else if(weapon_num == 1)
//		{
//			pos.x = 0;
//			rend.sprite = LaserGlow;
//		}

		switch(weapon_num)
		{
			case 0:
				pos.x = 0f;
				rend.sprite = BlueGlow;
				break;
			case 1:
				pos.x = 0f;
				rend.sprite = LaserGlow;
				break;
			case 2:
				pos.x = 0.6f;
				rend.sprite = GreenGlow;
				break;
			case 3:
				pos.x = 0f;
				rend.sprite = YellowGlow;
				break;
		}

		transform.localPosition = pos;
	}

}
