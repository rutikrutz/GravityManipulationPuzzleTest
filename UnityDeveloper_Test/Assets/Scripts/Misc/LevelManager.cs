using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static int PickupCount = 0;
    public  float Timer;
    private float TotalTime = 120;
    private int Minute;
    private int Seconds;

    public TextMeshProUGUI Point_txt;
    public TextMeshProUGUI Time_txt;

    public GameObject GameOverScreen;
    public GameObject GameWinScreen;
    public GameObject MainCanvas;
    public GameObject mainplayer;
    public float MaxDeadYZone;
    public bool bGameWin = false;
    public bool bGameOver = false;

    void Start()
    {
        PickupCount = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    private void LateUpdate()
    {
       TotalTime -= Time.deltaTime;
        Minute = Mathf.FloorToInt(TotalTime / 60);
        Seconds = Mathf.FloorToInt(TotalTime % 60);
        Minute = Mathf.Clamp(Minute, 0, 2);
        Seconds = Mathf.Clamp(Seconds, 0, 60);
        Time_txt.text = Minute.ToString("00") + ":" + Seconds.ToString("00");

    }
    // Update is called once per frame
    void Update()
    {
        
        Point_txt.text = PickupCount.ToString();
        GameOver();
        gameWin();


        
    }
    void FallDamage()
    {
        float DeadDist =Vector3.Distance( mainplayer.transform.position, this.transform.position);
        if(DeadDist > MaxDeadYZone)
        {
            MainCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameOverScreen.SetActive(true);
        }
    }
    void GameOver()
    {
        if(TotalTime <= 0)
        {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true ;
            GameOverScreen.SetActive(true);
//Time.timeScale = 0;
            MainCanvas.SetActive(false);
        }
        FallDamage();
        if (bGameOver)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameOverScreen.SetActive(true);
            MainCanvas.SetActive(false);

            // Time.timeScale = 0;
        }
    }
    void gameWin()
    {
        if(PickupCount >= 6)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameWinScreen.SetActive(true);
            MainCanvas.SetActive(false);

        }
        if (bGameWin)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameWinScreen.SetActive(true);
            MainCanvas.SetActive(false);

        }
    }
    public void Restart_level()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit_Application()
    {
        Application.Quit();
    }
}
