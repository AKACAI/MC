using UnityEngine;

public class GameApp : MonoBehaviour
{
    private bool _isInit = false;

    public void Init()
    {
        this._isInit = true;
    }

    void Update()
    {
        if (!this._isInit)
        {
            return;
        }
        TimerManager.GetInstance().Update();
    }
}
