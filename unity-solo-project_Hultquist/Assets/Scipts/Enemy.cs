using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;

    public int eHealth = 3;
    public int eMaxHealth = 3;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool shouldJump;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Is Grounded?
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, groundLayer);
        //Player Direction
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        //Player Above Detection
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, 1 << player.gameObject.layer);

        if (isGrounded)
        {
            //Chase Player
            rb.linearVelocity = new Vector2(direction * chaseSpeed, rb.linearVelocity.y);

            //Jump if gap is ahead && no ground infront
            //Else if player above & platform above

            //If Ground
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);
            //If Gap
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, groundLayer);
            //If Platform ahead
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, groundLayer);

            if (!groundInFront.collider && !gapAhead.collider)
            {
                shouldJump = true;
            }
            else if (isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }
        }
    }
    private void FixedUpdate()
    {
        if(isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (player.position - transform.position).normalized;

            Vector2 jumpDirection = direction * jumpForce;

            rb.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "prjctl")
        {
            eHealth -= 2;
            if (eHealth < 1)
                {
                transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }







}



