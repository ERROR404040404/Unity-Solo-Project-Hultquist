using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Ray2D interactRay;
    RaycastHit2D interactHit;
    GameObject pickupObj;
    Camera playerCam;

    public PlayerInput input;
    public Transform weaponSlot;
    public Weapon currentWeapon;


    public Rigidbody2D rb;

    float inputX;
    float inputY;

    float directionface;

    float acceleration;
    float MaxSpeed;
    float AccelTime;

    public int health = 10;
    public int maxHealth = 10;

    public float interactDistance = 1f;

    public float speed = 5f;

    public bool attacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        input = GetComponent<PlayerInput>();
        interactRay = new Ray2D(transform.position, transform.right);
        weaponSlot = transform.GetChild(0);


        rb = GetComponent<Rigidbody2D>();

        //Cursor.visible = false; makes cursur non-visible
        // Cursor.lockState = CursorLockMode.Locked; locks cursor

    }

    // Update is called once per frame
    private void Update()
    {
        if (Physics2D.Raycast(transform.position, transform.right))
        {
            if (interactHit.collider.tag == "weapon")
            {
                pickupObj = interactHit.collider.gameObject;
            }
        }
        else
            pickupObj = null;

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
    public void Interact()
    {
        if (pickupObj)
        {
            if (pickupObj.tag == "weapon")
                pickupObj.GetComponent<Weapon>().equip(this);
        }
    }
    public void Reload()
    {
        if (currentWeapon)
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
            health = 0;
        }
    }
}
