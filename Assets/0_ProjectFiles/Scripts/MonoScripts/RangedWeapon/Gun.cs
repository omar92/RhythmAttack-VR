using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour, IControllable {


    public float range = 100f;
    public float fireRate = 15f;
    public float destroyAfter = 0.1f;

    public GameEvent GunVibrate;
    public GameEvent BossHitE;
    float nextTimeToFire = 0f; 
    public Rigidbody projectile;
    public GeneralSounds sounds;
    AudioSource Source;

    [SerializeField]
    Transform gunFire;
    [SerializeField]
    ParticleSystem gunFireParticle;
    [SerializeField]
    ParticleSystem bossHitParticle;

    List<ParticleSystem> bulletsParticle ;
    List<ParticleSystem> hitParticle;

    private void Awake()
    {
        Source = GetComponent<AudioSource>();
        Source.clip = sounds.RangedShoot;
    }
    private void Start()
    {
        //StartCoroutine(CleanSceneFromParticles());
    }
    public void OnTrigger(bool isDown)
    {
        if (isDown)
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {

        if (this.gameObject.activeInHierarchy == true)
        {
            //muzel fire effect
            var gunParticle = Instantiate(gunFireParticle, gunFire.position, Quaternion.identity);
            gunParticle.gameObject.SetActive(true);
            Destroy(gunParticle, destroyAfter);

            //gun sounds
            Source.Play();

            //gun vibration
            GunVibrate.Raise();

            //throw projectile
            RaycastHit hit;
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position, projectile.transform.rotation) as Rigidbody;
            clone.velocity = transform.TransformDirection(Vector3.forward * 200);
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, range))
            {
                Debug.DrawRay(this.transform.position, this.transform.forward, Color.green, range, true);
                var target = hit.transform.GetComponent<TargetMonester>();
                if (target)
                {
                    target.OnHit();
                    BossHitE.Raise();
                }
                //bullet collision effect
                var bossParticle = Instantiate(bossHitParticle, hit.transform.position, Quaternion.identity);
                bossParticle.gameObject.SetActive(true);
                Destroy(bossParticle, destroyAfter);
            }
            Destroy(clone.gameObject, destroyAfter);
        }
    }

    public void OnSqueez(bool isDown)
    {
        //throw new NotImplementedException();
    }
    //IEnumerator CleanSceneFromParticles()
    //{
        
    //    //yield return new  WaitForSeconds(6f);
    //    //for (int i = 0; i < bulletsParticle.Capacity; i++)
    //    //{
    //    //    bulletsParticle[i].gameObject.SetActive(false);
    //    //}
    //    //for (int i = 0; i < hitParticle.Capacity; i++)
    //    //{
    //    //    hitParticle[i].gameObject.SetActive(false);
    //    //}
    //}
}
