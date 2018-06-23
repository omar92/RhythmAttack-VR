using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteParticles : MonoBehaviour
{

    public ParticleSystem noteParticle;

    public void InstansiateParticle()
    {

        var slashParticle = Instantiate(noteParticle, transform.position, Quaternion.identity);
        slashParticle.Play();
        Destroy(slashParticle, 2f);
    }
}
