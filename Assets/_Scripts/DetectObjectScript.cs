using UnityEngine;
using System.Collections;

public class DetectObjectScript : MonoBehaviour {

	public bool InArea = false;

	public int layers;
	public string _tag;

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.layer == layers || other.gameObject.tag == _tag )
		{
			InArea = true;
		} 

	}

	void OnTriggerExit2D (Collider2D other)
	{
		if(other.gameObject.layer == layers || other.gameObject.tag == _tag)
		{
			InArea = false;
		} 
	}
}
