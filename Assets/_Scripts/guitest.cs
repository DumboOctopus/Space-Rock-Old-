using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class guitest : MonoBehaviour
{

		public Vector2 p;
		public Vector2 s;


		void OnGUI ()
		{
		
				// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
				if (GUI.Button (new Rect (40, 40, 100, 100), "Level 1")) {
						SceneManager.LoadScene (1);
				}
		
				// Make the second button.
				if (GUI.Button (new Rect (165, 40, 100, 100), "Level 2")) {
						SceneManager.LoadScene (2);
				}

				if (GUI.Button (new Rect (290, 40, 100, 100), "Level 3")) {
						SceneManager.LoadScene (3);
				}
				if (GUI.Button (new Rect (415, 40, 100, 100), "Test Stuff")) {
						SceneManager.LoadScene (4);
				}
		}


}
