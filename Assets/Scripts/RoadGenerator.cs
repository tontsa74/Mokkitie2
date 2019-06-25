using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RoadBlock
{
    public GameObject block;
    public float probability;
}

public class RoadGenerator : MonoBehaviour
{
    public List<RoadBlock> roadBlocks;

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

    // Start is called before the first frame update
    void Start()
    {
        foreach(RoadBlock rb in roadBlocks)
        {
            totalProbability += rb.probability;
        }
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate() {
        AddBlock(roadBlocks[0], startPosition, startRotation);
        for (int i = 0; i < roadLength; i++) {

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

        Transform EndPoint = newBlock.transform.Find("EndPoint").gameObject.transform;

        if (EndPoint.eulerAngles.y < 270 && EndPoint.eulerAngles.y > 90 || Mathf.Abs(EndPoint.eulerAngles.z) > maxSlopeAngle)
        {
            Destroy(newBlock);
            return false;
        } else
        {
            nextPosition = EndPoint.position;
            nextRotation = EndPoint.eulerAngles;
        }
        return true;
    }
    
}