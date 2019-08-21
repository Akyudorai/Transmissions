using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Device : MonoBehaviour {

    public enum DeviceType { door, gravityStream, gravitySphere };    
    protected bool isActive = false;

    // -------------------------------------------------------------------
    //                    o Abstract Methodology o
    // -------------------------------------------------------------------

    public abstract void Activate();
    public abstract void Deactivate();
    public abstract DeviceType GetDeviceType();
    
}
