using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CONTROLLER : MonoBehaviour {

    //[Header("Ball Components")]
    Rigidbody2D rigid;
    LineRenderer line;
    SpriteRenderer sprite;

    [Header("Shot Components")]        
    [SerializeField] float shotPower; // The force that will be placed on the object
    [SerializeField] float shotMax; // The maximum force that can be applied to the object
    private int numBounce;
    private bool canShot;
    [Range(0.0f, 1.0f)] private float opacity = 1.0f;

    [Header("Hooked Tower")]
    [SerializeField] TOWER linkedTower;


    [SerializeField]
    bool isDrag; // Charge shot power based on the distance dragged away from the center of the object

    [SerializeField]
    bool isHooked;

    private void Start() {

        rigid = gameObject.GetComponent<Rigidbody2D>();
        line = gameObject.GetComponent<LineRenderer>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        canShot = true;

        numBounce = 0;

        if (line != null) 
        {
            line.startColor = Color.blue;
            line.endColor = Color.gray;

            line.endWidth = 0.1f;
            line.startWidth = 0.2f;

            

            line.enabled = false;

        }
        
    }

    private void Update () {
		
        // Change Opacity of ball as the strength of the signal fades
        sprite.color = new Color(255, 255, 255, opacity);

        if (!_GM.pauseGame)
        {
            if (isHooked)
            {
                rigid.rotation = 0.0f;
                rigid.velocity = new Vector2(0.0f, 0.0f);
                rigid.gravityScale = 0.0f;
            }

            else
            {
                rigid.isKinematic = false;
                rigid.gravityScale = 1.0f;
            }


            // CHECK MOUSE DRAG
            if (Input.GetKeyDown(KeyCode.Mouse0) && isDrag == false && canShot == true)
            {
                isDrag = true; // Set drag to true                            
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && isDrag == true)
            {
                // Unhook the shot if hooked;
                if (isHooked) SetHooked(false, gameObject.transform);

                // float power = shotPower;
                Vector2 mousePos = Input.mousePosition; // Acquire mouse position in screen point
                mousePos = Camera.main.ScreenToWorldPoint(mousePos); // Convert to world point
                mousePos = new Vector3(mousePos.x, mousePos.y, 0);           


                Vector2 objPos = gameObject.transform.position; // Acquire object position in world point

                Vector2 direction = objPos - mousePos; // Calculate vector between the two
                direction *= shotPower; // Multiply that vector by the power of the shot            

                if (direction.x > 15.0f) direction.x = 15.0f;
                else if (direction.x < -15.0f) direction.x = -15.0f;

                if (direction.y > 15.0f) direction.y = 15.0f;
                else if (direction.y < -15.0f) direction.y = -15.0f;

                rigid.AddForce(direction, ForceMode2D.Impulse); // Apply a force equal to the direction vector
                GameObject.Find("_GM").GetComponent<_GM>().AddShot();


                isDrag = false; // Set drag to false
                shotPower = 0.0f; // Reset the shot power
                linkedTower.Unhook(); // Unhook the ball and set the signal fade timer;
                line.enabled = false;
                canShot = false;
            }

            // IF MOUSE IS BEING DRAGGED, CHARGE YOUR FORCE
            if (isDrag)
            {
                // Hide default cursor
                //Cursor.visible = false;           

                // Charging based on time held down
                if (shotPower < shotMax)
                    shotPower = 4;

                if (shotPower > shotMax)
                    shotPower = shotMax;

                ArcLine();

            }            
        }
	}

    private void ArcLine() {

        
        Vector2 p1 = gameObject.transform.position;
        
        Vector2 p2 = Input.mousePosition;
        p2 = Camera.main.ScreenToWorldPoint(p2);

        Vector3 dir = -(p1 - p2);        

        line.SetPosition(0, p1);
        line.SetPosition(1, p2);

        line.enabled = true;

    }


    public void SetHooked(bool state, Transform point) {
        
        // Set the Hook
        isHooked = state;
        transform.position = point.position;
        opacity = 1.0f; // Full Strength after reaching a tower        
        
        if (!canShot)
            canShot = true;

        if (!rigid)
            rigid = gameObject.GetComponent<Rigidbody2D>();

        rigid.rotation = 0.0f;
        rigid.velocity = new Vector2(0.0f, 0.0f);
        rigid.gravityScale = 0.0f;

        // Transmit Data       
        if (point.gameObject.GetComponentInParent<TOWER>())
        {
            linkedTower = point.gameObject.GetComponentInParent<TOWER>();
            linkedTower.setTransmission(true);
            linkedTower.Hook();
        }
            

        // Reset Shot
        numBounce = 0;
    }

    private void OnCollisionEnter2D(Collision2D col) {

        if (col.gameObject.tag == "Rubber")
        {       
            numBounce += 4;
            opacity -= 0.33f;  // Lose Strength for every bounce

            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y / 1.2f);

            if (numBounce >= 10)
            {
                Destroy(gameObject);
               // GameObject.Find("_GM").GetComponent<_GM>().Life(-1);
            }
                
        }
        
        if (col.gameObject.tag == "Copper")
        {            
            //GameObject.Find("_GM").GetComponent<_GM>().Life(-1);
            Destroy(gameObject);
        }

        if (col.gameObject.tag == "Aluminum")
        {
            
            numBounce += 0;
            opacity -= 0.0f;  // Lose Strength for every bounce

            if (numBounce >= 10)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        
        if (col.tag == "Hook")
        {            
            SetHooked(true, col.transform);            
        }

             

    }

    private void OnTriggerStay2D(Collider2D col) {
        
        if (col.tag =="Well")
        {
            Vector2 dir = transform.position - col.gameObject.transform.position;
            Debug.Log("Pulling Ball");
            rigid.AddForce(dir * -25.0f);
        }

        if (col.tag == "Gravity")
        {
            rigid.AddForce(col.gameObject.GetComponent<Device>().GetDirection());
        }

    }

}
