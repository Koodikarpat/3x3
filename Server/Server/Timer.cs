using System;
using System.Timers;

namespace Server
{
    public class Timer
    {
        int turnTime = 20; //turn time in seconds
        public static Game gameManager;

        public void Start()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = turnTime * 1000; // seconds to milliseconds
            aTimer.Enabled = true;
        }

        // Specify what you want to happen when the Elapsed event is raised.
        private static void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            gameManager.ChangeTurn();
        }
    }
}
