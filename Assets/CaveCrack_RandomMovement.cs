using UnityEngine;
using System.Collections;

public class CaveCrack_RandomMovement : MonoBehaviour
{

		public Transform Cam;
		//the camera object
		public float width;
		//the width of the crack
		public Vector4 area;

		public float paralax;
		private float oldCameraX;

		// Update is called once per frame
		void Update ()
		{
				Vector3 tmp2 = this.transform.position;
				tmp2.x += paralax * (Cam.position.x - oldCameraX);
				this.transform.position = tmp2;
				if (Cam.position.x - (this.transform.position.x + width) > 5) {
						float randx = Random.Range (3f * area.x, 3f * (area.x + area.z)) / 3f;
						float randy = Random.Range (10f * area.y, 10f * (area.y + area.w)) / 10f;
			
						Vector3 tmp = this.transform.position;

						tmp.x += randx + width;
						tmp.y = randy;
						
						this.transform.position = tmp;

						this.transform.eulerAngles.Set (this.transform.eulerAngles.x, this.transform.eulerAngles.y, Mathf.Floor (Random.Range (0, 3)) * 90);
				}
				oldCameraX = Cam.position.x;
		}
}
