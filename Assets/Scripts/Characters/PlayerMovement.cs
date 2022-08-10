using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float slideSpeed;

    Rigidbody playerRB;
    [SerializeField] Animator playerAnim;

    Vector3 horizontal, vertical, direction;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameControl.instance.isEnd)
        {
            GameControl.instance.isGame = true;
            vertical = Vector3.forward;
        }
        
        Movement();
        Anim();
    }


    void Movement()
    {
        
        if (GameControl.instance.isGame && !GameControl.instance.isEnd)
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 5f));

                if (Mathf.Abs(transform.position.x - mouse.x) <= 0.2f)
                {
                    horizontal = Vector3.zero;
                }
                else if (transform.position.x < mouse.x)
                {
                    horizontal = Vector3.right;
                }
                else if (transform.position.x > mouse.x)
                {
                    horizontal = Vector3.left;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                horizontal = Vector3.zero;
            }
            

            playerRB.velocity = new Vector3(horizontal.x * slideSpeed, playerRB.velocity.y, vertical.z * speed);
        }
        else
        {
            playerRB.velocity = Vector3.zero;
        }
    }

    void Anim()
    {
        if (!GameControl.instance.isEnd)
        {
            playerAnim.SetBool("isGame", GameControl.instance.isGame);
        }
        else
        {
            playerAnim.SetBool("isGame", false);
        }
        
    }



}
