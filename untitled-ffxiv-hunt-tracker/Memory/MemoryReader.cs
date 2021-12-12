using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Sharlayan;
using Sharlayan.Core;
using Sharlayan.Enums;
using Sharlayan.Models;
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

                /*//_memoryHandler.Scanner.Locations.Clear(); idk what this does
                //signature for current world, from FFXIVAPP discord, user paru, 
                //https://discord.com/channels/45715488583450625/330209806021296138/563853091816865793
                Signature[] signatures = new Signature[]{
                   new Signature{
                    ASMSignature = true,
                    Key = "WORLD",
                    PointerPath = new List<long>{-13, 0, -112, 0},
                    Value= "40534883EC40488B05********488BD98B401485C00F"
                    }
                };
                _memoryHandler.Scanner.LoadOffsets(signatures);*/

                //_memoryHandler.Scanner.Locations.Clear(); idk what this does
                //this seems to correspond to the 'Welcome to WORLD.' message, has a period and change with that message.
                //doesn't give world on first log-on
                /* Signature[] signatures = new Signature[]{
                    new Signature{
                     Key = "CURRENTWORLD",
                     PointerPath = new List<long>
                     {
                         0x1DC0CDD

                     }
                     }
                 };
                 _memoryHandler.Scanner.LoadOffsets(signatures);*/



                Signature[] signatures = new Signature[]{
                   new Signature{
                    Key = "EORZEANTIME",
                    PointerPath = new List<long>
                    {
                        //0x01DB3400
                        0x01DB3400,
                        0x30,
                        0x70,
                        0x1F8,
                        0x20,
                        0x10,
                        4
                    }
                   }
                };
                MemoryHandler.Scanner.LoadOffsets(signatures);



                Console.WriteLine(processModel.ProcessID);

                //wait a mo' for things to load
                Thread.Sleep(2000);

                return true;
            }

            return false;
        }

        public ActorItem GetUser()
        { //change this to canGetUser or something and run it in main to check or something
            //while (!CanGetUser)
            //Thread.Sleep(1000)
            if (MemoryHandler.Reader.CanGetActors())
            {
                var actors = _reader.GetActors();

                var p = _reader.GetCurrentPlayer()?.Entity ?? new ActorItem() { Name = "not found" };

                /* Console.WriteLine(
                         $"Name: {p.Name}, " +
                         $"Pos: ({ConvertPosArr(p.X)}, " +
                         $"{ConvertPosArr(p.Y)}), " +
                         $"World: {GetWorld()} " +
                         $"Map Territory: {p.MapTerritory} " +
                         $"|{Sharlayan.Utilities.ZoneLookup.GetZoneInfo(p.MapTerritory).Name.English}| " +
                         $"MapID: {p.MapID} " +
                         $"Map Index: {p.MapIndex}");*/

                /*var time = Encoding.Default.GetString(
                    _memoryHandler.GetByteArray(_memoryHandler.Scanner.Locations["TIME"],5));
                Console.WriteLine(time);*/

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
            //get a list iof mob names/ids from a json file or something
            if (MemoryHandler.Reader.CanGetActors())
            {
                var actors = _reader.GetActors();

#if DEBUG
                //print mobs with more than xx hp.
                foreach (var actorsCurrentMonster in actors.CurrentMonsters)
                {
                    /*if (actorsCurrentMonster.Value.HPMax > 3000000)
                        Console.WriteLine(
                            $"MemoryReader -> GetMobs" + 
                            "Name: {actorsCurrentMonster.Value.Name}, " +
                            $"Pos: ({ConvertPosArr(actorsCurrentMonster.Value.X)}, " +
                            $"{ConvertPosArr(actorsCurrentMonster.Value.Y)}), " +
                            $"ModelID : {actorsCurrentMonster.Value.ModelID}, " +
                            $"World: {GetWorld()}");*/

                    /*
                     *get mob names from json file
                     * foreach (Mob mob in Json File)
                     *if (actorscurrentMonster.Value.ModelID == mob.ModelID
                     * Add to a list named after the current world, if that list already exists, use that.
                     */
                }
#endif

                return actors.CurrentMonsters;
            }
            else
            {
                Console.WriteLine("Can't get actors");
                return null;
            }

        }

        //this only getst he players home world - it doesn't change with world visit.
        public string GetEorzeanTime(){

        var et = Encoding.Default.
            GetString(MemoryHandler.GetByteArray(MemoryHandler.Scanner.Locations["EORZEANTIME"], 5));
            
            return et;
        }

        public string GetWorld()
        {
            //scan chatlog isntead for 'Welcome to WORLD'
            //Console.WriteLine(_memoryHandler.Scanner.IsScanning);
            var world = System.Text.Encoding.Default.GetString(MemoryHandler.GetByteArray(MemoryHandler.Scanner.Locations["CURRENTWORLD"], 12));
            var world1 = System.Text.Encoding.Default.GetString(MemoryHandler.GetByteArray(MemoryHandler.Scanner.Locations["HOMEWORLD"], 8));
            var worldRegex = Regex.Match($"{world}", @"^.*?(?=\.)").Value;

            //Console.WriteLine("world: " + world);
            return worldRegex + $"  - {world1}";
        }


        //helper
        private double ConvertPosArr(double num) //works for ShB
        {
            //this converts into  x y pos on arr world maps, doesn't work for city states, residential areas, gold saucer etc.
            return (Math.Floor((21.5 + (Convert.ToDouble(num) / 50)) * 10)) / 10;
        }
    }
}