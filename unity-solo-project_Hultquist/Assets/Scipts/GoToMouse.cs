using UnityEngine;

public class GoToMouse : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = Input.mousePosition / 1000;
    }
}
