using UnityEngine;
public class GoToMouse : MonoBehaviour
{
    private Vector3 mPos;
    public float mSpeed = .1f;

    // Update is called once per frame
    void Update()
    {
        mPos = Input.mousePosition;
        mPos = Camera.main.ScreenToWorldPoint(mPos);
        transform.position = Vector2.Lerp(transform.position, mPos, mSpeed);
    }
}
