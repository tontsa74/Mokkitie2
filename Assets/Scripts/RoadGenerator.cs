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

    //[SerializeField]
    //public GameObject roadStraight;
    //public float probabilityStraight = 50f;

    //[SerializeField]
    //public GameObject roadJump;
    //public float probabilityJump = 10f;

    //[SerializeField]
    //public GameObject roadTurn;
    //public float probabilityTurn = 10f;

    public Vector3 startPosition = new Vector3(0, 04083685, 0);

    public Vector3 startRotation = new Vector3(0, 0, 0);

    Vector3 nextPosition;

    Vector3 nextRotation;

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate() {
        // AddStraight(startPosition, startRotation);
        AddBlock(roadBlocks[0], startPosition, startRotation);
        for (int i = 0; i < 100; i++) {

            float rdm = Random.Range(0, 1f);

            if (rdm < 0.2f)
            {
                //AddJump(nextPosition, nextRotation);
                AddBlock(roadBlocks[0], nextPosition, nextRotation);
            }
            else if(rdm < 0.4f)
            {
                //AddTurn(nextPosition, nextRotation);
                AddBlock(roadBlocks[1], nextPosition, nextRotation);
            } else
            {
                //AddStraight(nextPosition, nextRotation);
                AddBlock(roadBlocks[2], nextPosition, nextRotation);
            }
        }
       

    }

    //void AddStraight(Vector3 position, Vector3 rotation)
    //{
    //    GameObject roadBlock = Instantiate(roadStraight, position, Quaternion.identity);
    //    roadBlock.transform.Rotate(rotation);

    //    Transform[] sp = roadBlock.GetComponentsInChildren<Transform>();
    //    nextPosition = sp[2].position;
    //    nextRotation = sp[2].eulerAngles;
    //}
    
    //void AddJump(Vector3 position, Vector3 rotation)
    //{
    //    GameObject roadBlock = Instantiate(roadJump, position, Quaternion.identity);
    //    roadBlock.transform.Rotate(rotation);

    //    Transform[] sp = roadBlock.GetComponentsInChildren<Transform>();
    //    nextPosition = sp[2].position;
    //    nextRotation = sp[2].eulerAngles;
    //}
    
    //void AddTurn(Vector3 position, Vector3 rotation) {
    //    GameObject roadBlock = Instantiate(roadTurn, position, Quaternion.identity);
    //    roadBlock.transform.Rotate(rotation);
        
    //    Transform[] sp = roadBlock.GetComponentsInChildren<Transform>();
    //    nextPosition = sp[2].position;
    //    nextRotation = sp[2].eulerAngles;
    //}

    void AddBlock(RoadBlock rb, Vector3 position, Vector3 rotation)
    {
        GameObject roadBlock = Instantiate(rb.block, position, Quaternion.identity);
        roadBlock.transform.Rotate(rotation);

        Transform[] sp = roadBlock.GetComponentsInChildren<Transform>();
        nextPosition = sp[2].position;
        nextRotation = sp[2].eulerAngles;
    }
}
