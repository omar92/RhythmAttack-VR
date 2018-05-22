using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{

    public GameEvent pauseEvent;
    public GameEvent unPauseEvent;

    public GameObject bgMusic;
    AudioSource bgSource;

    [SerializeField] private GameObject pausePanel;
    void Start()
    {
        bgSource = bgMusic.GetComponent<AudioSource>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("pause the game");
            if (!pausePanel.activeInHierarchy)
            {
                pauseEvent.Raise();
            }
            
        }
        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    ContinueGame();
        //    // 
        //}
    }
    public void Pause()
    {
        bgSource.Pause();
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        bgSource.Play();
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }
}
