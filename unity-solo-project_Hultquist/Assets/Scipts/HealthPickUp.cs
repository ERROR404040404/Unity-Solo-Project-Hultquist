using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
   
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.position = new Vector3(10000, 10000, 0);
            Invoke("DelayedAction", 600f);
        }

    }

    void DelayedAction()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }


}
