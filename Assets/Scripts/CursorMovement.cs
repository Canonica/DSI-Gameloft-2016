using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {
    public int id;
    public float cursorSensibility=1;
    public Vector2 minLimit, maxLimit;
    Vector3 cursorPosition;
    // Use this for initialization
    void Start () {
        maxLimit.x = 10;
        maxLimit.y = 4;
        minLimit.x = -10;
        minLimit.y = -4;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
    }

    void Movement()
    {
        cursorPosition = transform.position;
        float h = Input.GetAxisRaw("Horizontal " + id);
        float v = Input.GetAxisRaw("Vertical " + id);
        if (Mathf.Abs(h) > 0.8f)
        {
            cursorPosition.x += h * cursorSensibility * Time.deltaTime;
        }

        if (Mathf.Abs(v) > 0.8f)
        {
            
            cursorPosition.z -= v * cursorSensibility * Time.deltaTime;
        }
        validateMove();
        
        
        
    }

    void validateMove()
    {
        if (cursorPosition.x > maxLimit.x || cursorPosition.x < minLimit.x)
        {
            cursorPosition.x = transform.position.x;
        }
        if (cursorPosition.z > maxLimit.y || cursorPosition.z < minLimit.y)
        {
            cursorPosition.z = transform.position.z;
        }
        transform.position = cursorPosition;
    }

}
