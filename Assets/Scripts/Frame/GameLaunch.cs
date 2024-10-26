using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class GameLaunch : MonoBehaviour
{
    private GameApp game;

    void Start()
    {
        this.InitManagers();
        this.game = this.GetComponent<GameApp>();
        this.game.Init();
    }

    private void InitManagers()
    {
        TimerManager.GetInstance().Init();
    }
}
