using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using untitled_ffxiv_hunt_tracker.Entities;
using untitled_ffxiv_hunt_tracker.Utilities;

namespace untitled_ffxiv_hunt_tracker.Factories
{
    static class EWMobFactory
    {

        private static Dictionary<HuntRank, List<Mob>> EWDictionary = new Dictionary<HuntRank, List<Mob>>();

        //load mob objects from json file,

        //return a List of mobs.

        static EWMobFactory()
        {
            var A = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/EW-A.json"));
            var B = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/EW-B.json"));
            var S = JsonConvert.DeserializeObject<List<Mob>>(File.ReadAllText("./data/EW-S.json"));

            EWDictionary.Add(HuntRank.A, A);
            EWDictionary.Add(HuntRank.B, B);
            EWDictionary.Add(HuntRank.S, S);

            foreach (var list in EWDictionary.Values)
            {
                list.ForEach(m =>
                {
                    if (m.Name != "Forgiven Rebellion")
                    {
                        var mapName = Helpers.GetMapName((uint)m.MapTerritory).Replace(" ", "_");
                        var imagePath = $"{Globals.ImageRootDir}{mapName}-data.jpg";

#if FACTORYTESTIMAGE
                        Console.WriteLine(imagePath);

                        m.MapImagePath = imagePath;
#endif
                    }
                    else
                    {
                        var mapName = Helpers.GetMapName((uint)m.MapTerritory).Replace(" ", "_");
                        var imagePath = $"{Globals.ImageRootDirForgivenRebellion}{mapName}_-_Forgiven_Rebellion.jpg";

#if FACTORYTESTIMAGE
                        Console.WriteLine(imagePath);

                        m.MapImagePath = imagePath;
#endif
                    }
                });
            }

#if FACTORYTEST
            Console.WriteLine("=====\nEndwalker\n====");
            A.ForEach((m) => Console.WriteLine($"{m.Name} \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
            B.ForEach((m) => Console.WriteLine($"{m.Name} \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
            S.ForEach((m) => Console.WriteLine($"{m.Name} \t\t\t| {m.Rank} \t\t\t| {m.MapTerritory}"));
#endif
        }

        public static Dictionary<HuntRank, List<Mob>> GetEWDict()
        {
            return EWDictionary;
        }
    }
}