using UnityEngine;

public class Opponent : Character
{
    Animator opponentAnim;
    Rigidbody opponentRB;
    [SerializeField] float speed;
    float horizontal;
    [SerializeField] float slideSpeed;

    Transform obstacle;
    Transform horizontalObstacle;
    Transform rotatorObstacle;
    Transform halfDonutObstacle;

    float angle;
    float staticXPos;
    float horizontalXPos;
    float rotatorXPos;
    float halfDonutXPos;

    bool left, right;

    bool run;

    // Start is called before the first frame update
    void Start()
    {
        opponentAnim = GetComponent<Animator>();
        opponentRB = GetComponent<Rigidbody>();
        speed = Random.Range(0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            run = true;
        }
        
        Movement();
        Anim();
        //If opponent see any obstacle, call function
        if(obstacle != null)
        {
            Obstacle(staticXPos);
        }
        if(horizontalObstacle != null)
        {
            HorizontalObstacle(horizontalXPos);
        }
        if(rotatorObstacle != null)
        {
            Rotator(rotatorXPos);
        }
        if (halfDonutObstacle != null)
        {
            HalfDonut(halfDonutXPos);
        }

    }

    void Anim()
    {
        if (run)
        {
            opponentAnim.SetBool("isGame", run);
        }
        else
        {
            opponentAnim.SetBool("isGame", run);
        }
    }

    void Movement()
    {
        //Whenever the opponent approaches the edge of the ground, stop it.
        if (transform.position.x > 0.86f || transform.position.x < -0.86f)
        {
            horizontal = 0f;
            if (transform.position.x >= 0.86f)
            {
                transform.position = Vector3.MoveTowards(transform.position, 
                    new Vector3(0.83f, transform.position.y, transform.position.z), 0.05f);
            }
            else if(transform.position.x <= -0.86f)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(-0.83f, transform.position.y, transform.position.z), 0.05f);
            }
            
        }
        if (run)
        {
            opponentRB.velocity = new Vector3(horizontal * slideSpeed, opponentRB.velocity.y, speed);
        }
    }

    public override void Finish()
    {
        base.Finish();
        opponentRB.velocity = Vector3.zero;
        run = false;
    }

    public override void Reset()
    {
        base.Reset();
        obstacle = null;
        horizontalObstacle = null;
        rotatorObstacle = null;
        halfDonutObstacle = null;


        horizontal = 0f;
        staticXPos = 0f;
        horizontalXPos = 0f;
        rotatorXPos = 0f;
        halfDonutXPos = 0f;
        angle = 0f;

        left = false;
        right = false;
    }

    //Give obstacle information and calculate which route is better
    #region StaticObstacle
    float ObstaclePos()
    {
        float xPos = 0f;
        if (obstacle.position.x > 0f)
        {
            xPos = Random.Range(-0.84f, 0f);
        }
        else if (obstacle.position.x < 0f)
        {
            xPos = Random.Range(0f, 0.84f);
        }
        else if (obstacle.position.x == 0f)
        {
            int dir = Random.Range(0, 2);
            switch (dir)
            {
                case 0:
                    xPos = Random.Range(-0.84f, -0.5f);
                    break;
                case 1:
                    xPos = Random.Range(0.5f, 0.84f);
                    break;
                default:
                    break;
            }
        }
        return xPos;
    }

    void Obstacle(float xPos)
    {
        
        if(Mathf.Abs(transform.position.x - xPos) < 0.05f || transform.position.x < -0.84f || transform.position.x > 0.84f)
        {
            horizontal = 0f;
        }
        else if(transform.position.x < xPos)
        {
            horizontal = 1f;
        }
        else if (transform.position.x > xPos)
        {
            horizontal = -1f;
        }
        if(transform.position.z >= obstacle.position.z)
        {
            obstacle = null;
            staticXPos = 0f;
        }
    }
    #endregion

    #region HorizontalObstacle
    float HorizontalPos()
    {
        float xPos = 0f;
        if (right)
        {
            if (horizontalObstacle.position.x >= 0.3f)
            {
                xPos = Random.Range(-0.84f, 0f);
            }
            else if (horizontalObstacle.position.x < 0.3f)
            {
                xPos = Random.Range(0.3f, 0.84f);
            }

        }
        else if (left)
        {
            if (horizontalObstacle.position.x <= -0.3f)
            {
                xPos = Random.Range(0.2f, 0.84f);
            }
            else if (horizontalObstacle.position.x > -0.3f)
            {
                xPos = Random.Range(-0.3f, -0.84f);
            }
        }

        return xPos;
    }

    void HorizontalObstacle(float xPos)
    {
        
        if (Mathf.Abs(transform.position.x - xPos) < 0.05f || transform.position.x < -0.84f || transform.position.x > 0.84f)
        {
            horizontal = 0f;
        }
        else if (transform.position.x < xPos)
        {
            horizontal = 1f;
        }
        else if (transform.position.x > xPos)
        {
            horizontal = -1f;
        }

        if (horizontalObstacle.position.z <= transform.position.z)
        {
            horizontalObstacle = null;
            horizontalXPos = 0f;
            left = false;
            right = false;
        }

    }
    #endregion

    #region Rotator
    float RotatorPos()
    {
        float xPos = 0f;
        if((angle >= 0f && angle <= 160f) || (angle > 300f && angle <= 360f))
        {
            xPos = Random.Range(-0.84f, -0.6f);
        }
        else if((angle > 160f && angle <= 300f))
        {
            xPos = Random.Range(0.6f, 0.84f);
        }
        return xPos;
    }

    void Rotator(float xPos)
    {
        if (Mathf.Abs(transform.position.x - xPos) < 0.05f || transform.position.x < -0.84f || transform.position.x > 0.84f)
        {
            horizontal = 0f;
        }
        else if (transform.position.x < xPos)
        {
            horizontal = 1f;
        }
        else if (transform.position.x > xPos)
        {
            horizontal = -1f;
        }
        if (transform.position.z >= rotatorObstacle.position.z)
        {
            rotatorObstacle = null;
        }
    }
    #endregion

    #region HalfDonut
    float HalfDonutPos()
    {
        float xPos = 0f;
        if(halfDonutObstacle.position.x < 0f)
        {
            xPos = Random.Range(0.1f, 0.84f);
        }
        else if(halfDonutObstacle.position.x > 0f)
        {
            xPos = Random.Range(-0.84f, -0.1f);
        }
        return xPos;
    }
    void HalfDonut(float xPos)
    {
        if (Mathf.Abs(transform.position.x - xPos) < 0.05f || transform.position.x < -0.84f || transform.position.x > 0.84f)
        {
            horizontal = 0f;
        }
        else if (transform.position.x < xPos)
        {
            horizontal = 1f;
        }
        else if (transform.position.x > xPos)
        {
            horizontal = -1f;
        }
        if (transform.position.z >= halfDonutObstacle.position.z)
        {
            halfDonutObstacle = null;
        }
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            obstacle = other.gameObject.transform;
            staticXPos = ObstaclePos();
        }


        if (other.gameObject.CompareTag("HorizontalObstacle"))
        {
            horizontalObstacle = other.gameObject.transform;
            HorizontalMovement directionCheck = other.GetComponent<HorizontalMovement>();

            if (directionCheck.left)
            {
                left = false;
                right = true;
            }
            else if (directionCheck.right)
            {
                right = false;
                left = true;
            }
            horizontalXPos = HorizontalPos();
        }

        if (other.gameObject.CompareTag("Rotator"))
        {
            rotatorObstacle = other.gameObject.transform;
            angle = other.gameObject.transform.eulerAngles.y;
            rotatorXPos = RotatorPos();
        }

        if (other.gameObject.CompareTag("HalfDonut"))
        {
            halfDonutObstacle = other.gameObject.transform;
            halfDonutXPos = HalfDonutPos();
        }
    }

}
