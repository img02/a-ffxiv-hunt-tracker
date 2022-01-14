using Newtonsoft.Json;
using Sharlayan.Core;
using System;
using System.ComponentModel;

namespace untitled_ffxiv_hunt_tracker.Entities
{
    public class Mob : ActorItem, ICoordinates, INotifyPropertyChanged
    {
        private Coords _coordinates;
        private double _hPPercent;
        private double _hPPercentAsPercentage;

        public Coords Coordinates
        {
            get => _coordinates;
            set
            {
                _coordinates = value;
                CoordsChanged?.Invoke(this, _coordinates);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Coordinates)));
            }
        }

        public event EventHandler<Coords> CoordsChanged;
        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; set; }
        public int ModelID { get; set; }
        public string Rank { get; set; }
        public double HP { get; set; }
        public MapID MapTerritory { get; set; }
        public string MapImagePath { get; set; }
        public new double HPPercent
        {
            get => _hPPercent;
            set
            {
                _hPPercent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HPPercent)));

                HPPercentAsPercentage = Math.Round(_hPPercent * 100, 2);
            }
        }

        public double HPPercentAsPercentage
        {
            get => _hPPercentAsPercentage;
            private set
            {
                _hPPercentAsPercentage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HPPercentAsPercentage)));
            }
        }

        public Mob()
        {

        }

        public Mob(string name, int modelId, string rank)
        {
            Name = name;
            ModelID = modelId;
            Rank = rank;
        }

        [JsonConstructor]
        public Mob(string name, string rank, MapID mapTerritory)
        {
            Name = name;
            Rank = rank;
            MapTerritory = mapTerritory;
        }

        public Mob(Coords coordinates, string name, int modelId, string rank, double hp, MapID mapTerritory)
        {
            Coordinates = coordinates;
            Name = name;
            ModelID = modelId;
            Rank = rank;
            HP = hp;
            MapTerritory = mapTerritory;
        }

        public void UnregisterHandlers()
        {
            CoordsChanged = null;
            PropertyChanged = null;
        }

        public override string ToString()
        {
            return $"{Name} {Coordinates.ToString()} {HPPercent * 100,0:0.00}%";
        }
    }
}
