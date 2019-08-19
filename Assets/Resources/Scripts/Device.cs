using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour {

    public enum DeviceType { door, gravity };
    public DeviceType type;

    [Header("Door Components")]
    [SerializeField] Vector3 point1, point2;  // p1 = open; p2 = close
    Vector3 openSpeed, closeSpeed;
    
    [Header("Satalite Components")]
    [SerializeField] Vector2 direction;
    private Vector2 force;
    [SerializeField] ParticleSystem p1, p2; // p1 = active;  p2 = inActive
    

    private bool isActive = false;

    private void Start() {
        
        force = direction;

        openSpeed = (point2 - transform.position) / 20;
        closeSpeed = -openSpeed;

        switch (type)
        {
            case DeviceType.door:

                break;

            case DeviceType.gravity:

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

                

                break;

        }

        
        
    }

    public void Activate() {


        switch (type) 
        {
            case DeviceType.door:   
                if (isActive == false)
                    StartCoroutine(OpenDoor(true));

                isActive = true;
                break;

            case DeviceType.gravity:
                // Gravity Up;
                force = -direction;

                var em1 = p1.emission;
                em1.enabled = true;              

                var em2 = p2.emission;
                em2.enabled = false;


                break;
        }

        isActive = true;

    }

    public void Deactivate() {        

        switch (type)
        {
            case DeviceType.door:   
                if (isActive == true)
                    StartCoroutine(OpenDoor(false));

                isActive = false;
                break;

            case DeviceType.gravity:
                // Gravity Down;
                force = direction;

                var em1 = p1.emission;
                em1.enabled = false;

                var em2 = p2.emission;
                em2.enabled = true;

                break;
        }

        isActive = false;

    }

    public Vector2 GetDirection() {
                
        return force;
    }

    private IEnumerator OpenDoor(bool state) {
        
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
	
}
