// This script controls the camera

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CameraScript : MonoBehaviour {

	public List<GameObject> CameraViews = new List<GameObject>(); //the GameObject that the camera should face

	public int CameraViewNum = 0;
	public float Speed = 1f; //the speed in which the cannon will rotate around the target
	public float RotateSpeed = 1f;
	

    void FixedUpdate() 
	{
		CameraViewNum = Mathf.Clamp(CameraViewNum,0,CameraViews.Count -1);

		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,CameraViews[CameraViewNum].transform.position, Speed * Time.deltaTime);
		gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation,CameraViews[CameraViewNum].transform.rotation, RotateSpeed * Time.deltaTime);
		                  
	}
}
