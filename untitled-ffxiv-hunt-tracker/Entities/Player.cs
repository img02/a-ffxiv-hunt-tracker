using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Sharlayan.Core;
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

        public string GetCoords() => Coordinates.ToString();
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
            SetMapImagePath();
        }

        public void SetMapImagePath()
        {
            var mapName = Helpers.GetMapName((uint)MapTerritory).Replace(" ", "_");
            CurrentMapImagePath = $"{Globals.ImageRootDir}/Maps/{mapName}-data.jpg";
        }

        public override string ToString()
        {
            return $"{Name} {Coordinates.ToString()} {(HPPercent * 100),0:0.00}%";
        }
    }
}
