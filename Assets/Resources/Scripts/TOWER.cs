using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TOWER : MonoBehaviour {

    public enum BroadcastType { Normal, Device };

    [Header("Broadcast Type")]
    public BroadcastType type;
    
    [Header("Tower Components")]
    [SerializeField] Transform broadcastPoint;    
    [SerializeField] bool hasTransmitted = false;
    DeviceManager dm;
    [SerializeField] float signalDuration, signalDecay;
    [SerializeField] bool isMain = false;

    [SerializeField] Canvas timerCanvas;
    [SerializeField] Image timer;

    [SerializeField] SpriteRenderer sprite;

    [SerializeField] Color inactiveColor;
    [SerializeField] Color fullActiveColor;

    ////////////////////////////////////////////
    private bool isTowerDecay; 

	private void Start () {
		
        hasTransmitted = false;

        if (type == BroadcastType.Device)
        {
            dm = gameObject.GetComponent<DeviceManager>();            
        }

        if (isMain)
            setTransmission(true);
        else 
            setTransmission(false);

	}
	
	private void Update () {
		
        if (hasTransmitted && !timerCanvas.enabled)
        {
            timerCanvas.enabled = true;            
        }

        else if (hasTransmitted && timerCanvas.enabled)
        {
            timer.fillAmount = signalDuration / 30;            
        }

        if (!hasTransmitted && timerCanvas.enabled)
        {
            timerCanvas.enabled = false;
        }


        if (hasTransmitted && dm)
        {
            sprite.color = Color.Lerp(inactiveColor, Color.blue, signalDuration / 30);
            dm.DeviceTrigger(true);
        }
        
        else if (hasTransmitted && !dm)
        {
            sprite.color = Color.Lerp(inactiveColor, fullActiveColor, signalDuration / 30);
        }

        else if (!hasTransmitted && dm)
        {
            sprite.color = inactiveColor;
            dm.DeviceTrigger(false);
        }

        else if (!hasTransmitted && !dm)
        {
            sprite.color = inactiveColor;
        }
        
       

        // Signal Fade
        if (signalDuration < 0)
            signalDuration = 0;
        else if (signalDuration == 0)
            setTransmission(false);
        else if (signalDuration > 0 && !isMain)
        {           
            signalDuration -= signalDecay * Time.deltaTime;
        }


    }

    public bool getTransmission() {
        return hasTransmitted;
    }

    public void setTransmission(bool state) {
        hasTransmitted = state;      
        
        
    }

    public void Hook()
    {
        signalDuration = 30.0f;
        signalDecay = 0.0f;
    }

    public void Unhook()
    {
        signalDuration = 30.0f;
        signalDecay = 1.0f;
    }

    public Transform getPoint() {
        return broadcastPoint;
    }

    ////////////////////////////////////////////////////

    public void SetTowerSettings(bool setTowerDecay)
    {

    }
}
