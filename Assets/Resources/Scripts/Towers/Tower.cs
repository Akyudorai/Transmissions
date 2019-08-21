using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tower : MonoBehaviour
{    
    public Transform broadcastPoint = null;
    protected bool hasTransmitted = false;
    protected float signalDuration = 0;
    protected float signalDecay = 0;

    [SerializeField] Canvas timerCanvas;
    [SerializeField] Image timer;

    [SerializeField] SpriteRenderer sprite;

    [SerializeField] protected Color activeColor = new Color(0, 230, 255, 255);
    [SerializeField] protected Color inactiveColor = new Color(123, 123, 123, 255);

    // -------------------------------------------------------------------
    //                    o Virtual Methodology o
    // -------------------------------------------------------------------

    protected virtual void Start()
    {
        hasTransmitted = false;    
    }

    protected virtual void Update()
    {
        // Handle the Visible TImer for the Tower
        {
            // Enables the timer if power has been transmitted
            if (hasTransmitted && !timerCanvas.enabled)
            {
                timerCanvas.enabled = true;
            }

            // While the timer is active, adjust the fill amount
            else if (hasTransmitted && timerCanvas.enabled)
            {
                timer.fillAmount = signalDuration / 30;
            }

            // When the power is lost, disable the canvas
            if (!hasTransmitted && timerCanvas.enabled)
            {
                timerCanvas.enabled = false;
            }
        }

        // Handle the Color Change of the tower
        {
            if (hasTransmitted)
            {                
                sprite.color = Color.Lerp(GetInactiveColor(), GetActiveColor(), signalDuration / 30);
            }

            else
            {
                sprite.color = GetInactiveColor();
            }
        }        
    }

    // -------------------------------------------------------------------
    //                    o Inherited Methodology o
    // -------------------------------------------------------------------    

    public bool GetTransmission() {
        return hasTransmitted;
    }

    public void SetTransmission(bool state) {
        hasTransmitted = state;
    }

    public void Link()
    {
        signalDuration = 30.0f;
        signalDecay = 0.0f;
    }

    public void Unlink()
    {
        signalDuration = 30.0f;
        signalDecay = 1.0f;
    }

    public Transform GetShotPoint() {
        return broadcastPoint;
    }

    // -------------------------------------------------------------------
    //                    o Abstract Methodology o
    // -------------------------------------------------------------------

    public abstract Color GetActiveColor();
    public abstract Color GetInactiveColor();    
}
