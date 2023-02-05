using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed=1000f;

    private Quaternion rotationY;


    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        InputTouch();
    }


    private void InputTouch()
    {
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                rotationY = Quaternion.Euler(transform.rotation.x, touch.deltaPosition.x *rotationSpeed *Time.deltaTime , transform.rotation.z);
                transform.rotation *= rotationY;
            }
        }
    }
}
