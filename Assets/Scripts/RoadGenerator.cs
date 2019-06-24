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

    Vector3 nextPosition;

    Quaternion nextQuaternion;
    Vector3 nextRotation;

    public Vector3 startPosition = new Vector3(0, 04083685, 0);

    public Vector3 startRotation = new Vector3(-90, 0, 0);


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
        for (int i = 0; i < 10; i++) {
            AddStraight(nextPosition, nextRotation);
            AddJump(nextPosition, nextRotation);
            // AddTurn(nextPosition, startRotation + new Vector3(0, -45, 0));
            AddTurn(nextPosition, nextRotation);
        }
       

    }

    void AddStraight(Vector3 position, Vector3 rotation) {
        GameObject roadBlock = Instantiate(roadStraight, position, Quaternion.identity);
        roadBlock.transform.Rotate(rotation);
        // nextPosition = position + new Vector3(0, 0, 7.11f);
        Transform[] sp = roadBlock.GetComponentsInChildren<Transform>();
        print(sp[2].name);
        nextPosition = sp[2].position;
        //nextRotation = sp[2].rotation;
        nextRotation = sp[2].eulerAngles;
    }
    
    void AddJump(Vector3 position, Vector3 rotation) {
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

        // foreach(Transform tf in sp) {
        //     print(tf.name + ", Z:" + tf.rotation.z);
        // }
        
        print(sp[2].rotation.z + ", next: " + nextRotation);
    }
}
