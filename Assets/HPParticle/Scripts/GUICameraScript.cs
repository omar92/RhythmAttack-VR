// GUI script to change the camera view in Demo

using UnityEngine;
using System.Collections;

public class GUICameraScript : MonoBehaviour {

	public CameraScript CS;

	void OnGUI()
	{
		GUI.Box ( new Rect (Screen.width - 150,10,140,70), "");

		GUI.Label(new Rect(Screen.width - 140, 20, 100, 25),"Camera");
		
		if (GUI.Button(new Rect(Screen.width - 140, 40, 50, 25), "<--"))
		{
			CS.CameraViewNum = CS.CameraViewNum - 1;
			
			if (CS.CameraViewNum == -1)
			{
				CS.CameraViewNum = CS.CameraViews.Count -1;
			}
		}
		
		if (GUI.Button(new Rect(Screen.width -90, 40, 50, 25), "-->"))
		{
			CS.CameraViewNum = CS.CameraViewNum + 1;
			
			if (CS.CameraViewNum == CS.CameraViews.Count)
			{
				CS.CameraViewNum = 0;
			}
		}
	}
}
