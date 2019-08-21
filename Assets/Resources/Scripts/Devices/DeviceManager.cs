using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour {
    
    [Header("Device Management")]
    [SerializeField] Device contraption;    

    public void DeviceTrigger(bool state) {

        switch (state)
        {
            case true:
                contraption.Activate();
                break;

            case false:
                contraption.Deactivate();                
                break;
        }
    }
	


}
