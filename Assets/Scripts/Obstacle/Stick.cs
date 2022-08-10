using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] float force;

    //When collide any character, take its script and call the function
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Character push = collision.gameObject.GetComponent<Character>();
            push.Push(force, transform.forward);
        }
    }

}
