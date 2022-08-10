using System.Collections;
using UnityEngine;

public class HalfDonut : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distance;
    [SerializeField] float duration;
    float time;
    [Range(-1,1)]
    [SerializeField] int dir;
    [Range(-1,1)]
    [SerializeField] int forceDir;
    Vector3 startPos;
    Vector3 finishPos;
    Vector3 target;

    [SerializeField] float force;
    Rigidbody characterRB;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0f;
        StartCoroutine(Delay());
        distance = 0.131f;
        duration = 1f;
        time = duration;
        startPos = transform.position;
        finishPos = new Vector3(transform.position.x + (distance * dir), transform.position.y, transform.position.z);
        target = finishPos;

        //characterRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Patrol();
    }

    void Patrol()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, target, speed);
        if (Vector3.Distance(transform.position, startPos) <= 0.01f)
        {
            time -= Time.deltaTime;
            if(time <= 0f)
            {
                target = finishPos;
                time = duration;
            }
        }
        else if (Vector3.Distance(transform.position, finishPos) <= 0.01f)
        {
            target = startPos;
        }
    }
    //When collide any character, take its script and call the function
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Character bounce = collision.gameObject.GetComponent<Character>();
            bounce.Bounce(force, forceDir);
        }
    }

    IEnumerator Delay()
    {
        float time = Random.Range(0, 1f);
        yield return new WaitForSeconds(time);
        speed = 0.005f;
    }

}
