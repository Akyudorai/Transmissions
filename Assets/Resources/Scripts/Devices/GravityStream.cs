using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityStream : Device
{
    [Header("Satalite Components")]
    [SerializeField] Vector2 direction;
    private Vector2 force;
    [SerializeField] ParticleSystem p1 = null;
    [SerializeField] ParticleSystem p2 = null; // p1 = active;  p2 = inActive

    // -------------------------------------------------------------------
    //                      o Class Methodology o
    // -------------------------------------------------------------------

    private void Start()
    {
        force = direction;

        var em1 = p1.emission;
        var em2 = p2.emission;

        if (isActive)
        {

            em1.enabled = true;
            em2.enabled = false;
        }

        else
        {
            em1.enabled = false;
            em2.enabled = true;
        }
    }

    public Vector2 GetDirection()
    {

        return force;
    }

    // -------------------------------------------------------------------
    //                    o Overridden Methodology o
    // -------------------------------------------------------------------

    public override void Activate()
    {
        // Gravity Up;
        force = -direction;

        var em1 = p1.emission;
        em1.enabled = true;

        var em2 = p2.emission;
        em2.enabled = false;

        isActive = true;
    }

    public override void Deactivate()
    {
        // Gravity Down;
        force = direction;

        var em1 = p1.emission;
        em1.enabled = false;

        var em2 = p2.emission;
        em2.enabled = true;

        isActive = false;
    }

    public override DeviceType GetDeviceType()
    {
        return DeviceType.gravityStream;
    }    
}
