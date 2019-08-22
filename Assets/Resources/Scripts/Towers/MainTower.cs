using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : Tower
{
    protected override void Start()
    {
        base.Start();

        hasTransmitted = true;
    }

    protected override void Update()
    {
        base.Update();
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
