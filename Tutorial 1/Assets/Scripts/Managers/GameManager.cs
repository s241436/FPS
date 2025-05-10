using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public HighScores highScores;
    // Reference to the overlay Text to display winning text, etc
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    public GameObject highScorePanel;
    public TextMeshProUGUI highScoresText;

    public Button newGameButton;
    public Button highScoresButton;
    public Button returnButton;


    public TargetHealth[] targets;
    public GameObject player;
    public Camera worldCamera;

    // crosshair UI
    public Canvas crosshair;

    public float startTimerAmount = 3;
    private float startTimer;

    public float targetActivateTimerAmount = 1;
    private float targetActivateTimer;

    public float gameTimerAmount = 60;
    private float gameTimer;

    private int score = 0;

    private void Awake()
    {
        gameState = GameState.GameOver;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        player.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GameManager = this;
            targets[i].gameObject.SetActive(false);

        }
        startTimer = startTimerAmount;
        messageText.text = "Press Enter to Start";
        timerText.text = "";
        scoreText.text = "";

        highScorePanel.gameObject.SetActive(false);
        newGameButton.gameObject.SetActive(true);
        highScoresButton.gameObject.SetActive(true);
        crosshair.gameObject.SetActive(false);



    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }

        switch (gameState)
        {
            case GameState.Start:
                GameStateStart();
                break;
            case GameState.Playing:
                GameStatePlaying();
                break;
            case GameState.GameOver:
                GameStateGameOver();
                break;
        }
    }
    
    private void GameStateStart()
    {
        startTimer -= Time.deltaTime;

        messageText.text = "Get Ready " + (int)(startTimer + 1);

        if (startTimer < 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
            gameState = GameState.Playing;
            gameTimer = gameTimerAmount;
            startTimer = startTimerAmount;
            score = 0;

            highScorePanel.gameObject.SetActive(false);
            newGameButton.gameObject.SetActive(false);
            highScoresButton.gameObject.SetActive(false);

            player.SetActive(true);
            worldCamera.gameObject.SetActive(false);
            messageText.gameObject.SetActive(false);
            crosshair.gameObject.SetActive(true);
        }
    }


    private void GameStatePlaying()
    {
        gameTimer -= Time.deltaTime;
        int seconds = Mathf.RoundToInt(gameTimer);
        timerText.text = string.Format("Time: {0:D2}:{1:D2}", (seconds/60), (seconds%60));

        if (gameTimer < 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Debug.Log("Game Over Score: " + score);
            gameState = GameState.GameOver;
            player.SetActive(false);
            worldCamera.gameObject.SetActive(true);
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].gameObject.SetActive(false);
            }
            highScores.AddScore(score);
            highScores.SaveScoresToFile();
            newGameButton.gameObject.SetActive(true);
            highScoresButton.gameObject.SetActive(true);
            crosshair.gameObject.SetActive(false);

        }

        //Timer before activating target.
        targetActivateTimer -= Time.deltaTime;
        if (targetActivateTimer <= 0)
        {
            ActivateRandomTarget();
            targetActivateTimer = targetActivateTimerAmount;
        }
    }

    private void GameStateGameOver()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            gameState = GameState.Start;
            timerText.text = "";
            scoreText.text = "";
        }
    }

    //Randomly activates a target.
    private void ActivateRandomTarget()
    {
        int randomIndex = Random.Range(0, targets.Length);
        targets[randomIndex].gameObject.SetActive(true);
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }


    public enum GameState
    {
        Start,
        Playing,
        GameOver
    }

    private GameState gameState;

    public GameState State { get { return gameState; } }

    
    public void OnNewGame()
    {
        gameState = GameState.Start;
    }

    public void OnHighScores()
    {
        messageText.text = "";

        highScoresButton.gameObject.SetActive(false);
        highScorePanel.gameObject.SetActive(true);

        newGameButton.gameObject.SetActive(false);
        returnButton.gameObject.SetActive(true);

        string text = "";
        for (int i = 0; i < highScores.scores.Length; i++)
        {
            text += highScores.scores[i] + "\n";
        }
        highScoresText.text = text;

    }

    public void returnbutton()
    {
        highScorePanel.gameObject.SetActive(false);
        highScoresButton.gameObject.SetActive(true);
        newGameButton.gameObject.SetActive(true);
        returnButton.gameObject.SetActive(false);
    }

}
