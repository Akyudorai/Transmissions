using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : Device
{
    [Header("Door Components")]
    [SerializeField] Transform point1 = null;
    [SerializeField] Transform point2 = null;  // p1 = open; p2 = close
    //Vector3 openSpeed = Vector3.zero;
    //Vector3 closeSpeed = Vector3.zero;
    
    // -------------------------------------------------------------------
    //                      o Class Methodology o
    // -------------------------------------------------------------------

    private void Start()
    {
        //openSpeed = (point2 - transform.position) / 20;
        //closeSpeed = -openSpeed;
    }

    private IEnumerator OpenDoor(bool state)
    {

        // TEMP
        if (state)
        {
            //gameObject.transform.position += openSpeed;
            gameObject.transform.position = point2.position;            
        }

        if (!state)
        {
            //gameObject.transform.position += closeSpeed;
            gameObject.transform.position = point1.position;
        }

        yield return new WaitForSeconds(0.1f);

        //if (!state && gameObject.transform.position.y > point2.position.y)
        //    StartCoroutine(OpenDoor(false));

        //if (state && gameObject.transform.position.y < point1.position.y)
        //    StartCoroutine(OpenDoor(true));
        
    }

    // -------------------------------------------------------------------
    //                    o Overridden Methodology o
    // -------------------------------------------------------------------

    public override void Activate()
    {
        if (isActive == false)
            StartCoroutine(OpenDoor(true));

        isActive = true;
    }

    public override void Deactivate()
    {
        if (isActive == true)
            StartCoroutine(OpenDoor(false));

        isActive = false;
    }

    public override DeviceType GetDeviceType()
    {
        return DeviceType.door;
    }
}
