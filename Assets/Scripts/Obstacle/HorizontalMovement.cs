using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    [SerializeField]
    Vector3[] points;

    Vector3 patrol;

    [SerializeField] float patrolSpeed;

    int index;

    public bool left, right;

    private void Start()
    {
        index = Random.Range(0, 2);
        if (index == 0) left = true;
        else if (index == 1) right = true;
    }

    void FixedUpdate()
    {
        Patrol();
    }

    void Patrol()
    {
        patrol = new Vector3(points[index].x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, patrol, patrolSpeed);
        if(Vector3.Distance(transform.position, patrol) <= 0.1f)
        {
            if (index == 0)
            {
                index++;
                left = false;
                right = true;
            }
            else if (index == 1)
            {
                index--;
                right = false;
                left = true;
            }
        }
    }

}
