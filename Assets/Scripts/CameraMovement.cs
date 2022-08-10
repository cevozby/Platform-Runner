using UnityEngine;
using DG.Tweening;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Transform wall;
    [SerializeField] Vector3 offset;

    [SerializeField] float distance = 3.9f;

    Quaternion startRotate;

    private void Start()
    {
        startRotate = transform.rotation;
    }

    private void Update()
    {
        if (GameControl.instance.isEnd && GameControl.instance.level == 0)
        {
            transform.DOMoveZ(wall.position.z - distance, 0.5f);
            transform.DORotate(Vector3.zero, 0.5f);
        }
        
    }

    void LateUpdate()
    {
        if (!GameControl.instance.isEnd)
        {
            transform.rotation = startRotate;
            transform.position = new Vector3(transform.position.x, target.position.y + offset.y, target.position.z + offset.z);
        }
        
    }

    
}
