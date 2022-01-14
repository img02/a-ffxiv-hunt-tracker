using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using untitled_ffxiv_hunt_tracker.Entities;
using untitled_ffxiv_hunt_tracker.Utilities;

namespace untitled_ffxiv_hunt_tracker.Factories
{
    static class HWMobFactory
    {

        private static Dictionary<HuntRank, List<Mob>> HWDictionary = new Dictionary<HuntRank, List<Mob>>();

        //load mob objects from json file,

        //return a List of mobs.

        static HWMobFactory()
        {
            var A = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/HW-A.json"));
            var B = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/HW-B.json"));
            var S = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/HW-S.json"));

            HWDictionary.Add(HuntRank.A, A);
            HWDictionary.Add(HuntRank.B, B);
            HWDictionary.Add(HuntRank.S, S);

            foreach (var list in HWDictionary.Values)
            {
                list.ForEach(m =>
                {

                    var mapName = Helpers.GetMapName((uint)m.MapTerritory).Replace(" ", "_");
                    var imagePath = $"{Globals.ImageRootDir}{mapName}-data.jpg";

#if FACTORYTESTIMAGE
                    Console.WriteLine(imagePath);

                    m.MapImagePath = imagePath;
#endif
                });
            }

#if FACTORYTEST
            Console.WriteLine("=====\nHeavensward\n====");
            A.ForEach((m) => Console.WriteLine($"{m.Name} \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
            B.ForEach((m) => Console.WriteLine($"{m.Name} \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
            S.ForEach((m) => Console.WriteLine($"{m.Name} \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
#endif
        }

        public static Dictionary<HuntRank, List<Mob>> GetHWDict()
        {
            return HWDictionary;
        }
    }
}