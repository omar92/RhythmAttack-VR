using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ANote : MonoBehaviour
{
    public LevelSettings settings;
    private Rigidbody rb;
    private Vector3 velocity;

    protected Rigidbody Rb
    {
        get
        {
            if (rb == null) Rb = GetComponent<Rigidbody>();
            return rb;
        }

        set
        {
            rb = value;
        }
    }

    protected Collider Co
    {
        get
        {
            if (_co == null) _co = GetComponent<Collider>();
            return _co;
        }

        set
        {
            _co = value;
        }
    }
    private Collider _co;
    protected virtual void Spawn(Vector3 source, Vector3 dist)
    {
        transform.SetParent(null);
        if (_co) _co.enabled = true;
        transform.position = source;
        var newDistance = Vector3.Distance(source, dist);
        float newVelocity = newDistance / settings.NoteVelocity;
        velocity = (dist - source).normalized * newVelocity;
        Rb.velocity = velocity;
    }

    public void OnPause()
    {
        if (_co) _co.enabled = false;
        Rb.velocity = Vector3.zero;
    }
    public void OnUnPause()
    {
        if (_co) _co.enabled = true;
        Rb.velocity = velocity;
    }

    public virtual void Hide()
    {
        Destroy(gameObject);
    }

}
