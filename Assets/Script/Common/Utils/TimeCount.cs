using UnityEngine.Events;

namespace MyUtils{
    public class TimeCount
    {
        private UnityEvent on_End = new UnityEvent();
        public UnityEvent On_End
        {
            get
            {
                return on_End;
            }
            set
            {
                on_End = value;
            }
        }
        float countTime;
        float deltaTime;
        bool triggered=false;
        bool stop = true;

        public TimeCount()
        {
            
        }

        public TimeCount(float t )
        {
            countTime=t;
        }
        public TimeCount(float t , UnityEvent e)
        {
            countTime=t;
            on_End=e;
        }
        public void Update(float t)
        {
            if(stop)return;
            deltaTime += t;
            if(deltaTime>countTime && !triggered )
            {
                on_End?.Invoke();
                triggered = true;
                stop=true;
            }
        }
        public void Reset()
        {
            deltaTime = 0;
            triggered = false;
            stop=false;
        }

        public void SetTime(float t)
        {
            countTime=t;
        }

        public bool isStop()
        {
            return stop;
        }
    }

}