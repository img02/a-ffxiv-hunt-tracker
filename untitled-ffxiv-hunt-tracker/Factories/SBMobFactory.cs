using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using untitled_ffxiv_hunt_tracker.Entities;
using untitled_ffxiv_hunt_tracker.Utilities;

namespace untitled_ffxiv_hunt_tracker.Factories
{
    static class SBMobFactory
    {

        private static Dictionary<HuntRank, List<Mob>> SBDictionary = new Dictionary<HuntRank, List<Mob>>();

        //load mob objects from json file,

        //return a List of mobs.

        static SBMobFactory()
        {
            var A = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/SB-A.json"));
            var B = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/SB-B.json"));
            var S = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/SB-S.json"));

            SBDictionary.Add(HuntRank.A, A);
            SBDictionary.Add(HuntRank.B, B);
            SBDictionary.Add(HuntRank.S, S);

            foreach (var list in SBDictionary.Values)
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
            Console.WriteLine("=====\nStormblood\n====");
            A.ForEach((m) => Console.WriteLine($"|{m.Name}| \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
            B.ForEach((m) => Console.WriteLine($"|{m.Name}| \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
            S.ForEach((m) => Console.WriteLine($"|{m.Name}| \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
#endif
        }

        public static Dictionary<HuntRank, List<Mob>> GetSBDict()
        {
            return SBDictionary;
        }
    }
}