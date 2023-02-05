using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    [SerializeField] private Button pauseButton;
    [SerializeField] private TMP_Text textPlayer;
    [SerializeField] private TMP_Text textScore;
    [SerializeField] private TMP_Text textTime;
    [SerializeField] private TMP_Text playTimeText;
    [SerializeField] private Sprite playButtonSprite, pauseButtonSprite;

    [SerializeField] private PlayerMovement playerMovement;


    [SerializeField] private GameObject winPanel, playerPanel, scorePanel, timePanel, losePanel,startGamePanel;

    [SerializeField] private EnemyAl enemyAl;

    [SerializeField] private float startingTime = 3f;
    [SerializeField] private float currentPlayGameTime = 3f;


    public int countPlayer;

    private float currentTime;
    

    public bool isAlive = true;

    private bool paused = false;

    private void Awake()
    {
        
    }

    void Start()
    {
        Instance = this;
        currentTime = startingTime;
        pauseButton.image.sprite = pauseButtonSprite;
    }


    void Update()
    {
        LoadingGame();
        ScoreText();
        TimeText();
        PlayerText();
    }

    private void ScoreText()
    {

        textScore.text = enemyAl.forcePower.ToString();
    }

    private void TimeText()
    {
        currentTime -= 1 * Time.deltaTime;

        textTime.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            Time.timeScale = 0;

            if (Time.timeScale == 0)
            {
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
            }
        }
    }

    private void PlayerText()
    {
        textPlayer.text = countPlayer.ToString();
        if (countPlayer == 1)
        {
            if (!isAlive)
            {
                scorePanel.SetActive(false);
                timePanel.SetActive(false);
                playerPanel.SetActive(false);
                winPanel.SetActive(true);
                playerMovement.enabled = false;


            }
            else
            {
                scorePanel.SetActive(false);
                timePanel.SetActive(false);
                playerPanel.SetActive(false);
                losePanel.SetActive(true);
                playerMovement.enabled = false;

            }
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void PauseButton()
    {


        if (!paused)
        {
            Time.timeScale = 0f;
            pauseButton.image.sprite = playButtonSprite;
            paused = true;
        }
        else
        {
            Time.timeScale = 1f;
            pauseButton.image.sprite = pauseButtonSprite;
            paused = false;
        }

    }

    private void LoadingGame()
    {
        enemyAl.isGameStarted = false;
        playerMovement.enabled = false;
        currentPlayGameTime -= 1 * Time.deltaTime;

        playTimeText.text = currentPlayGameTime.ToString("0");
        scorePanel.SetActive(false);
        timePanel.SetActive(false);
        playerPanel.SetActive(false);
        pauseButton.gameObject.SetActive(false);

        if (currentPlayGameTime <= 0f)
        {
            scorePanel.SetActive(true);
            timePanel.SetActive(true);
            playerPanel.SetActive(true);
            pauseButton.gameObject.SetActive(true);
            startGamePanel.SetActive(false);
            playerMovement.enabled = true;
            enemyAl.isGameStarted = true;

        }

    }
}
