using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    int frameRate = 30;

    bool gameOver = false;

    void Start()
    {
        Application.targetFrameRate = frameRate;
    }

    void Update()
    {
        if (gameOver == true)
        {
            ShowGameOver();
            Invoke("ReturnMenu", 3f);
        }
    }

    void ReturnMenu() { }

    void ShowGameOver() { }
}
