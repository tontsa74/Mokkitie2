using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{

    [SerializeField]
    public GameObject roadStraight;

    [SerializeField]
    public GameObject roadJump;
    
    [SerializeField]
    public GameObject roadTurn;

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
        AddStraight(startPosition, startRotation);
        for (int i = 0; i < 8; i++) {
            AddStraight(nextPosition, nextRotation);
            AddTurn(nextPosition, nextRotation);
            AddStraight(nextPosition, nextRotation);
            AddJump(nextPosition, nextRotation);
            AddStraight(nextPosition, nextRotation);
        }
       

    }

    void AddStraight(Vector3 position, Vector3 rotation)
    {
        GameObject roadBlock = Instantiate(roadStraight, position, Quaternion.identity);
        roadBlock.transform.Rotate(rotation);

        Transform[] sp = roadBlock.GetComponentsInChildren<Transform>();
        nextPosition = sp[2].position;
        nextRotation = sp[2].eulerAngles;
    }
    
    void AddJump(Vector3 position, Vector3 rotation)
    {
        GameObject roadBlock = Instantiate(roadJump, position, Quaternion.identity);
        roadBlock.transform.Rotate(rotation);

        Transform[] sp = roadBlock.GetComponentsInChildren<Transform>();
        nextPosition = sp[2].position;
        nextRotation = sp[2].eulerAngles;
    }
    
    void AddTurn(Vector3 position, Vector3 rotation) {
        GameObject roadBlock = Instantiate(roadTurn, position, Quaternion.identity);
        roadBlock.transform.Rotate(rotation);
        
        Transform[] sp = roadBlock.GetComponentsInChildren<Transform>();
        nextPosition = sp[2].position;
        nextRotation = sp[2].eulerAngles;
    }
}
