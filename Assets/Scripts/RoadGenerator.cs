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

    public Vector3 startPosition = new Vector3(0, 04083685, 0);

    public Vector3 startRotation = new Vector3(0, 0, 0);

    public int roadLength = 100;

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
            float count = 0;
            
            foreach (RoadBlock rb in roadBlocks)
            {
                count += rb.probability;
                if (rdm <= count)
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

        Transform[] sp = newBlock.GetComponentsInChildren<Transform>();

        if (sp[2].eulerAngles.y < 270 && sp[2].eulerAngles.y > 90)
        {
            Destroy(newBlock);
            return false;
        } else
        {
            nextPosition = sp[2].position;
            nextRotation = sp[2].eulerAngles;
        }
        return true;
    }
    
}