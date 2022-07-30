using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float slideSpeed;

    Rigidbody playerRB;
    [SerializeField] Animator playerAnim;

    public static bool isGame;

    Vector3 horizontal, vertical, direction;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        //playerAnim = GetComponent<Animator>();
        isGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isGame = true;
            vertical = Vector3.forward;
        }
        Movement();
        Anim();
    }


    void Movement()
    {
        
        if (isGame)
        {
            playerRB.velocity = Vector3.forward * speed * Time.deltaTime;
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));
            if(Mathf.Abs(transform.position.x - mouse.x) <= 0.2f)
            {
                horizontal = Vector3.zero;
            }
            else if(transform.position.x < mouse.x)
            {
                horizontal = Vector3.right;
            }
            else if (transform.position.x > mouse.x)
            {
                horizontal = Vector3.left;
            }
            transform.position = Vector3.MoveTowards(transform.position, 
                new Vector3(mouse.x, transform.position.y, transform.position.z), slideSpeed);
            //playerController.Move(horizontal * slideSpeed * Time.deltaTime);
        }
    }

    void Anim()
    {
        playerAnim.SetBool("isGame", isGame);
    }

}
