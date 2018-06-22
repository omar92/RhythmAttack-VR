/*
this script is attached to ammo, and stores data to be used by the PickUpItemScript
*/

using UnityEngine;
using System.Collections;

public class AlwaysFace : MonoBehaviour {


	public GameObject Target;
	public float Speed;

	public bool JustOnStart = false;

	// turn towards target
	void Start() 
	{
		if (Target == null)
		{
			return;
		}

		Vector3 dir = Target.transform.position - transform.position;
		Quaternion Rotation = Quaternion.LookRotation(dir);
		
		gameObject.transform.rotation = Rotation;
	}
	
	// turn towards target
	void FixedUpdate () 
	{
		if (Target == null)
		{
			return;
		}

		if (JustOnStart == false)
		{
			Vector3 dir = Target.transform.position - transform.position;
			Quaternion Rotation = Quaternion.LookRotation(dir);

			gameObject.transform.rotation = Quaternion.Lerp (gameObject.transform.rotation,Rotation,Speed * Time.deltaTime);
		}
	}
}
