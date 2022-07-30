using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, target.position.y + offset.y, target.position.z + offset.z);
    }
}
