using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameApp : MonoBehaviour
{
    private bool _isInit = false;

    public void Init()
    {
        this._isInit = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!this._isInit)
        {
            return;
        }
        TimerManager.GetInstance().Update();
    }
}
