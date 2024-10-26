

public delegate void TimerFunc(params object[] list);

public class Timer
{
    /**触发的时间戳（ms)*/
    public int triggerTime;
    /**是否循环*/
    public bool isLoop;
    /**触发的回调*/
    public TimerFunc func;
    /**触发时用的参数列表*/
    public object[] data;
    /**触发的对象*/
    public object target;
    /**定时器的标识符*/
    public string token;
    /**待释放的脏标记*/
    public bool isRemove;

    public Timer()
    {
        this.triggerTime = 0;
        this.isLoop = false;
        this.func = null;
        this.data = null;
        this.target = null;
    }

    /**是否超出了该定时器的时间*/
    public bool IsOutTime(int compareTime)
    {
        return compareTime >= this.triggerTime;
    }

    public static int comparison(Timer a, Timer b)
    {
        if (a.triggerTime > b.triggerTime)
        {
            return -1;
        }
        else if (a.triggerTime < b.triggerTime)
        {
            return 1;
        }
        return 0;
    }
}
