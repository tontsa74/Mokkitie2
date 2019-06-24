using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{

    [SerializeField]
    public GameObject roadStraight;

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
        Vector3 place = new Vector3(0, 0.04083685f ,0);
        GameObject roadBlock = Instantiate(roadStraight, place, Quaternion.identity);
        roadBlock.transform.Rotate(-90,0,0);
        

    }
}
