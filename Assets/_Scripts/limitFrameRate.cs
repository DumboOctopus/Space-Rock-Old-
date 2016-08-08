using UnityEngine;
using System.Collections;

public class limitFrameRate : MonoBehaviour {

	void Awake() {
		Application.targetFrameRate = 50;
	}
	

}
