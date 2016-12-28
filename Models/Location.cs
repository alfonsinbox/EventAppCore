using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EventAppCore.Models
{
    public class Location : DbEntityBase
    {
        public float Latitude { get; set; }

        public float Longitude { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }

        public ICollection<Event> Events { get; set; }

        public User CreatedBy { get; set; }

        [NotMapped]
        public double Distance { get; set; }

        public double GetDistanceFrom(double myLatitude, double myLongitude)
        {
            const bool kilometers = true;
            var theta = Longitude - myLongitude;
            var dist = Math.Sin(DegToRad(Latitude)) * Math.Sin(DegToRad(myLatitude)) + Math.Cos(DegToRad(Latitude)) * Math.Cos(DegToRad(myLatitude)) * Math.Cos(DegToRad(theta));
            dist = Math.Acos(dist);
            dist = RadToDeg(dist) * 60 * 1.1515 * 1.609344 * (kilometers ? 1 : 1000);
            return dist;
        }

        public double GetAndSetDistanceFrom(double myLatitude, double myLongitude)
        {
            //Distance = GetDistanceFrom(myLatitude, myLongitude);
            return Distance;
        }

        public Location WithDistanceFrom(double myLatitude, double myLongitude)
        {
            Distance = GetDistanceFrom(myLatitude, myLongitude);
            return this;
        }

        public Location SetDistanceFrom(double myLatitude, double myLongitude)
        {
            Distance = GetDistanceFrom(myLatitude, myLongitude);
            return this;
        }

        private static double DegToRad(double deg) => (deg * Math.PI / 180.0);

        private static double RadToDeg(double rad) => (rad / Math.PI * 180.0);
    }
}