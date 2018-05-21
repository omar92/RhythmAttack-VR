using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCuttingEffect : MonoBehaviour
{

    public Rigidbody Half1;
    public Rigidbody Half2;
    public Transform SlicesContainer;


    // Use this for initialization
    void Awake()
    {
        Half1.isKinematic = true;
        Half2.isKinematic = true;
        Half1.gameObject.SetActive(false);
        Half2.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.tag == "Sword")
        {
            Cut();
        }
    }

    public void Cut()
    {
        StartCoroutine(SLiceNote());
    }

    private IEnumerator SLiceNote()
    {

        Slice();
        yield return new WaitForSeconds(1);
        Glow();
    }

    void Slice()
    {
        Half1.gameObject.SetActive(true);
        Half2.gameObject.SetActive(true);
        Half1.transform.parent = null;
        Half2.transform.parent = null;
        Half1.isKinematic = false;
        Half2.isKinematic = false;
        Half1.AddExplosionForce(1000, transform.position, 100);
        Half2.AddExplosionForce(1000, transform.position, 100);
    }
    void Glow()
    {
        Half1.isKinematic = true;
        Half2.isKinematic = true;
        Half1.velocity = Vector3.zero;
        Half2.velocity = Vector3.zero;
        Half1.transform.parent = SlicesContainer;
        Half2.transform.parent = SlicesContainer;
        Half1.transform.position = SlicesContainer.position;
        Half2.transform.position = SlicesContainer.position;
        Half2.transform.rotation = Quaternion.identity;
        Half2.transform.rotation = Quaternion.identity;
        Half1.gameObject.SetActive(false);
        Half2.gameObject.SetActive(false);
    }
}
