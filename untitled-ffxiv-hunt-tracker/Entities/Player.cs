using Sharlayan.Core;
using System;
using System.ComponentModel;
using untitled_ffxiv_hunt_tracker.Utilities;

namespace untitled_ffxiv_hunt_tracker.Entities
{
    public class Player : ActorItem, ICoordinates, INotifyPropertyChanged
    {
        private string _currentMapImagePath;
        private Coords _coordinates;

        public Coords Coordinates
        {
            get => _coordinates;
            set
            {
                _coordinates = value;
                CoordsChanged?.Invoke(this, _coordinates);
            }
        }

        public new double HPPercent { get; set; }

        public event EventHandler<Coords> CoordsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public string CurrentWorld { get; set; }
        public string PlayerIconImagePath { get; set; }

        public string CurrentMapImagePath
        {
            get => _currentMapImagePath;
            set
            {
                _currentMapImagePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentMapImagePath)));
            }
        }

        public Player()
        {

        }
        public Player(Coords coordinates, string name, string currentWorld, uint mapTerritory)
        {
            Coordinates = coordinates;
            Name = name;
            CurrentWorld = currentWorld;
            MapTerritory = mapTerritory;
            SetMapImagePath(false);
        }

        public void SetMapImagePath(bool SSMap)
        {
            if (!SSMap)
            {
                var mapName = Helpers.GetMapName((uint)MapTerritory).Replace(" ", "_");
                CurrentMapImagePath = $"{Globals.ImageRootDir}/Maps/{mapName}-data.jpg";
            }
            else
            {
                var mapName = Helpers.GetMapName((uint)MapTerritory).Replace(" ", "_");
                CurrentMapImagePath = $"{Globals.ImageRootDir}/Maps/SS Maps/{mapName}_SS-data.jpg";
            }
        }

        public override string ToString()
        {
            return $"{Name} {Coordinates.ToString()} {(HPPercent * 100),0:0.00}%";
        }
    }
}
