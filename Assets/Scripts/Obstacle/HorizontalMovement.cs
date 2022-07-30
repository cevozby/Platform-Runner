using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField] Vector3 pointA;
    [SerializeField] Vector3 pointB;

    [SerializeField]
    Vector3[] points;

    [SerializeField] float patrolSpeed;

    int index;

    private void Start()
    {
        index = Random.Range(0, 2);
        
    }

    void FixedUpdate()
    {
        Patrol();
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, points[index], patrolSpeed);
        if(Vector3.Distance(transform.position, points[index]) <= 0.1f)
        {
            if (index == 0) index++;
            else if (index == 1) index--;
        }
    }

}
