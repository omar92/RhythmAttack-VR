// This Script changes the player's HP when the player get's hit by a projectile

using UnityEngine;
using System.Collections;

public class PlayerCollisionScript : MonoBehaviour {


	public enum EnumChangeHPType{ Normal = 0, CustomColor = 1, CustomForce = 2, CustomColorandForce = 3, CustomText = 4};
	public EnumChangeHPType ChangeHPType = EnumChangeHPType.Normal;

	public Color CustomColor;
	public Vector3 CustomForce;
	public float CustomForceScatter;

	void OnCollisionEnter(Collision col) 
	{
		//if the gameobject that hits the character is a projectile
		if (col.gameObject.tag == "Projectile")
		{
			if (ChangeHPType == EnumChangeHPType.Normal)
			{
				gameObject.GetComponent<HPScript>().ChangeHP(col.gameObject.GetComponent<ProjectileScript>().Damage,col.contacts[0].point);
			}
			else if (ChangeHPType == EnumChangeHPType.CustomColor)
			{
				gameObject.GetComponent<HPScript>().ChangeHP(col.gameObject.GetComponent<ProjectileScript>().Damage,col.contacts[0].point,CustomColor);
			}
			else if (ChangeHPType == EnumChangeHPType.CustomForce)
			{
				gameObject.GetComponent<HPScript>().ChangeHP(col.gameObject.GetComponent<ProjectileScript>().Damage,col.contacts[0].point,CustomForce,CustomForceScatter);
			}
			else if (ChangeHPType == EnumChangeHPType.CustomColorandForce)
			{
				gameObject.GetComponent<HPScript>().ChangeHP(col.gameObject.GetComponent<ProjectileScript>().Damage,col.contacts[0].point,CustomForce,CustomForceScatter,CustomColor);
			}
			else if (ChangeHPType == EnumChangeHPType.CustomText)
			{
				gameObject.GetComponent<HPScript>().ChangeHP(col.gameObject.GetComponent<ProjectileScript>().Damage,col.contacts[0].point,"Custom Text");
            }
        }

		// I should note that these functions can be called from anyother script as well
		// for example in the ProjectileScript.cs these functions can be called within the OnCollisionEnter function

	}
	
}
