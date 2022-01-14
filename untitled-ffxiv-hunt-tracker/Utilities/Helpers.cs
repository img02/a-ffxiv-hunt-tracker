using Sharlayan.Enums;

namespace untitled_ffxiv_hunt_tracker.Utilities
{
    static class Helpers
    {
        public static string GetMapName(uint mapTerritory)
        {
            switch (Globals.Language)
            {
                case GameLanguage.English:
                    return Sharlayan.Utilities.ZoneLookup.GetZoneInfo(mapTerritory).Name.English;
                case GameLanguage.French:
                    return Sharlayan.Utilities.ZoneLookup.GetZoneInfo(mapTerritory).Name.French;
                case GameLanguage.German:
                    return Sharlayan.Utilities.ZoneLookup.GetZoneInfo(mapTerritory).Name.German;
                case GameLanguage.Japanese:
                    return Sharlayan.Utilities.ZoneLookup.GetZoneInfo(mapTerritory).Name.Japanese;
                case GameLanguage.Korean:
                    return Sharlayan.Utilities.ZoneLookup.GetZoneInfo(mapTerritory).Name.Korean;
                case GameLanguage.Chinese:
                    return Sharlayan.Utilities.ZoneLookup.GetZoneInfo(mapTerritory).Name.Chinese;
                default:
                    return Sharlayan.Utilities.ZoneLookup.GetZoneInfo(mapTerritory).Name.English;
            }
        }
    }
}
