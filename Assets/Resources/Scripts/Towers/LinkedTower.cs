﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedTower : Tower
{
    [Header("Device")]
    [SerializeField] DeviceManager dm;
    

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // Enable and Disable the device if there is power
        {
            if (hasTransmitted)
            {
                dm.DeviceTrigger(true);
            }

            else
            {
                dm.DeviceTrigger(false);
            }
        }

        // Periodic Signal Fade
        {
            if (signalDuration < 0)
                signalDuration = 0;
            else if (signalDuration == 0)
                SetTransmission(false);
            else if (signalDuration > 0)
            {
                signalDuration -= signalDecay * Time.deltaTime;
            }
        }
    }

    public override Color GetActiveColor()
    {
        return activeColor;
    }

    public override Color GetInactiveColor()
    {
        return inactiveColor;
    }
}