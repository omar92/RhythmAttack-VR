/*
Destorys a the GameObject it is attached to after time
*/

using UnityEngine;
using System.Collections;

public class DestroyAfter : MonoBehaviour {


	public float time = 5f;


	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject,time);
	}

}
