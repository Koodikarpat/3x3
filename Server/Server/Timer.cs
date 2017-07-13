using System;
using System.Timers;

namespace Server
{
    public class Timer
    {
        int turnTime = 20; //turn time in seconds
        public Game gameManager;
        System.Timers.Timer aTimer;

        public void Initialize()
        {
            aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = turnTime * 1000; // seconds to milliseconds
            Stop();
        }

        public void Start()
        {
            aTimer.Enabled = true;
        }

        public void Stop()
        {
            aTimer.Enabled = false;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Stop();
            Console.WriteLine("timer stopped");
            if (gameManager != null) gameManager.ChangeTurn();
            else Console.WriteLine("Timer: gameManager not set");
        }
    }
}
