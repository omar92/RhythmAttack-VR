// This Script is attached to the projectile

using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

	//stores damage the projectile will do
	public float Damage;

	//stores the owner of the projectile
	public GameObject Owner;

	//stores the explosion the projectile will make when it hits something
	public GameObject Explosion;

	void OnCollisionEnter(Collision col) 
	{
		//make the explosion
		GameObject ThisExplosion = Instantiate(Explosion,gameObject.transform.position,gameObject.transform.rotation) as GameObject;

		//destory the projectile
		Destroy(gameObject);
	}	
}
