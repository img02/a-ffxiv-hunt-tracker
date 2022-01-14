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
            var session = CreateSession();

            while (true)
            {
                session.GetUser();
                session.SearchNearbyMobs();
                Thread.Sleep(1000);
            }
        }

        public static Session CreateSession()
        {
            var memoryReader = new MemoryReader();
            var tts = new SpeechSynthesizer { Rate = 1, Volume = 100 };

            return new Session(memoryReader, tts);
        }
    }
}
