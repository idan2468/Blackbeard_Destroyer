using UnityEngine;

public class ControlPaddle : MonoBehaviour
{
    [SerializeField] private const float MAX_X = 11.6f;
    [SerializeField] private const float MIN_X = .35f;
    [SerializeField] private const float WIDTH_RATIO = 16f;
    [SerializeField] const float speed = 6f;
    private const float PADDLE_MOUSE_OFFSET = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //KeyBoardControl();
        MouseControl();
    }

    private void MouseControl()
    {
        var mousePos = Input.mousePosition.x / Screen.width * WIDTH_RATIO;
        Vector2 paddlePos = new Vector2(getPosX(), transform.position.y);
        transform.position = paddlePos;
    }
    private float getPosX()
    {
        float mousePos = 0;
        if (FindObjectOfType<GameSession>().isAutoPlayOn())
        {
            mousePos = FindObjectOfType<BallHandler>().transform.position.x;
        }
        else
        {
            mousePos = Input.mousePosition.x / Screen.width * WIDTH_RATIO - PADDLE_MOUSE_OFFSET;
        }
        return Mathf.Clamp(mousePos, MIN_X, MAX_X);
    }

    private void KeyBoardControl()
    {
        Vector3 paddleVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        Vector3 transformAddition = paddleVec * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            var newPos = transform.position + transformAddition;
            if (newPos.x >= MIN_X)
                transform.position += transformAddition;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            var newPos = transform.position - transformAddition;
            if (newPos.x <= MAX_X)
                transform.position += transformAddition;
        }
    }
}
