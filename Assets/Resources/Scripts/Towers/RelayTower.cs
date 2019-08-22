using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelayTower : Tower
{
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

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

    public override string GetTowerType()
    {
        return "Relay";
    }
}
