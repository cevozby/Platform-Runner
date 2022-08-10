using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    Vector3 startPos;

    [SerializeField] Rigidbody characterRB;

    public bool isRun;


    // Start is called before the first frame update
    void Awake()
    {
        characterRB = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControl.instance.paintWin)
        {
            transform.position = startPos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("HorizontalObstacle")
            || collision.gameObject.CompareTag("Rotator") || collision.gameObject.CompareTag("DoubleRotator") 
            || collision.gameObject.CompareTag("HalfDonut"))
        {
            Reset();
        }
        if (collision.gameObject.CompareTag("Finish"))
        {
            Finish();
            //GameControl.instance.level++;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Down"))
        {
            Reset();
        }
    }


    public virtual void Finish()
    {
        characterRB.velocity = Vector3.zero;
    }

    public virtual void Reset()
    {
        StartCoroutine(ResetTimer());
    }

    public void Push(float force, Vector3 dir)
    {
        characterRB.AddForce(dir * force, ForceMode.Impulse);
        Debug.Log("Push");
    }

    public void Bounce(float force, int dir)
    {
        characterRB.AddForce(Vector3.right * force * dir);
        
    }

    IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = startPos;
    }

}
