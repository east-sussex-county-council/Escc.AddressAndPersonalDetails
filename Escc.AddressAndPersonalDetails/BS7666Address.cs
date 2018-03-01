using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Escc.AddressAndPersonalDetails
{
    /// <summary>
    /// An address in a format compliant with National Land and Property Gazetteer and e-GIF
    /// </summary>
    /// <remarks><para>Descriptions of BS7666 fields are taken from "Addresses and Government Data Standards" document available at 
    /// http://www.govtalk.gov.uk/documents/ADDRESS%20PAPER%20WITH%20GDSC%20Revised%202001-12-30.doc</para>
    /// <para>Use the Escc.AddressAndPersonalDetails.SerialisedtoBS7666.xslt stylesheet to convert a serialisation of this class to one which 
    /// validates against the BS7666 XML schema v2.0. (XML serialisation was tried to automate this, but caused problems when using 
    /// web services.)</para>
    /// </remarks>
    public class BS7666Address
    {
        #region Private fields
        private string paon;
        private string saon;
        private string streetName;
        private string locality;
        private string town;
        private string administrativeArea;
        private string postcode;
        private bool hasAddress;
        private bool hasAddressCached;

        #endregion // Private fields

        #region Public properties

        /// <summary>
        /// Gets or sets the ID if the details are stored in a database.
        /// </summary>
        /// <value>The ID.</value>
        public int Id { get; set; }

        /// <summary>
        /// Geographic point marking the location of the address
        /// </summary>
        public GeoCoordinate GeoCoordinate { get; set; } = new GeoCoordinate();

        /// <summary>
        /// Store the code allocated by the Post Office to identify a postal delivery point (NLPG)
        /// </summary>
        public string Postcode
        {
            get
            {
                return this.postcode;
            }
            set
            {
                if (value != null)
                {
                    this.postcode = value.Trim();

                    // Add space to postcode if missing
                    if (this.postcode.IndexOf(" ", StringComparison.Ordinal) == -1)
                    {
                        if (this.postcode.Length == 6)
                        {
                            this.postcode = this.postcode.Substring(0, 3) + " " + this.postcode.Substring(3, 3);
                        }
                        else if (this.postcode.Length == 7)
                        {
                            this.postcode = this.postcode.Substring(0, 4) + " " + this.postcode.Substring(4, 3);
                        }
                    }

                }
                else this.postcode = String.Empty;

                this.hasAddressCached = false;
            }
        }

        /// <summary>
        /// A geographic area that may be the highest level local administrative area, which may be a county or a unitary authority, an island or island group or London (NLPG)
        /// </summary>
        public string AdministrativeArea
        {
            get
            {
                return this.administrativeArea;
            }
            set
            {
                this.administrativeArea = value;

                this.hasAddressCached = false;
            }
        }

        /// <summary>
        /// A city or town that is not an administrative area, a suburb of an administrative area that does not form part of another town or a London district (NLPG)
        /// </summary>
        public string Town
        {
            get
            {
                return this.town;
            }
            set
            {
                this.town = value;

                this.hasAddressCached = false;
            }
        }

        /// <summary>
        /// A neighbourhood, suburb, district, village, estate, settlement, or parish that may form part of a town, or stand in its own right within the context of an administrative area. Where an industrial estate contains streets it is defined as a locality in its own right. (NLPG)
        /// </summary>
        public string Locality
        {
            get
            {
                return this.locality;
            }
            set
            {
                this.locality = value;

                this.hasAddressCached = false;
            }
        }

        /// <summary>
        /// The designated street name or street description that has been allocated to a street by the street naming authority (NLPG)
        /// </summary>
        public string StreetName
        {
            get
            {
                return this.streetName;
            }
            set
            {
                this.streetName = value;

                this.hasAddressCached = false;
            }
        }

        /// <summary>
        /// Number, name or description used to identify the secondary addressable object within or related to a primary addressable object (NLPG)
        /// </summary>
        public string Saon
        {
            get
            {
                return this.saon;
            }
            set
            {
                this.saon = value;

                this.hasAddressCached = false;
            }
        }

        /// <summary>
        /// Designated premise number, and/or the premise name, where neither of these exist then the PAON is the name of the organisation in occupation, or a description of the addressable object (NLPG)
        /// </summary>
        public string Paon
        {
            get
            {
                return this.paon;
            }
            set
            {
                this.paon = value;

                this.hasAddressCached = false;
            }
        }

        /// <summary>
        /// A unique identifier for each street (NLPG)
        /// </summary>
        public string Usrn { get; set; }

        /// <summary>
        /// A unique identifier for each land and property unit (NLPG)
        /// </summary>
        public string Uprn { get; set; }

        #endregion // Public properties

        #region Constructors
        /// <summary>
        /// An address in BS7666 format, compliant with National Land and Property Gazetteer  and e-GIF
        /// </summary>
        public BS7666Address() { }

        /// <summary>
        /// An address in BS7666 format, compliant with National Land and Property Gazetteer  and e-GIF
        /// </summary>
        /// <param name="uprn">A unique identifier for each land and property unit (NLPG)</param>
        /// <param name="usrn">A unique identifier for each street (NLPG)</param>
        /// <param name="paon">Designated premise number, and/or the premise name, where neither of these exist then the PAON is the name of the organisation in occupation, or a description of the addressable object (NLPG)</param>
        /// <param name="saon">Number, name or description used to identify the secondary addressable object within or related to a primary addressable object (NLPG)</param>
        /// <param name="streetName">The designated street name or street description that has been allocated to a street by the street naming authority (NLPG)</param>
        /// <param name="locality">A neighbourhood, suburb, district, village, estate, settlement, or parish that may form part of a town, or stand in its own right within the context of an administrative area. Where an industrial estate contains streets it is defined as a locality in its own right. (NLPG)</param>
        /// <param name="town">A city or town that is not an administrative area, a suburb of an administrative area that does not form part of another town or a London district (NLPG)</param>
        /// <param name="administrativeArea">A geographic area that may be the highest level local administrative area, which may be a county or a unitary authority, an island or island group or London (NLPG)</param>
        /// <param name="postcode">The code allocated by the Post Office to identify a postal delivery point (NLPG)</param>
        public BS7666Address(string uprn, string usrn, string paon, string saon, string streetName, string locality, string town, string administrativeArea, string postcode)
        {
            this.Uprn = uprn;
            this.Usrn = usrn;
            this.Paon = paon;
            this.Saon = saon;
            this.StreetName = streetName;
            this.Locality = locality;
            this.Town = town;
            this.AdministrativeArea = administrativeArea;
            this.Postcode = postcode;
        }

        /// <summary>
        /// An address in BS7666 format, compliant with National Land and Property Gazetteer  and e-GIF
        /// </summary>
        /// <param name="paon">Designated premise number, and/or the premise name, where neither of these exist then the PAON is the name of the organisation in occupation, or a description of the addressable object (NLPG)</param>
        /// <param name="saon">Number, name or description used to identify the secondary addressable object within or related to a primary addressable object (NLPG)</param>
        /// <param name="streetName">The designated street name or street description that has been allocated to a street by the street naming authority (NLPG)</param>
        /// <param name="locality">A neighbourhood, suburb, district, village, estate, settlement, or parish that may form part of a town, or stand in its own right within the context of an administrative area. Where an industrial estate contains streets it is defined as a locality in its own right. (NLPG)</param>
        /// <param name="town">A city or town that is not an administrative area, a suburb of an administrative area that does not form part of another town or a London district (NLPG)</param>
        /// <param name="administrativeArea">A geographic area that may be the highest level local administrative area, which may be a county or a unitary authority, an island or island group or London (NLPG)</param>
        /// <param name="postcode">The code allocated by the Post Office to identify a postal delivery point (NLPG)</param>
        public BS7666Address(string paon, string saon, string streetName, string locality, string town, string administrativeArea, string postcode)
        {
            this.Uprn = String.Empty;
            this.Usrn = String.Empty;
            this.Paon = paon;
            this.Saon = saon;
            this.StreetName = streetName;
            this.Locality = locality;
            this.Town = town;
            this.AdministrativeArea = administrativeArea;
            this.Postcode = postcode;
        }


        #endregion // Constructors

        #region Conversion to other formats
        /// <summary>
        /// Gets a simple address to use for display. If using on a web page, use <see cref="SimpleAddressControl"/> instead for microformat support.
        /// </summary>
        /// <returns>SimpleAddress populated from the BS7666 address</returns>
        public SimpleAddress GetSimpleAddress()
        {
            // Create a copy of the address so we can correct the case without affecting the external properties
            BS7666Address addrCopy = new BS7666Address(String.IsNullOrEmpty(this.Paon) ? String.Empty : this.Paon.Trim(),
                String.IsNullOrEmpty(this.Saon) ? String.Empty : this.Saon.Trim(),
                String.IsNullOrEmpty(this.StreetName) ? String.Empty : this.StreetName.Trim(),
                String.IsNullOrEmpty(this.Locality) ? String.Empty : this.Locality.Trim(),
                String.IsNullOrEmpty(this.Town) ? String.Empty : this.Town.Trim(),
                String.IsNullOrEmpty(this.AdministrativeArea) ? String.Empty : this.AdministrativeArea.Trim(),
                String.IsNullOrEmpty(this.Postcode) ? String.Empty : this.Postcode.Trim());

            if (addrCopy.StreetName != null && addrCopy.StreetName.ToUpper(CultureInfo.CurrentCulture) == addrCopy.StreetName)
            {
                addrCopy.StreetName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(addrCopy.StreetName.ToLower(CultureInfo.CurrentCulture));
            }
            if (addrCopy.Locality != null && addrCopy.Locality.ToUpper(CultureInfo.CurrentCulture) == addrCopy.Locality)
            {
                addrCopy.Locality = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(addrCopy.Locality.ToLower(CultureInfo.CurrentCulture));
            }
            if (addrCopy.Town != null && addrCopy.Town.ToUpper(CultureInfo.CurrentCulture) == addrCopy.Town)
            {
                addrCopy.Town = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(addrCopy.Town.ToLower(CultureInfo.CurrentCulture));
            }
            if (addrCopy.AdministrativeArea != null && addrCopy.AdministrativeArea.ToUpper(CultureInfo.CurrentCulture) == addrCopy.AdministrativeArea)
            {
                addrCopy.AdministrativeArea = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(addrCopy.AdministrativeArea.ToLower(CultureInfo.CurrentCulture));
            }

            // Create a presentational address
            SimpleAddress addr = new SimpleAddress();

            addr.AddressLine1 = addrCopy.Saon;

            if ((addr.AddressLine1 == null || addr.AddressLine1.Trim().Length == 0) && !SimpleAddress.IsFormat1Name(addrCopy.Paon))
            {
                // If line one still spare, we have enough lines
                addr.AddressLine1 = addrCopy.Paon;
                addr.AddressLine2 = addrCopy.StreetName;
            }
            else
            {
                // If line 1 used by SAON or PAON is just a number, cram PAON and StreetName onto one line
                if (addrCopy.Paon != null && addrCopy.Paon.Length > 0 && addrCopy.StreetName != null && addrCopy.StreetName.Length > 0)
                {
                    if (Regex.IsMatch(this.Paon, "^[0-9]{1,2}[-/]?[0-9]{0,2}[A-Z]?$", RegexOptions.IgnoreCase))
                    {
                        addr.AddressLine2 = String.Format(CultureInfo.CurrentCulture, "{0} {1}", addrCopy.Paon, addrCopy.StreetName);
                    }
                    else
                    {
                        addr.AddressLine2 = String.Format(CultureInfo.CurrentCulture, "{0}, {1}", addrCopy.Paon, addrCopy.StreetName);
                    }
                }
                if (addrCopy.Paon != null && addrCopy.Paon.Length > 0 && (addrCopy.StreetName == null || addrCopy.StreetName.Length == 0))
                {
                    addr.AddressLine2 = addrCopy.Paon;
                }
                else if ((addrCopy.Paon == null || addrCopy.Paon.Length == 0) && (addrCopy.StreetName != null && addrCopy.StreetName.Length > 0))
                {
                    addr.AddressLine2 = addrCopy.StreetName;
                }
            }
            addr.AddressLine3 = addrCopy.Locality;
            addr.AddressLine4 = addrCopy.Town;
            addr.AddressLine5 = String.Format(CultureInfo.CurrentCulture, "{0} {1}", addrCopy.AdministrativeArea, addrCopy.Postcode).Trim();

            return addr;
        }

        /// <summary>
        /// Returns the address contained by this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.GetSimpleAddress().ToString();
        }

        #endregion // Conversion to other formats

        /// <summary>
        /// Determines whether this instance contains an address (at least one of SAON, PAON, street descriptor, locality, town, administrative area or postcode).
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance contains an address; otherwise, <c>false</c>.
        /// </returns>
        public bool HasAddress()
        {
            if (!this.hasAddressCached)
            {
                this.hasAddress = (
                    (this.saon != null && this.saon.Trim().Length > 0) ||
                    (this.paon != null && this.paon.Trim().Length > 0) ||
                    (this.streetName != null && this.streetName.Trim().Length > 0) ||
                    (this.locality != null && this.locality.Trim().Length > 0) ||
                    (this.town != null && this.town.Trim().Length > 0) ||
                    (this.administrativeArea != null && this.administrativeArea.Trim().Length > 0) ||
                    (this.postcode != null && this.postcode.Length > 0)
                    );

                this.hasAddressCached = true;
            }

            return this.hasAddress;
        }
    }
}
