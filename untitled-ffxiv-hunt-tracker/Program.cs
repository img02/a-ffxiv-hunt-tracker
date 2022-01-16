using System;
using System.Speech.Synthesis;
using System.Threading;
using System.Threading.Tasks;
using untitled_ffxiv_hunt_tracker.Memory;
using untitled_ffxiv_hunt_tracker.ViewModels;

namespace untitled_ffxiv_hunt_tracker
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var session = CreateSession(144);
            CancellationTokenSource ct = new CancellationTokenSource();
            session.Start(ct);

            /*while (true)
            {
                session.GetUser();
                session.SearchNearbyMobs();
                Thread.Sleep(1000);
            }*/
        }

        public static Session CreateSession(int refreshRate)
        {
            var memoryReader = new MemoryReader();
            var tts = new SpeechSynthesizer { Rate = 1, Volume = 100 };

            return new Session(memoryReader, tts, 1000/refreshRate); //1000 divided by refresh because it's actually how many milliseconds before refresh
        }
    }
}
