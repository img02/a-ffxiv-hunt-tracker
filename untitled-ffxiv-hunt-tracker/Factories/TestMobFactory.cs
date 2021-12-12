using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using untitled_ffxiv_hunt_tracker.Entities;

namespace untitled_ffxiv_hunt_tracker.Factories
{
    static class TestMobFactory
    {

        private static Dictionary<HuntRank, List<Mob>> TestDictionary = new Dictionary<HuntRank, List<Mob>>();

        //load mob objects from json file,

        //return a List of mobs.

        static TestMobFactory()
        {
            var A = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/test.json"));
            //var B = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/ShB-B.json"));
            //var S = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/ShB-S.json"));

            TestDictionary.Add(HuntRank.A, A);
            //ARRDictionary.Add(HuntRank.B, B);
            //TestDictionary.Add(HuntRank.S, S);

#if FACTORYTEST
            Console.WriteLine("=====\nTEST DATA\n====");
            A.ForEach((m) => Console.WriteLine($"{m.Name} \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
            //S.ForEach((m) => Console.WriteLine($"{m.Name} \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
#endif
        }

        public static Dictionary<HuntRank, List<Mob>> GetTestDict()
        {
            return TestDictionary;
        }
    }
}