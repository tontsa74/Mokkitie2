using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlaneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ontrigger " + other.gameObject.name + " osuu " +this.name );
        if(other.gameObject.name != "car")
        {
            Destroy(other.gameObject);

        }
    }


}
