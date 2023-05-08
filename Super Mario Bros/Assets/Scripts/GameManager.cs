using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int world { get; private set; }
    public int stage { get; private set; }
    public int lives { get; private set; }
    public int coins { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);   //maintain this (singleton) GameManager gameObject when loading different levels/scenes
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        coins = 0;

        LoadLevel(1, 1);
    }

    private void LoadLevel(int world, int scene) //world, stage
    {
        this.world = world;
        this.stage = scene;

        SceneManager.LoadScene($"{world}-{scene}");  //'$' allows string interpolation - put variables in a string
    }

    public void NextLevel()
    {
        //Todo: need logic to check if current stage is the last stage
        LoadLevel(world, stage + 1);
    }

    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    public void ResetLevel()
    {
        lives--;

        if (lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        NewGame();                              //only an example of what can be done with GameOver  
        //SceneManager.LoadScene("GameOver");    
    }

    public void AddCoin()
    {
        coins++;

        if (coins==100)
        {
            AddLife();
            coins = 0;
        }
    }

    public void AddLife()       //would probably do more than just increment lives - update UI for example
    {
        lives++;
    }
}
