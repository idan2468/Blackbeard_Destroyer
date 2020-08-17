using UnityEngine;

public class BallHandler : MonoBehaviour
{
    private bool gameStarted = false;
    [SerializeField] float xStartVelocity = 2f;
    [SerializeField] float yStartVelocity = 9f;
    private Vector3 offsetFromPaddle;
    [SerializeField] float randomFactor = .2f;
    // cache components
    [SerializeField] ControlPaddle myPaddle = null;
    [SerializeField] AudioClip[] ballSounds;
    Rigidbody2D myRigidBody;
    private const int PADDLE_COLL_SOUND = 0;
    private const int OTHER_COLL_SOUND = 1;
    [SerializeField] int MIN_SPEED_Y = 6;
    [SerializeField] int MAX_SPEED_Y = 15;

    // Start is called before the first frame update

    // Cached Components Ref

    AudioSource myAudioSource;
    void Start()
    {
        offsetFromPaddle = transform.position - myPaddle.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
        {
            UpdateStartPos();
            launchOnMouseClick();
        }
    }

    private void launchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody.velocity = new Vector2(xStartVelocity, yStartVelocity);
            gameStarted = true;
        }
    }

    private void UpdateStartPos()
    {
        transform.position = myPaddle.transform.position + offsetFromPaddle;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameStarted)
        {
            velocityTweak();
            handleSound(collision);
        }
    }
    public static bool IsWithin(float value, float minimum, float maximum)
    {
        return value >= minimum && value <= maximum;
    }
    private void velocityTweak()
    {
        Vector2 velocityTweakVector = new Vector2(UnityEngine.Random.Range(0f, randomFactor), 0);
        myRigidBody.velocity += velocityTweakVector;
        //Debug.Log(myRigidBody.velocity.ToString());
        //improveVelocityTweak();
    }

    private void improveVelocityTweak()
    {
        float newVeclocityY = myRigidBody.velocity.y;
        if (!IsWithin(newVeclocityY, MIN_SPEED_Y, MAX_SPEED_Y) && !IsWithin(newVeclocityY, -MAX_SPEED_Y, -MIN_SPEED_Y))
        {
            if (newVeclocityY < 0)
            {
                newVeclocityY = Random.Range(-MAX_SPEED_Y, -MIN_SPEED_Y);
            }
            else
            {
                newVeclocityY = Random.Range(MIN_SPEED_Y, MAX_SPEED_Y);
            }
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, newVeclocityY);
        }
    }

    private void handleSound(Collision2D collision)
    {
        int soundIndex = 1;
        if (collision.collider.tag == "Paddle")
        {
            soundIndex = 0;
        }
        var audioClip = ballSounds[soundIndex];
        myAudioSource.PlayOneShot(audioClip);
    }
    public bool isGameStarted()
    {
        return gameStarted;
    }
}
