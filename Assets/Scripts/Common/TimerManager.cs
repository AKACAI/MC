using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    private MinHeap<Timer> _timerList;
    private Timer[] _timerPool;
    private int _poolSize;
    private int _curTailIdx;
    private const int DEFAULT_POOL_SIZE = 16;
    /**一次更新时最多执行多少个定时器*/
    private const int TIMER_EXECUTE_COUNT = 20;

    public void Init()
    {
        Comparer<Timer> comparer = Comparer<Timer>.Create(Timer.comparison);
        this._timerList = new MinHeap<Timer>(comparer);
        this._timerPool = new Timer[DEFAULT_POOL_SIZE];
        this._poolSize = DEFAULT_POOL_SIZE;
        this._curTailIdx = -1;
    }

    public void Update()
    {
        int curExecuteCount = 0;
        int curTime = Utils.GetTimeStamp();
        Timer timer = this._timerList.Top();
        while (timer != null && timer.IsOutTime(curTime) && curExecuteCount < TIMER_EXECUTE_COUNT)
        {
            if (timer.isRemove)
            {
                this._timerList.Dequeue();
                timer = this._timerList.Top();
                this.GivebackTimer(timer);
            }
            else
            {
                this._timerList.Dequeue();
                timer.func.Invoke(timer.data);
                if (timer.isLoop)
                {
                    this.AddTimer(timer.triggerTime, true, timer.func, timer.target, timer.data, timer.token);
                }
                timer = this._timerList.Top();
                curExecuteCount++;
            }
        }
    }

    /** 添加一个定时器（ms） */
    public void AddTimer(int time, bool isLoop = false, TimerFunc func = null, object target = null, object[] data = null, string token = "")
    {
        if (time == 0)
        {
            return;
        }
        int curTime = Utils.GetTimeStamp();
        Timer timer = this.GetTimer();
        timer.triggerTime = time + curTime;
        timer.isLoop = isLoop;
        timer.func = func;
        timer.target = target;
        timer.data = data;
        timer.token = token;
        timer.isRemove = false;
        this._timerList.Enqueue(timer);
    }

    public void RemoveTimer(TimerFunc func)
    {
        Timer item = this._timerList.Find((Timer timer) =>
        {
            if (timer.func == func)
            {
                return true;
            }
            return false;
        });
        if (item != null)
        {
            item.isRemove = true;
        }
    }

    public void RemoveTimer(string token)
    {
        Timer item = this._timerList.Find((Timer timer) =>
        {
            if (timer.token == token)
            {
                return true;
            }
            return false;
        });
        if (item != null)
        {
            item.isRemove = true;
        }
    }

    /**尝试从对象池中获取到一个定时器*/
    private Timer GetTimer()
    {
        if (this._curTailIdx == -1)
        {
            // 说明对象池里没有了
            Timer timer = new Timer();
            return timer;
        }
        else
        {
            Timer timer = this._timerPool[this._curTailIdx];
            this._timerPool[this._curTailIdx] = default(Timer);
            this._curTailIdx--;
            return timer;
        }
    }

    /**将定时器放回对象池中，目前数量有限制，超出不放回*/
    private void GivebackTimer(Timer timer)
    {
        if (timer == null)
        {
            return;
        }
        if (this._curTailIdx == this._timerPool.Length - 1)
        {
            return;
        }
        this._curTailIdx++;
        this._timerPool[this._curTailIdx] = timer;
    }
}
