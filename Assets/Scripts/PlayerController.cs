using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public VariableJoystick variableJoystick;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    private bool touchStart = false;
    private Vector3 pointA;
    private Vector3 pointB;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        variableJoystick.SetMode(JoystickType.Dynamic);
        count = 0;
        SetCountText();
        winText.text = "";

    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           pointA = new Vector3(Input.mousePosition.x, 0,Input.mousePosition.y);
        }
        if(Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = new Vector3(Input.mousePosition.x, 0,Input.mousePosition.y);
        }
        else
        {
            touchStart = false;
        }
    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //rb.AddForce(movement * speed * Time.deltaTime);
        if (touchStart)
        {
            Vector3 offset = pointB - pointA;
            //Vector3 direction = Vector3.ClampMagnitude(offset, 1.0f);
            Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!!"; 
        }
    }
}
