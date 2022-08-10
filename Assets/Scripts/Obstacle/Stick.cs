using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] float force;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Character push = collision.gameObject.GetComponent<Character>();
            push.Push(force, transform.forward);
        }
    }

}
