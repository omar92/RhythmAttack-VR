// GUI script to change the Projectile Settings in Demo

using UnityEngine;
using System.Collections;

public class GUIProjectileSettingsScript : MonoBehaviour {

	public GameObject Projectile;
	public float Damage = -1f;

	public GameObject ProjectileShooter;
	public float Rate = 0.25f;

	void OnGUI()
	{
		GUI.Box ( new Rect (Screen.width - 150, 90,140,140), "");

		GUI.Label(new Rect(Screen.width -140, 100, 500, 500),"Projectile Settings");

		GUI.Label(new Rect(Screen.width -140, 140, 500, 500),"Rate of Fire");

		Rate = GUI.HorizontalSlider(new Rect(Screen.width -130, 160, 100, 25), Rate, 0.75F, 0.05F);

		ProjectileShooter.GetComponent<ProjectileShooterScript>().Rate = Rate;

		GUI.Label(new Rect(Screen.width -140, 180, 500, 500),"Damage | Heal");

		Damage = GUI.HorizontalSlider(new Rect(Screen.width -130, 200, 100, 25), Damage, -10F, 10F);

		Projectile.GetComponent<ProjectileScript>().Damage = Mathf.Round(Damage);


	}
}
