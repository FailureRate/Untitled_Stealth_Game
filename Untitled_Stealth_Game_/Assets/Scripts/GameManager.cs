using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool Obj1;
    Button StartBtn;
    Button QuitBtn;    

    static public GameManager instance = null;//so you don't have to load the whol scene again

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
        StartBtn = GameObject.Find("StartButton").GetComponent<Button>();
        GameObject temp = GameObject.Find("QuitButton");

        if (temp)
        {
            QuitBtn = temp.GetComponent<Button>();//finds it in the hiearchy

        }
        StartBtn.onClick.AddListener(StartGame);//call startgame, loading the fist screen       
        QuitBtn.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);  
    }

    public void WinGame()
    {
        SceneManager.LoadScene(2);
    }
    public void QuitGame()
    {       
        Application.Quit();//not working
    }
}
