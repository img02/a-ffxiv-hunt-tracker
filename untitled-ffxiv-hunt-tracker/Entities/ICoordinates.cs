using System;
using System.ComponentModel;

namespace untitled_ffxiv_hunt_tracker.Entities
{
    public interface ICoordinates
    {
        Coords Coordinates { get; set; }
        string GetCoords();

    }

    public struct Coords
    {
        public Coords(double x, double y)
        {

            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public override string ToString() => $"({X,0:0.0}, {Y,0:0.0})";
    }
}