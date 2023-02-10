using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    /*
    {
        transform.position=Vector3.MoveTowards(
            transform.position,
            target.position,
            4f*Time.deltaTime
        );
    }
    */
    {
        transform.position=Vector3.Lerp(
        transform.position,
        target.position,
        1f*Time.deltaTime
        );
    }
}
