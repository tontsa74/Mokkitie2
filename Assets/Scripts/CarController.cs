using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
    public bool braking;

}

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public List<Light> lights; // the information about each individual axle

    public float maxMotorTorque = 400; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle = 30; // maximum steer angle the wheel can have
    public float maxBrakeTorque = 1000;
    private bool lightsOn = true;
    private bool isBraking = false;
    private bool isReversing = false;
    private float rearLightIntensity = 1f;
    private float rearLightRange = 0.3f;


    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        float brake = maxBrakeTorque * 0;

        var velocity = GetComponent<Rigidbody>().velocity;
        var localVel = transform.InverseTransformDirection(velocity);

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            if (axleInfo.braking)
            {
                if (localVel.z > 0 && Input.GetAxis("Vertical") < 0 ||
                        localVel.z < 0 && Input.GetAxis("Vertical") > 0) {
                    brake = maxBrakeTorque * Mathf.Abs(Input.GetAxis("Vertical"));
                    if(localVel.z > 0)
                    {
                        isBraking = true;
                        BrakingLights(isBraking);
                    }
                } else if(localVel.z < 0 && Input.GetAxis("Vertical") < 0) //(isBraking || isReversing)
                {
                    isBraking = false;
                    BrakingLights(isBraking);
                    isReversing = true;
                    ReverseLights(isReversing);
                } else {
                    isBraking = false;
                    BrakingLights(isBraking);
                    isReversing = false;
                    ReverseLights(isReversing);
                }
                axleInfo.leftWheel.brakeTorque = brake;
                axleInfo.rightWheel.brakeTorque = brake;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            ResetCar();
        }
        if (Input.GetKeyDown("l"))
        {
            LightsOnOff();
        }
    }

    void ResetCar()
    {
        transform.position = new Vector3(0, 3, 0);
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void LightsOnOff()
    {
        if(lightsOn)
        {
            foreach (Light light in lights)
            {
                light.enabled = false;
            }
            lightsOn = false;
        } else if(!lightsOn)
        {
            foreach (Light light in lights)
            {
                light.enabled = true;
            }
            lightsOn = true;
        }
    }

    void BrakingLights(bool isBraking)
    {
        if(isBraking)
        {
            lights[4].range = rearLightRange * 1.5f;
            lights[5].range = rearLightRange * 1.5f;
            lights[4].intensity = rearLightIntensity * 1.5f;
            lights[5].intensity = rearLightIntensity * 1.5f;
        } else
        {
            lights[4].range = rearLightRange;
            lights[5].range = rearLightRange;
            lights[4].intensity = rearLightIntensity;
            lights[5].intensity = rearLightIntensity;
        }

    } 

    void ReverseLights(bool isReversing)
    {
        if (isReversing)
        {
            lights[4].range = rearLightRange * 1.5f;
            lights[4].intensity = rearLightIntensity * 1.5f;
            lights[4].color = Color.white;
        }
        else
        {
            lights[4].range = rearLightRange;
            lights[4].intensity = rearLightIntensity;
            lights[4].color = Color.red;
        }
    }
}

