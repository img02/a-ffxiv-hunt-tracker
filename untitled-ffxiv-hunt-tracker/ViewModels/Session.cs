using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog.LayoutRenderers;
using Sharlayan;
using Sharlayan.Core;
using Sharlayan.Core.Interfaces;
using untitled_ffxiv_hunt_tracker.Entities;
using untitled_ffxiv_hunt_tracker.Factories;
using untitled_ffxiv_hunt_tracker.Memory;
using untitled_ffxiv_hunt_tracker.Utilities;

namespace untitled_ffxiv_hunt_tracker.ViewModels
{
    public class Session
    {
        //TEST
        private readonly Dictionary<HuntRank, List<Mob>> ARRDict;
        private readonly Dictionary<HuntRank, List<Mob>> HWDict;
        private readonly Dictionary<HuntRank, List<Mob>> SBDict;
        private readonly Dictionary<HuntRank, List<Mob>> ShBDict;
        private readonly Dictionary<HuntRank, List<Mob>> TestDict;

        private ObservableCollection<Mob> _currentNearbyMobs;


        private readonly Dictionary<String, List<Mob>> Trains;

        public  List<Mob> CurrentTrain;
        private string CurrentTrainName { get; set; }
        public ObservableCollection<Mob> CurrentNearbyMobs { get; }

        public Player CurrentPlayer { get; set; }

        public MemoryReader _memoryReader;
        //make these private later?
        public ChatLogReader _chatLogReader;

        public Session()
        {
            SetUpMemReaderAndChatLogReader();

            //test
            var test = new Test();
            _chatLogReader.ChatLogEvent += test.ListenerMethod;
            _chatLogReader.ChatLogCommandCreateTrain += CreateTrain;
            _chatLogReader.ChatLogCommandPrintTrain += PrintTrain;
            _chatLogReader.ChatLogCommandDeleteTrain += DeleteTrain;
            _chatLogReader.ChatLogCommandChangeTrain += ChangeTrain;
            _chatLogReader.ChatLogCommandStopRecordingTrain += StopRecordingTrain;

            ARRDict = ARRMobFactory.GetARRDict();
            HWDict = HWMobFactory.GetHWDict();
            SBDict = SBMobFactory.GetSBDict();
            ShBDict = ShBMobFactory.GetShBDict();
            TestDict = TestMobFactory.GetTestDict();

            Trains = new Dictionary<string, List<Mob>>();

            CurrentTrain = new List<Mob>();
            CurrentTrainName = null;
            CurrentNearbyMobs = new ObservableCollection<Mob>();

            //train test

            var train = ARRDict[HuntRank.A];
            double x = -400;
            double y = -400;
            
            foreach (Mob m in train)
            {
                m.X = x;
                m.Y = y;
                m.Coordinates = new Coords(ConvertPos(m.X), ConvertPos(m.Y));
                m.HPPercent = 1;
                x += 80;
                y += 80;
            }

            Trains.Add("test train", train);
            CurrentTrain = train;

            //end test code



            CurrentPlayer = new Player();
            GetUser();
        }

        private void CurrentNearbyMobsOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SetUpMemReaderAndChatLogReader()
        {
            _memoryReader = new MemoryReader();
            var processExists = _memoryReader.Setup();

            while (!processExists)
            {
                Console.WriteLine("Process not found... Trying again?");
                Trace.WriteLine("Process not found... Trying again..?");
                processExists = _memoryReader.Setup();
                Thread.Sleep(100);
            }
            Console.WriteLine("Process found!");
            Trace.WriteLine("Process found!");
            _chatLogReader = new ChatLogReader(_memoryReader.MemoryHandler);
        }

        public void Start()
        {
            //readhing chat log throws arithmetic / overflow error if user logs out and logs back in. idk how 2 fix pls
           /* _ = Task.Run(() => //read chatlog for stuff, don't wait for this to finish before continuing .
           {
               while (true)
               {
                   ReadChatLog();
                   Thread.Sleep(100); //if you want to only read chat log / refresh every x seconds
               }
           });*/
           Trace.WriteLine($"can get process? {_memoryReader.CanGetProcess} has exited? {_memoryReader.ProcessHasExited}");
            while (true)
            {
                //Trace.WriteLine($"1inside loop can get process? {_memoryReader.CanGetProcess} has exited? {_memoryReader.ProcessHasExited}");
                //this is really messy but it works. so...
                if (!_memoryReader.CanGetProcess || _memoryReader.ProcessHasExited)
                {
                    Trace.WriteLine($"1inside loop can get process? {_memoryReader.CanGetProcess} has exited? {_memoryReader.ProcessHasExited}");
                    Thread.Sleep(2000);
                    SetUpMemReaderAndChatLogReader();
                }
                //i'm stupid btw.
                //Console.Clear();
                GetUser();
                SearchNearbyMobs();
                //GetMobs();
                //GetNearbyMobs();

                //await Task.Run(() => chatLog.ReadChatLog());

                //1000/144 = 6.94 - so this should refresh a bit more than 144 times per sec, so animation should be 144+fps, right? seems legit. def smoother. cpu usage low.
                Thread.Sleep(6);
            }
        }

        public void SearchNearbyMobs()
        {
            //CurrentNearbyMobs.Clear();
            var tempNearbyList = new ObservableCollection<Mob>();

            var map = CurrentPlayer.MapTerritory;
            var actors = GetMobs();

            if (actors == null)
            {
                return;
            }

            var expansionDict = map < 180 ? ARRDict : map < 402 ? HWDict : map < 622 ? SBDict : ShBDict; //818 the tempest

            foreach (var actor in actors)
            {
                //test check ENDWALKER
                if (actor.Value.HPMax > 25000000)
                {
                    var m = actor.Value;
                    Console.WriteLine($"{m.Name} - ({ConvertPos(m.X)}, {ConvertPos(m.Y)}) - {m.HPMax}");
                }
                //END TEST PRINT

                List<Mob> tempList = expansionDict.Values.FirstOrDefault(l =>
                    (l.FirstOrDefault(m =>
                    m.ModelID == actor.Value.ModelID
                        && (uint)m.MapTerritory == actor.Value.MapTerritory) != null));

                if (tempList != null)
                {
                    var mob = new Mob
                    {
                        Name = actor.Value.Name,
                        ModelID = (int)actor.Value.ModelID,
                        MapTerritory = (MapID)actor.Value.MapTerritory,
                        Rank = tempList.FirstOrDefault(m => m.ModelID == actor.Value.ModelID)?.Rank,
                        Coordinates = new Coords(ConvertPos(actor.Value.X), ConvertPos(actor.Value.Y)),
                        MapImagePath = tempList.FirstOrDefault(m => m.ModelID == actor.Value.ModelID)?.MapImagePath ?? "no image path",
                        HP = actor.Value.HPMax,
                        X = actor.Value.X,
                        Y = actor.Value.Y,
                        Coordinate = actor.Value.Coordinate,
                        HPPercent = actor.Value.HPPercent
                    };


                    /*var mob = (Mob)actor.Value;
                    mob.Coordinates = new Coords(ConvertPos(actor.Value.X), ConvertPos(actor.Value.Y));
                    mob.MapImagePath = tempList.FirstOrDefault(m => m.ModelID == actor.Value.ModelID)?.MapImagePath ??
                                       "no image path";*/

                    tempNearbyList.Add(mob);
                    // CurrentNearbyMobs.Add(mob);

                    //if recording data for a train
                    if (CurrentTrainName != null)
                    {
                        if (Trains[CurrentTrainName].FirstOrDefault(m => m.ModelID == mob.ModelID) == null)
                        {
                            Trains[CurrentTrainName].Add(mob);
                        }
                    }
                }


                #region TEST
                /*
                                if (TestDict.Values.FirstOrDefault(l => (l.FirstOrDefault(m => m.ModelID == actor.Value.ModelID) != null)) != null)
                                {
                                    var mob1 = new Mob
                                    {
                                        Name = actor.Value.Name,
                                        ModelID = (int)actor.Value.ModelID,
                                        MapTerritory = (MapID)actor.Value.MapTerritory,
                                        Rank = TestDict[HuntRank.A].FirstOrDefault(m => m.ModelID == actor.Value.ModelID)?.Rank,
                                        Coordinates = new Coords(ConvertPos(actor.Value.X), ConvertPos(actor.Value.Y)),
                                        MapImagePath = TestDict[HuntRank.A].FirstOrDefault(m => m.ModelID == actor.Value.ModelID)?.MapImagePath ?? "no image path",
                                        HP = actor.Value.HPMax,
                                        X = actor.Value.X,
                                        Y = actor.Value.Y,
                                        Coordinate = actor.Value.Coordinate,
                                        HPPercent = actor.Value.HPPercent
                                    };

                                    CurrentNearbyMobs.Add(mob1);

                                    //if recording data for a train
                                    if (CurrentTrainName != null)
                                    {
                                        if (Trains[CurrentTrainName].FirstOrDefault(m => m.ModelID == mob1.ModelID) == null)
                                        {
                                            Trains[CurrentTrainName].Add(mob1);
                                        }
                                    }
                                }*/
                #endregion
            }



            var toRemove = new List<int>();
            //remove any old mobs that don't exist in the new list
            foreach (var mob in new Collection<Mob>(CurrentNearbyMobs))
            {
                if (tempNearbyList.FirstOrDefault(m => m.ModelID == mob.ModelID) == null)
                {
                    toRemove.Add(mob.ModelID);
                }
            }

            toRemove.ForEach(i =>
            {
                var mob = CurrentNearbyMobs.FirstOrDefault(m => m.ModelID == i);
                mob.UnregisterHandlers(); // remove handlers from event
                CurrentNearbyMobs.Remove(mob);
            });


            //add new mobs, update oldmobs coords.
            foreach (var tMob in tempNearbyList)
            {
                if (CurrentNearbyMobs.FirstOrDefault(m => m.ModelID == tMob.ModelID) == null)
                {
                    CurrentNearbyMobs.Add(tMob);
                }
                else
                {
                    var mob = CurrentNearbyMobs.FirstOrDefault(m => m.ModelID == tMob.ModelID);
                    mob.Coordinates = tMob.Coordinates;
                    mob.HPPercent = tMob.HPPercent;
                }
            }


            //delete this
            if (CurrentNearbyMobs.Count > 0)
            {
                Console.WriteLine($"CurrentNearbyMobs: {CurrentNearbyMobs.Count}");
                //Trace.WriteLine($"CurrentNearbyMobs: {CurrentNearbyMobs.Count}");
            }

        }

        public void GetNearbyMobs()
        {
            //if using list
            /*CurrentNearbyMobs.ForEach(m =>
            {
                Console.WriteLine($"GetNearbyMobs : {m.Name} - {m.Coordinates}: {Sharlayan.Utilities.ZoneLookup.GetZoneInfo((uint)m.MapTerritory).Name.English} ");
            });*/

            foreach (var m in CurrentNearbyMobs)
            {
                //Console.WriteLine($"GetNearbyMobs : {m.Name} - {m.Coordinates}: {Sharlayan.Utilities.ZoneLookup.GetZoneInfo((uint)m.MapTerritory).Name.English}");
            }
        }

        #region Trains

        //create train and start recording train - merge change and create? more prone to user error with typoed name?
        public void CreateTrain(object sender, string name)
        {
            Console.WriteLine(name);
            var list = new List<Mob>();
            if (Trains.ContainsKey(name))
            {
                Console.WriteLine("\n==============\ntrain name already exists\n" +
                                  "Did you mean to change trains? => /change NAME\n==================\n");
                return;
            }
            Trains.Add(name, list);
            CurrentTrainName = name;
        }

        //print train info to console. 
        public void PrintTrain(object sender, string name)
        {
            Console.WriteLine(name);

            if (!Trains.ContainsKey(name))
            {
                return;
            }

            foreach (var m in Trains[name])
            {
                Console.WriteLine($"{m.Name} : {m.Coordinates}, {m.HP}, {Sharlayan.Utilities.ZoneLookup.GetZoneInfo((uint)m.MapTerritory).Name.English}");
            }
        }
        //delete a train
        public void DeleteTrain(object sender, string name)
        {
            Console.WriteLine(name);

            if (Trains.ContainsKey(name))
            {
                Trains.Remove(name);
            }

            if (CurrentTrainName == name)
            {
                CurrentTrainName = null;
            }

        }

        //change to a different train, 
        public void ChangeTrain(object sender, string name)
        {
            Console.WriteLine(name);

            if (!Trains.ContainsKey(name))
            {
                Console.WriteLine("================\nTrain does not exist" +
                                  "\nDid you mean to create a new train? => /train" +
                                  "\n===============\n");
            }
            else
            {
                CurrentTrainName = name;
            }
        }

        //stop recording for train - set currentTrainName to null;
        public void StopRecordingTrain(object sender, EventArgs e)
        {
            CurrentTrainName = null;
        }

        //remove mob from train. 'start' trian first? to allow removal.

        #endregion


        //move everything into session.run() method - run loops tasks search etc?

        #region  Stuff
        public void ReadChatLog()
        {
            _chatLogReader.ReadChatLog();
        }

        public void GetUser()
        {
            var user = _memoryReader.GetUser();

            CurrentPlayer.Coordinates = new Coords(ConvertPos(user.X), ConvertPos(user.Y));
            CurrentPlayer.Name = user.Name;
            CurrentPlayer.MapTerritory = user.MapTerritory;
            CurrentPlayer.SetMapImagePath();
            CurrentPlayer.PlayerIconImagePath = Globals.PlayerIconImagePath;
            CurrentPlayer.Heading = user.Heading;
            // Console.WriteLine($"Heading: {CurrentPlayer.Heading}");
            CurrentPlayer.HPPercent = user.HPPercent;

            //TEST PRINT
          /*  Console.WriteLine($"Name: {user.Name} - Coords: {CurrentPlayer.Coordinates} - ({user.X}, {user.Y}) -" +
                              $" HP: {CurrentPlayer.HPPercent*100.00} " +
                              $"MapID: {user.MapTerritory} , {Helpers.GetMapName(user.MapTerritory)}");*/
        }

        public ConcurrentDictionary<uint, ActorItem> GetMobs()
        {
            return _memoryReader.GetMobs();
        }

        #endregion

        #region Helpers
        private double ConvertPos(double num) //works for ShB
        {
            //this converts into  x y pos on arr world maps, doesn't work for city states, residential areas, gold saucer etc.
            return (Math.Floor((21.5 + (Convert.ToDouble(num) / 50)) * 10)) / 10;
        }


        #endregion
    }
}
