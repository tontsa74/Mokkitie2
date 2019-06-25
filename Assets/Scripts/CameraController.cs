using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform car;
    public float distance;
    public float height;
    public float rotationDamping;
    public float heightDamping;
    public float zoomRatio;
    public float defaultFOV;

    private float rotation_vector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            height += 0.15f;
            distance += 0.2f;
            defaultFOV += 0.5f;
        } else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (height > 0.1 && distance > 0.2f)
            {
                height -= 0.15f;
                distance -= 0.2f;
                defaultFOV -= 0.5f;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 local_velocity = car.InverseTransformDirection(car.GetComponent<Rigidbody>().velocity);
        if(local_velocity.z < -0.5f)
        {
            rotation_vector = car.eulerAngles.y + 180 ;
        } else
        {
            rotation_vector = car.eulerAngles.y;
        }

        if(Input.GetKey("left shift"))
        {
            RotateCamera(local_velocity);
        }

        float acceleration = car.GetComponent<Rigidbody>().velocity.magnitude;
        Camera.main.fieldOfView = defaultFOV + acceleration * zoomRatio * Time.deltaTime;
    }

    void LateUpdate()
    {
        float wantedAngle = rotation_vector;
        float wantedHeight = car.position.y + height;
        float myAngle = transform.eulerAngles.y;
        float myHeight = transform.position.y;

        myAngle = Mathf.LerpAngle(myAngle,wantedAngle,rotationDamping*Time.deltaTime);
        myHeight = Mathf.LerpAngle(myHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);

        transform.position = car.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        Vector3 temp = transform.position;
        temp.y = myHeight;
        transform.position = temp;

        transform.LookAt(car);

    }

    void RotateCamera(Vector3 localVelocity)
    {
        if (localVelocity.z < -0.5f)
        {
            rotation_vector = car.eulerAngles.y;
        }
        else
        {
            rotation_vector = car.eulerAngles.y + 180;
        }
    }
}
