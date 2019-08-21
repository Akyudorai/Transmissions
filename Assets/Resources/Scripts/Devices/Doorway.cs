using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorway : Device
{
    [Header("Door Components")]
    [SerializeField] Vector3 point1 = Vector2.zero;
    [SerializeField] Vector3 point2 = Vector2.zero;  // p1 = open; p2 = close
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
            gameObject.transform.position = point2;            
        }

        if (!state)
        {
            //gameObject.transform.position += closeSpeed;
            gameObject.transform.position = point1;
        }

        yield return new WaitForSeconds(0.1f);

        if (!state && gameObject.transform.position.y > point2.y)
            StartCoroutine(OpenDoor(false));

        if (state && gameObject.transform.position.y < point1.y)
            StartCoroutine(OpenDoor(true));
        
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
