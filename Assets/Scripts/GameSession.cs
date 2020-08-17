using System;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [Range(.1f, 10f)] [SerializeField] float gameSpeed = 1;
    [SerializeField] int currScore = 0;
    [SerializeField] int blockScore = 50;
    [SerializeField] TextMeshProUGUI scoreText = null;
    [SerializeField] TextMeshProUGUI timer = null;
    [SerializeField] bool isAutoPlayMode = false;
    [SerializeField] float currTimer;
    [SerializeField] bool useTimer = true;
    [SerializeField] float tweakCountDown = 0;
    [SerializeField] float activeTweakCountDown = 0;
    BallHandler myBall;
    bool isTweakOn = false;
    private void Awake()
    {
        saveGameSessionBetweenScences();
    }

    private void saveGameSessionBetweenScences()
    {
        int numOfGameStatusObjcs = FindObjectsOfType<GameSession>().Length;
        if (numOfGameStatusObjcs > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = currScore.ToString();
        currTimer = tweakCountDown;
        timer.text = currTimer.ToString("0");
        myBall = FindObjectOfType<BallHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        if(myBall.isGameStarted())
        {
            if(useTimer)
                timerHandler();
        }
    }

    private void timerHandler()
    {
        currTimer -= 1 * Time.deltaTime;
        if (currTimer < 5.5)
        {
            timer.color = Color.red;
        }
        if (currTimer <= 0)
        {
            handleTweak();
            currTimer = isTweakOn ? activeTweakCountDown: tweakCountDown;
        }
        timer.text = currTimer.ToString("0");
    }

    private void handleTweak()
    {
        if(!isTweakOn)
        {
            isTweakOn = true;
            Camera.main.transform.rotation = new Quaternion(0,0,180,0);
        }
        else
        {
            Camera.main.transform.rotation = new Quaternion(0, 0, 0, 0);
            isTweakOn = false;
        }
    }

    public void addScore()
    {
        currScore += blockScore;
        scoreText.text = currScore.ToString();
    }
    public void destroyGameSession()
    {
        Destroy(gameObject);
    }
    public bool isAutoPlayOn()
    {
        return this.isAutoPlayMode;
    }
}
