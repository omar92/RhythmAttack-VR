using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteParticles : MonoBehaviour {

    public ParticleSystem noteParticle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Sword")
        {
            noteParticle.Play();
        }
    }
}
