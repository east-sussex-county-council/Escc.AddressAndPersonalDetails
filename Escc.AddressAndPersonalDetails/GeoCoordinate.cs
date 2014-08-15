
namespace Escc.AddressAndPersonalDetails
{
    /// <summary>
    /// A geographic point expressed using latitude and longitude or easting and northing
    /// </summary>
    public class GeoCoordinate
    {
        /// <summary>
        ///  A geographic coordinate that specifies the north-south position of a point on the Earth's surface
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        ///  A geographic coordinate that specifies the east-west position of a point on the Earth's surface
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the easting grid coordinate
        /// </summary>
        public int Easting { get; set; }

        /// <summary>
        /// Gets or sets the northing grid coordinate
        /// </summary>
        public int Northing { get; set; }
    }
}
