using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventAppCore.Models.View
{
    public class ViewLocation
    {
        public string Id { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Description { get; set; }

/*
                        Could be useful when searching for events from location
                        public ICollection<Event> Events { get; set; }
*/
        // Is CreatedBy necessary to include?
        public ViewUser CreatedBy { get; set; }

        public double Distance { get; set; }

        public void SetDistanceFrom(double myLatitude, double myLongitude)
        {
            const bool kilometers = true;
            var theta = Longitude - myLongitude;
            var dist = Math.Sin(DegToRad(Latitude)) * Math.Sin(DegToRad(myLatitude)) + Math.Cos(DegToRad(Latitude)) * Math.Cos(DegToRad(myLatitude)) * Math.Cos(DegToRad(theta));
            dist = Math.Acos(dist);
            Distance = RadToDeg(dist) * 60 * 1.1515 * 1.609344 * (kilometers ? 1 : 1000);
        }

        private static double DegToRad(double deg) => (deg * Math.PI / 180.0);

        private static double RadToDeg(double rad) => (rad / Math.PI * 180.0);

    }
}