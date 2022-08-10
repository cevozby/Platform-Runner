using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float speed;

    private void Start()
    {
        speed = 0f;
        StartCoroutine(Delay());
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.up * speed);
    }

    IEnumerator Delay()
    {
        float time = Random.Range(0, 1f);
        yield return new WaitForSeconds(time);
        speed = 1;
    }
}
