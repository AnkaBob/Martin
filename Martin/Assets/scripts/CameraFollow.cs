using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    public Martin martin;
    public Vector2 offset;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {
        var destination = new Vector3(martin.transform.position.x + offset.x, martin.transform.position.y + offset.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, destination, Time.deltaTime * 5f);
    }
}
