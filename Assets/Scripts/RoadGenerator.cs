using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RoadBlock
{
    public GameObject block;
    public float probability = 10;

}

public class RoadGenerator : MonoBehaviour
{
    public List<RoadBlock> roadBlocks;
    List<GameObject> road;

    public Vector3 startPosition = new Vector3(0, 0, 0);

    public Vector3 startRotation = new Vector3(0, 0, 0);

    public int roadLength = 100;

    public float maxUphillAngle = 350f;
    public float maxDownhillAngle = 10f;

    public float maxAngleChange = 1f;
    public float maxSlopeAngle = 0f;

    float totalProbability;

    Vector3 nextPosition;

    Vector3 nextRotation;

    bool cross = true;

    int minDirection = 90;
    int maxDirection = 270;

    public GameObject car;
    public GameObject cabin;

    // Start is called before the first frame update
    void Start()
    {

        road = new List<GameObject>();

        foreach (RoadBlock rb in roadBlocks)
        {
            totalProbability += rb.probability;
        }
        Generate(startPosition, startRotation, roadLength);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            CarController carController = car.GetComponent<CarController>();
            if(carController.checkpoint == null)
            {
                foreach (GameObject gameObject in road)
                {
                    Destroy(gameObject);
                }
                road.Clear();
                cross = true;
                minDirection = 90;
                maxDirection = 270;
                Generate(startPosition, startRotation, roadLength);
            }
        }
    }

    void Generate(Vector3 startPos, Vector3 startRot, int blockAmount)
    {
        if (cross)
        {
            AddBlock(roadBlocks[0], startPos, startRot);
        }
        else
        {
            nextPosition = startPos;
            nextRotation = startRot;
        }

        for (int i = 0; i < blockAmount; i++)
        {

            float rdm = Random.Range(0, totalProbability);
            float probabilityCounter = 0;


            Vector3 angle = new Vector3(Random.Range(-maxAngleChange, maxAngleChange), 0, 0);

            Vector3 tempNextRotation = nextRotation + angle;

            // check hill angles
            if (!(tempNextRotation.x < maxUphillAngle && tempNextRotation.x > maxDownhillAngle))
            {
                nextRotation = tempNextRotation;
            }

            foreach (RoadBlock rb in roadBlocks)
            {
                probabilityCounter += rb.probability;
                if (rdm <= probabilityCounter)
                {
                    if (!AddBlock(rb, nextPosition, nextRotation))
                    {
                        i--;
                    }

                    break;
                }
            }
        }
        //    AddCabin(cabin, startPos, startRot);
        AddCabin(cabin, nextPosition, nextRotation);

        // end of road
    }

    bool AddBlock(RoadBlock rb, Vector3 position, Vector3 rotation)
    {

        GameObject newBlock = Instantiate(rb.block, position, Quaternion.identity);
        newBlock.transform.Rotate(rotation);

        newBlock.transform.SetParent(transform);


        Transform[] points = newBlock.GetComponentsInChildren<Transform>();

        Transform resultEndPoint = transform;


        foreach (Transform point in points)
        {
            if (point.name == "EndPoint")
            {
                Transform endPoint = point;

                if (!(endPoint.eulerAngles.z > maxSlopeAngle && endPoint.eulerAngles.z < 360 - maxSlopeAngle))
                {

                }
                else
                {
                    Destroy(newBlock);
                    return false;
                }

                if (endPoint.eulerAngles.y < maxDirection && endPoint.eulerAngles.y > minDirection)
                {
                    Destroy(newBlock);
                    return false;
                }
                else
                {
                    resultEndPoint = endPoint;
                }
            }

            if (point.name == "EndPoint2")
            {
                Transform endPoint2 = point;

                if (!(endPoint2.eulerAngles.z > maxSlopeAngle && endPoint2.eulerAngles.z < 360 - maxSlopeAngle))
                {

                }
                else
                {
                    Destroy(newBlock);
                    return false;
                }

                if (endPoint2.eulerAngles.y < maxDirection && endPoint2.eulerAngles.y > minDirection)
                {
                    Destroy(newBlock);
                    return false;
                }

                if (cross)
                {
                    cross = false;
                    maxDirection = 360;
                    minDirection = 90;
                    Generate(endPoint2.position, endPoint2.eulerAngles, (roadLength - road.Count));
                    maxDirection = 270;
                    minDirection = 0;
                }
                else
                {
                    Destroy(newBlock);
                    return false;
                }
            }

        }

        road.Add(newBlock);
        nextPosition = resultEndPoint.position;
        nextRotation = resultEndPoint.eulerAngles;
        return true;
    }

    void AddCabin(GameObject go, Vector3 position, Vector3 rotation)
    {
        GameObject cabin = Instantiate(go, position, Quaternion.identity);
        cabin.transform.Rotate(new Vector3(-90, -26, 0));
        road.Add(cabin);
    }

}