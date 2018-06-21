using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIntersiction : MonoBehaviour {

    public ParticleSystem intersictionParticle;
    //public int layerMask =1<<13;
    public int myMask ;
    private void Start()
    {
        //intersictionParticle=Instantiate(intersictionParticle.gameObject,Vector3.zero,Quaternion.identity);
        intersictionParticle.gameObject.SetActive(false);
        
    }
    private void Update()
    {
        myMask = 1 << 12;
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, .025f, Vector3.Cross(transform.forward, transform.right), out hit, 1f,myMask))
        {
            if (hit.transform.tag == "Blade")
            {
                
                intersictionParticle.transform.position = hit.point;
                intersictionParticle.gameObject.SetActive(true);
            }
            else
            {
                intersictionParticle.gameObject.SetActive(false);
                
            }

        }
        else
        {
            intersictionParticle.gameObject.SetActive(false);

        }

    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, Vector3.Cross(transform.forward, transform.right) * 1f, Color.red);
        Gizmos.DrawSphere(transform.position, .025f);
    }

}
