using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Camera cam;
    GameObject pickupObj;
    Camera playerCam;
    public AudioSource speakers;

    public PlayerInput input;
    public Transform weaponSlot;
    public Weapon currentWeapon;

    public float rForce = 100f;


    public Rigidbody2D rb;

    public float inputX;
    public float inputY;

    public int health = 10;
    public int maxHealth = 10;

    public float interactDistance = 1f;

    public float speed = 5f;

    public bool attacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        cam = Camera.main;
        input = GetComponent<PlayerInput>();
        weaponSlot = transform.GetChild(0);


        rb = GetComponent<Rigidbody2D>();
        weaponSlot = transform.GetChild(0);
        Cursor.visible = false;

        //Cursor.visible = false; makes cursur non-visible
        // Cursor.lockState = CursorLockMode.Locked; locks cursor

    }

    // Update is called once per frame
    private void Update()
    {
        if (Physics2D.Raycast(transform.position, transform.right, interactDistance))
        {
            if (Physics2D.Raycast(transform.position, transform.right, interactDistance).collider.tag == "weapon")
            {
                pickupObj = Physics2D.Raycast(transform.position, transform.right, interactDistance).transform.gameObject;
            }
            else
                pickupObj = null;
        }

        if (currentWeapon)
            if (currentWeapon.holdToAttack && attacking)
                currentWeapon.fire();


        Vector2 tempMove = rb.linearVelocity;

        tempMove.x = inputX * speed;

        if (health <= 0) //R U DED?
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void FixedUpdate()
    {
        // Some Move Code, flips player
        if (inputX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

            GetComponent<SpriteRenderer>().flipX = true;
            Debug.Log("goin' left");
        }
        if (inputX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

            GetComponent<SpriteRenderer>().flipX = false;
            Debug.Log("goin' right");
        }
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // Recoil?
              
           if (currentWeapon)
                if (currentWeapon.holdToAttack && attacking)
                if (currentWeapon.clip != 1)
                {
                if (inputX < 0)
                    rb.AddForce(-transform.right * rForce);
                    rb.AddForce(transform.up * rForce);

                if (inputX > 0)
                    rb.AddForce(transform.right * rForce);
                    rb.AddForce(transform.up * rForce);
            }
        

    }


    public void Interact()
    {
        

        if (pickupObj)
        {
            if (pickupObj.tag == "weapon")
                pickupObj.GetComponent<Weapon>().equip(this);

            Debug.Log("Weapon_Got");
        }
    }
    public void Reload()
    {
        if (currentWeapon)
            if (!currentWeapon.reloading)
                currentWeapon.reload();
    }
        

    public void Attack(InputAction.CallbackContext context)
    {
        if (currentWeapon)
        {
            if (currentWeapon.holdToAttack)
            {
                if (context.ReadValueAsButton())
                    attacking = true;

                else attacking = false;
            }
            else if (context.ReadValueAsButton())
                currentWeapon.fire();
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        Vector2 InputAxis = context.ReadValue<Vector2>();

        inputX = InputAxis.x;
        inputY = InputAxis.y;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Hazard")
        {
             health -= 2;
        }
    }
}
