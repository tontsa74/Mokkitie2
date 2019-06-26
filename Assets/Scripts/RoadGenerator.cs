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
    int roadLengthCount;

    public float maxUphillAngle = 350f;
    public float maxDownhillAngle = 10f;

    public float maxAngleChange = 1f;
    public float maxSlopeAngle = 0f;

    float totalProbability;

    Vector3 nextPosition;

    Vector3 nextRotation;

    bool cross;

    // Start is called before the first frame update
    void Start()
    {
        cross = true;
        roadLengthCount = roadLength;

        road = new List<GameObject>();

        foreach(RoadBlock rb in roadBlocks)
        {
            totalProbability += rb.probability;
        }
        Generate(startPosition, startRotation, roadLengthCount);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            foreach(GameObject gameObject in road) {
                Destroy(gameObject);
            }
            cross = true;
            roadLengthCount = roadLength;
            Generate(startPosition, startRotation, roadLengthCount);
        }
    }

    void Generate(Vector3 startPos, Vector3 startRot, int blockAmount) {
        AddBlock(roadBlocks[0], startPos, startRot);
        for (int i = 0; i < blockAmount; i++) {

            float rdm = Random.Range(0, totalProbability);
            float probabilityCounter = 0;

            
            Vector3 angle = new Vector3(Random.Range(-maxAngleChange, maxAngleChange), 0, 0);

            Vector3 tempNextRotation = nextRotation + angle;

            if (!(tempNextRotation.x < maxUphillAngle && tempNextRotation.x > maxDownhillAngle)) {
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
    }

    bool AddBlock(RoadBlock rb, Vector3 position, Vector3 rotation)
    {

        GameObject newBlock = Instantiate(rb.block, position, Quaternion.identity);
        newBlock.transform.Rotate(rotation);

        newBlock.transform.SetParent(transform);

        road.Add(newBlock);



        Transform[] points = newBlock.GetComponentsInChildren<Transform>();

        Transform resultEndPoint = transform;


        foreach(Transform point in points) {
            if (point.name == "EndPoint") {
                Transform endPoint = point;

                if(!(endPoint.eulerAngles.z > maxSlopeAngle && endPoint.eulerAngles.z < 360 - maxSlopeAngle)) {
            
                } else {
                    Destroy(newBlock);
                    return false;
                }
                
                if (endPoint.eulerAngles.y < 270 && endPoint.eulerAngles.y > 90)
                {
                    Destroy(newBlock);
                    return false;
                } else
                {
                    resultEndPoint = endPoint;
                }
            }

            if (point.name == "EndPoint2") {
                print("point name: " + point.name);
                Transform endPoint2 = point;

                if(!(endPoint2.eulerAngles.z > maxSlopeAngle && endPoint2.eulerAngles.z < 360 - maxSlopeAngle)) {
            
                } else {
                    Destroy(newBlock);
                    return false;
                }
                
                if (endPoint2.eulerAngles.y < 270 && endPoint2.eulerAngles.y > 90)
                {
                    Destroy(newBlock);
                    return false;
                }
                
                if (cross)
                {
                    print("Generate: ");
                    cross = false;
                    Generate(endPoint2.position, endPoint2.eulerAngles, roadLengthCount);
                } else 
                {
                    Destroy(newBlock);
                    return false;
                }
            }

        }

        nextPosition = resultEndPoint.position;
        nextRotation = resultEndPoint.eulerAngles;
        roadLengthCount--;
        return true;
    }
    
}