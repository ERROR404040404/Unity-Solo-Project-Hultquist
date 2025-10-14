using UnityEngine;

public class Left_Right_Flip : MonoBehaviour
{
    float inputX;
    float inputY;
    PlayerController Player;

    private void FixedUpdate()
    {
        // Some Move Code, flips player
        if (inputX < 0)
        {        
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (inputX > 0)
        {            
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void Update()
    {
        inputX = Player.inputX;
        inputY = Player.inputY;
    }


}