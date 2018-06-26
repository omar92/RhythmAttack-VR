using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteCuttingEffect : MonoBehaviour
{

    public Rigidbody Half1;
    public Transform Stencel1;
    public Rigidbody Half2;
    public Transform Stencel2;
    public Transform SlicesContainer;

    public float ForceMultiplier = 1;

    private Coroutine co;

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
            var swordScript = collision.GetComponent<Sword>();
            Cut(swordScript.dir);
        }
    }

    public void CutUp()
    {
        Cut(Direction.UP);
    }

    public void CutRight()
    {
        Cut(Direction.RIGHT);
    }

    void Cut(Direction dir)
    {
        if (co == null)
        {
            //  SlicesContainer.rotation = Quaternion.Euler(0, 0, 90);
            co = StartCoroutine(SLiceNote(dir));
        }
        else
        {
            StopCoroutine(co);
            co = null;
            Glow();
            //   SlicesContainer.rotation = Quaternion.Euler(0, 0, 90);
            Cut(dir);
        }
    }

    private IEnumerator SLiceNote(Direction dir)
    {
        Slice(dir);
        yield return new WaitForSeconds(1);
        Glow();
        co = null;
    }

    void Slice(Direction dir)
    {
        Half1.gameObject.SetActive(true);
        Half2.gameObject.SetActive(true);

        // Half1.transform.localRotation = Quaternion.identity;
        // Half2.transform.localRotation = Quaternion.identity;

        if (dir == Direction.UP || dir == Direction.DOWN)
        {
            Stencel1.rotation = Quaternion.Euler(0, 90, 0);
            Stencel2.rotation = Quaternion.Euler(0, -90, 0);
            Half1.isKinematic = false;
            Half2.isKinematic = false;

            Half1.AddForce((-transform.right - (transform.forward / 2)) * ForceMultiplier, ForceMode.Impulse);
            Half2.AddForce((transform.right - (transform.forward / 2)) * ForceMultiplier, ForceMode.Impulse);
        }
        else
        {
            Stencel1.rotation = Quaternion.Euler(-90, 90, -90);
            Stencel2.rotation = Quaternion.Euler(90, 90, 0);
            Half1.isKinematic = false;
            Half2.isKinematic = false;

            Half1.AddForce((transform.up - (transform.forward / 2)) * ForceMultiplier, ForceMode.Impulse);
            Half2.AddForce((-transform.up - (transform.forward / 2)) * ForceMultiplier, ForceMode.Impulse);
        }
        Half1.transform.parent = null;
        Half2.transform.parent = null;
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

        Stencel1.rotation = Quaternion.Euler(0, 90, 0);
        Stencel2.rotation = Quaternion.Euler(0, -90, 0);

        Half1.gameObject.SetActive(false);
        Half2.gameObject.SetActive(false);
    }
}
