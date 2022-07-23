using System;

namespace untitled_ffxiv_hunt_tracker.Entities
{
    public interface ICoordinates
    {
        Coords Coordinates { get; set; }
    }

    public struct Coords
    {
        public Coords(double x, double y)
        {

            X = x;
            Y = y;
        }

        public double X; /*{ get; set; }*/
        public double Y; /*{ get; set; }*/

        public override string ToString() => $"({Math.Floor(Math.Floor(X * 100) / 10) / 10}, {Math.Floor(Math.Floor(Y * 100) / 10) / 10})";
    }
}