using UnityEngine;
using System.Collections;

public class CursorMovement : MonoBehaviour {
    public int id;
    public float cursorSensibility=15;
    public Vector2 minLimit, maxLimit;
    Vector3 cursorPosition;
    // Use this for initialization
    void Start () {
        
        maxLimit.x = -Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0,0)).x;
        minLimit.x = -maxLimit.x;
        maxLimit.y = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        minLimit.y = -maxLimit.y;
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
    }

    void Movement()
    {
        cursorPosition = transform.position;
        float h = 0;//Input.GetAxisRaw("L_YAxis_" + id);
        float v = 0;//Input.GetAxisRaw("L_XAxis_" + id);
        if (Mathf.Abs(h) > 0.3f)
        {
            cursorPosition.x -= h * cursorSensibility * Time.deltaTime;
        }

        if (Mathf.Abs(v) > 0.3f)
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
