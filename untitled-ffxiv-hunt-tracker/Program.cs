using System;
using System.Globalization;
using System.Net.Mime;
using System.Runtime.Intrinsics.Arm;
using System.Threading;
using System.Threading.Tasks;
using untitled_ffxiv_hunt_tracker.Factories;
using untitled_ffxiv_hunt_tracker.Memory;
using untitled_ffxiv_hunt_tracker.ViewModels;

namespace untitled_ffxiv_hunt_tracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var session = new Session();

            _ = Task.Run(() => //read chatlog for stuff, don't wait for this to finish before continuing .
            {
                while (true)
                {
                    session.ReadChatLog();
                    //Thread.Sleep(3000); if you want to only read chat log / refresh every x seconds
                }
            });

            while (true)
            {
                //i'm stupid btw.
                //Console.Clear();
                session.GetUser();
                session.SearchNearbyMobs();
                session.GetMobs();
                session.GetNearbyMobs();
                
                //await Task.Run(() => chatLog.ReadChatLog());


                Thread.Sleep(1000);
            }

            Console.ReadLine();
        }
    }

}
