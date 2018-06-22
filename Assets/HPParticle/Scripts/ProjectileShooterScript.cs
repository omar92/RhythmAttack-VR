//The CannonScript produces the projectiles 

using UnityEngine;
using System.Collections;

public class ProjectileShooterScript : MonoBehaviour {

	public GameObject Target; //the GameObject that the cannon should face
	
	public GameObject Projectile; //This is the GameObject that the Cannon Fires
	public float ProjectileForce; //The Force that the projectile is shot at
	public float Scatter; //How randomly the shots are fired
	
	public float Rate; //the rate in which projectiles are shot
	private float LastShotTime = 0f; //this stores the time when each projectile is shot

	public GameObject Beryl;

	// Use this for initialization
	void Start () 
	{
		Beryl =  gameObject.transform.Find("Beryl").gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (Time.time - LastShotTime >= Rate)//determine if a shot should be fired
		{
			//Instantiate a new projectile
			GameObject ThisProjectile = Instantiate(Projectile,Beryl.transform.position,Beryl.transform.rotation) as GameObject;
			
			//add force to the projectile
			ThisProjectile.GetComponent<Rigidbody>().AddRelativeForce(Random.Range(Scatter * -1f, Scatter),Random.Range(Scatter * -1f, Scatter),ProjectileForce + Random.Range(Scatter * -1f, Scatter));

			//set the owner of the projectile...this will allow the shield to determine weather or not to let the projectile pass through
			if (gameObject.transform.parent == null)
			{
				ThisProjectile.GetComponent<ProjectileScript>().Owner = gameObject;

			}
			else
			{
				ThisProjectile.GetComponent<ProjectileScript>().Owner = gameObject.transform.parent.gameObject;
			}
			
			
			LastShotTime = Time.time; //set the time when the projectile was produced
		}
		
		//rotate the cannon to look at the target object
		Vector3 dir = Target.transform.position - transform.position;
		Quaternion Rotation = Quaternion.LookRotation(dir);
		gameObject.transform.rotation = Rotation;

	}


}
