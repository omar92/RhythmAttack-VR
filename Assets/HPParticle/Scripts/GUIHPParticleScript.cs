// GUI script to change the camera view in Demo

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIHPParticleScript : MonoBehaviour {
	

	public GameObject HPP;
	public GameObject Player;

	public int ChangeHPType = 0;

	public string HPParticleTypeName = "Default Color & Force";

	public float CR;
	public float CG;
	public float CB;

	public Vector3 Force = new Vector3(0f,250f,0f);
	public float ForceScatter = 50f;

	public float FadeSpeed = 2.5f;
    
	void Start()
	{
		CR = 0.25f;
		CG = 1f;
		CB = 0.0f;
	}

	void OnGUI()
	{
		//draw the box
		GUI.Box ( new Rect (10,10,150,Screen.height -20), "");

		//make label
		GUI.Label(new Rect(20, 20, 200, 25),"HP Particle Settings");

		//make label
		GUI.Label(new Rect(20, 60, 200, 25),"FadeSpeed");

		//set the FadeSpeed of the HP Particles
		FadeSpeed = GUI.HorizontalSlider(new Rect(20, 80, 100, 25), FadeSpeed, 0.1F, 10F);
		HPP.GetComponent<HPParticleScript>().FadeSpeed = FadeSpeed;

		//set the way the HP Particle will be be displayed
		if (ChangeHPType == 0)
		{
			HPParticleTypeName = "Default Color & Force"; //standard or default

			Player.GetComponent<PlayerCollisionScript>().ChangeHPType = PlayerCollisionScript.EnumChangeHPType.Normal;
		}
		else if (ChangeHPType == 1)
		{
			HPParticleTypeName = "Custom Color"; //with a Custom Color

			GUI.Label(new Rect(20, 180, 110, 25),"Color(RGB)");
			
			CR = GUI.HorizontalSlider(new Rect(20, 200, 100, 25), CR, 0.0F, 1F);
			CG = GUI.HorizontalSlider(new Rect(20, 220, 100, 25), CG, 0.0F, 1F);
			CB = GUI.HorizontalSlider(new Rect(20, 240, 100, 25), CB, 0.0F, 1F);

			Player.GetComponent<PlayerCollisionScript>().CustomColor.r = CR;
			Player.GetComponent<PlayerCollisionScript>().CustomColor.g = CG;
			Player.GetComponent<PlayerCollisionScript>().CustomColor.b = CB;

			Player.GetComponent<PlayerCollisionScript>().ChangeHPType = PlayerCollisionScript.EnumChangeHPType.CustomColor;

		}
		else if (ChangeHPType == 2)
		{
			HPParticleTypeName = "Custom Force";//with a Custom Force

			GUI.Label(new Rect(20, 180, 110, 25),"Force(xyz)");

			Force.x = GUI.HorizontalSlider(new Rect(20, 200, 100, 25), Force.x, -300F, 300F);
			Force.y = GUI.HorizontalSlider(new Rect(20, 220, 100, 25), Force.y, -300F, 300F);
			Force.z = GUI.HorizontalSlider(new Rect(20, 240, 100, 25), Force.z, -300F, 300F);

			GUI.Label(new Rect(20, 260, 110, 25),"Force Scatter");

			ForceScatter = GUI.HorizontalSlider(new Rect(20, 280, 100, 25), ForceScatter, 0.0F, 100F);

			Player.GetComponent<PlayerCollisionScript>().CustomForce = Force;
			Player.GetComponent<PlayerCollisionScript>().CustomForceScatter = ForceScatter;

			Player.GetComponent<PlayerCollisionScript>().ChangeHPType = PlayerCollisionScript.EnumChangeHPType.CustomForce;

        }
		else if (ChangeHPType == 3)
		{
			HPParticleTypeName = "Custom Color & Force"; //with custom color and force

			GUI.Label(new Rect(20, 180, 110, 25),"Color(RGB)");
			
			CR = GUI.HorizontalSlider(new Rect(20, 200, 100, 25), CR, 0.0F, 1F);
			CG = GUI.HorizontalSlider(new Rect(20, 220, 100, 25), CG, 0.0F, 1F);
			CB = GUI.HorizontalSlider(new Rect(20, 240, 100, 25), CB, 0.0F, 1F);
			
			Player.GetComponent<PlayerCollisionScript>().CustomColor.r = CR;
			Player.GetComponent<PlayerCollisionScript>().CustomColor.g = CG;
			Player.GetComponent<PlayerCollisionScript>().CustomColor.b = CB;

			GUI.Label(new Rect(20, 260, 110, 25),"Force(xyz)");
			
			Force.x = GUI.HorizontalSlider(new Rect(20, 280, 100, 25), Force.x, -300F, 300F);
			Force.y = GUI.HorizontalSlider(new Rect(20, 300, 100, 25), Force.y, -300F, 300F);
			Force.z = GUI.HorizontalSlider(new Rect(20, 320, 100, 25), Force.z, -300F, 300F);
			
			GUI.Label(new Rect(20, 340, 110, 25),"Force Scatter");
			
			ForceScatter = GUI.HorizontalSlider(new Rect(20, 360, 100, 25), ForceScatter, 0.0F, 100F);
			
			Player.GetComponent<PlayerCollisionScript>().CustomForce = Force;
			Player.GetComponent<PlayerCollisionScript>().CustomForceScatter = ForceScatter;

			Player.GetComponent<PlayerCollisionScript>().ChangeHPType = PlayerCollisionScript.EnumChangeHPType.CustomColorandForce;
        }

		//make buttons to change the way the particle is displayed
		GUI.Label(new Rect(20, 120, 200, 25),HPParticleTypeName);

		if (GUI.Button(new Rect(20, 140, 50, 25), "<--"))
		{
			ChangeHPType = ChangeHPType -1;

			if (ChangeHPType == -1)
			{
				ChangeHPType = 3;
			}

		}
		
		if (GUI.Button(new Rect(70, 140, 50, 25), "-->"))
		{
			ChangeHPType = ChangeHPType +1;
			
			if (ChangeHPType == 4)
			{
				ChangeHPType = 0 ;
            }

		}
	}
}
