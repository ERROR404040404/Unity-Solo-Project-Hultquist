using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{

    public Rigidbody2D rb;

    float inputX;
    float inputY;

    float directionface;

    float acceleration;
    float MaxSpeed;
    float AccelTime;

    public int health = 10;
    public int maxHealth = 10;

    public float speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Cursor.visible = false; makes cursur non-visible
        // Cursor.lockState = CursorLockMode.Locked; locks cursor
        
    }

    // Update is called once per frame
    private void Update()
    {
       Vector2 tempMove = rb.linearVelocity;

        tempMove.x = inputX * speed;

        if(health <= 0) //R U DED?
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        health = 0;
    }
}
