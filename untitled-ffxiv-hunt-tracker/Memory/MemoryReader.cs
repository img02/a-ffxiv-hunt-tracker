using Sharlayan;
using Sharlayan.Core;
using Sharlayan.Enums;
using Sharlayan.Models;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using untitled_ffxiv_hunt_tracker.Utilities;

namespace untitled_ffxiv_hunt_tracker.Memory
{
    public class MemoryReader
    {
        public MemoryHandler MemoryHandler { get; set; }
        public bool CanGetProcess => _process != null; //_process.HasExited doesn't seem to work? idk
        public bool ProcessHasExited => _process?.HasExited == null || _process.HasExited;
        private Reader _reader;
        private Process _process;


        public bool Setup()
        {
            // DX11
            Process[] processes = Process.GetProcessesByName("ffxiv_dx11");
            if (processes.Length > 0)
            {

                // supported: Global, Chinese, Korean
                GameRegion gameRegion = GameRegion.Global;

                GameLanguage gameLanguage = Globals.Language;

                // whether to always hit API on start to get the latest sigs based on patchVersion, or use the local json cache (if the file doesn't exist, API will be hit)
                bool useLocalCache = true;

                // patchVersion of game, or latest
                string patchVersion = "latest";
                _process = processes[0];

                ProcessModel processModel = new ProcessModel
                {
                    Process = _process

                };

                SharlayanConfiguration configuration = new SharlayanConfiguration
                {
                    GameLanguage = gameLanguage,
                    ProcessModel = processModel
                };

                MemoryHandler = SharlayanMemoryManager.Instance.AddHandler(configuration);
                MemoryHandler = SharlayanMemoryManager.Instance.GetHandler(processModel.ProcessID);
                _reader = MemoryHandler.Reader;

                Console.WriteLine(processModel.ProcessID);

                //wait a mo' for things to load
                Thread.Sleep(1000);

                return true;
            }

            return false;
        }

        public ActorItem GetUser()
        {
            if (MemoryHandler.Reader.CanGetActors())
            {
                var actors = _reader.GetActors();

                var p = _reader.GetCurrentPlayer()?.Entity ?? new ActorItem() { Name = "not found" };

                return p;
            }
            else
            {
                Console.WriteLine("actions " + _reader.CanGetActions());
                Console.WriteLine("actors " + _reader.CanGetActors());
                Console.WriteLine("chat log " + _reader.CanGetChatLog());
                Console.WriteLine("can't get actors");
                return null;
            }
        }

        public ConcurrentDictionary<uint, ActorItem> GetMobs()
        {
            if (MemoryHandler.Reader.CanGetActors())
            {
                var actors = _reader.GetActors();
                return actors.CurrentMonsters;
            }
            else
            {
                Console.WriteLine("Can't get actors");
                return null;
            }
        }
    }
}