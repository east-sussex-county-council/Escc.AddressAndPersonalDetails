using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using EsccWebTeam.Gdsc.Properties;

namespace EsccWebTeam.Gdsc
{
    /// <summary>
    /// PAF-based address in Great Britain, supplied by Ordnance Survey ADDRESS-POINT from Royal Mail data
    /// </summary>
    public class AddressPointAddress
    {
        #region Fields

        private string organisationName = "";
        private string departmentName = "";
        private string pOBoxNumber = "";
        private string subBuildingName = "";
        private string buildingName = "";
        private string buildingNumber = "";
        private string dependentThoroughfareName = "";
        private string thoroughfareName = "";
        private string doubleDependentLocalityName = "";
        private string dependentLocalityName = "";
        private string postTown = "";
        private string postalCounty = "";
        private string postcode = "";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets an abbreviated form of address referring to one or a collection of addresses that conforms to a specification set by Royal Mail
        /// </summary>
        public string Postcode
        {
            get
            {
                return this.postcode;
            }
            set
            {
                this.postcode = value;
            }
        }

        /// <summary>
        /// Gets or sets the county name (no longer used by Address Point)
        /// </summary>
        public string PostalCounty
        {
            get
            {
                return this.postalCounty;
            }
            set
            {
                this.postalCounty = value;
            }
        }

        /// <summary>
        /// Gets or sets the town or city in which is located the Royal Mail sorting office from which mail is delivered to its final recipient
        /// </summary>
        public string PostTown
        {
            get
            {
                return this.postTown;
            }
            set
            {
                this.postTown = value;
            }
        }

        /// <summary>
        /// Gets or sets an area within a post town
        /// </summary>
        public string DependentLocalityName
        {
            get
            {
                return this.dependentLocalityName;
            }
            set
            {
                this.dependentLocalityName = value;
            }
        }

        /// <summary>
        /// Gets or sets an area used to distinguish between similar or same thoroughfares within a dependent locality
        /// </summary>
        public string DoubleDependentLocalityName
        {
            get
            {
                return this.doubleDependentLocalityName;
            }
            set
            {
                this.doubleDependentLocalityName = value;
            }
        }

        /// <summary>
        /// Gets or sets a road, track or named access route on which there are Royal Mail delivery points
        /// </summary>
        public string ThoroughfareName
        {
            get
            {
                return this.thoroughfareName;
            }
            set
            {
                this.thoroughfareName = value;
            }
        }

        /// <summary>
        /// Gets or sets a named thoroughfare which is within another named thoroughfare
        /// </summary>
        public string DependentThoroughfareName
        {
            get
            {
                return this.dependentThoroughfareName;
            }
            set
            {
                this.dependentThoroughfareName = value;
            }
        }

        /// <summary>
        /// Gets or sets a number given to a single building or small group of buildings which identifies it from its neighbours; aka postal number
        /// </summary>
        public string BuildingNumber
        {
            get
            {
                return this.buildingNumber;
            }
            set
            {
                this.buildingNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets a description applied to a single building or small group of buildings
        /// </summary>
        public string BuildingName
        {
            get
            {
                return this.buildingName;
            }
            set
            {
                this.buildingName = value;
            }
        }

        /// <summary>
        /// Gets or sets a name and/or number identifying a subdivision of a property
        /// </summary>
        /// <example>Flat 3</example>
        public string SubBuildingName
        {
            get
            {
                return this.subBuildingName;
            }
            set
            {
                this.subBuildingName = value;
            }
        }

        /// <summary>
        /// Gets or sets the address of a Post Office box
        /// </summary>
        public string POBoxNumber
        {
            get
            {
                return this.pOBoxNumber;
            }
            set
            {
                this.pOBoxNumber = value;
            }
        }

        /// <summary>
        /// Gets or sets a subdivision of a main organisation which receives mail at a distinct delivery point
        /// </summary>
        public string DepartmentName
        {
            get
            {
                return this.departmentName;
            }
            set
            {
                this.departmentName = value;
            }
        }

        /// <summary>
        /// Gets or sets the business name given to a delivery point within a building or small group of buildings
        /// </summary>
        public string OrganisationName
        {
            get
            {
                return this.organisationName;
            }
            set
            {
                this.organisationName = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// PAF-based address in Great Britain, supplied by Ordnance Survey ADDRESS-POINT from Royal Mail data
        /// </summary>
        public AddressPointAddress()
        {
        }

        /// <summary>
        /// PAF-based address in Great Britain, supplied by Ordnance Survey ADDRESS-POINT from Royal Mail data
        /// </summary>
        /// <param name="row">A DataRow containing some or all of the Address point field mnemonics</param>
        public AddressPointAddress(DataRow row)
        {
            this.InitialiseControl(row);
        }

        /// <summary>
        /// PAF-based address in Great Britain, supplied by Ordnance Survey ADDRESS-POINT from Royal Mail data
        /// </summary>
        /// <param name="row">A DataRowView containing some or all of the Address point field mnemonics</param>
        /// <exception cref="System.ArgumentNullException" />
        public AddressPointAddress(DataRowView row)
        {
            if (row == null) throw new ArgumentNullException("row");

            this.InitialiseControl(row.Row);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Populate properties from a DataRow of ADDRESS-POINT data
        /// </summary>
        /// <param name="row">A DataRow containing some or all of the Address point field mnemonics</param>
        private void InitialiseControl(DataRow row)
        {
            if (row != null)
            {
                this.DepartmentName = row["DP"].ToString().Trim();
                this.POBoxNumber = row["PB"].ToString().Trim();
                //this.OrganisationName = row["ON"].ToString().Trim();
                this.BuildingNumber = row["BN"].ToString().Trim();
                this.SubBuildingName = row["SB"].ToString().Trim();
                this.BuildingName = row["BD"].ToString().Trim();
                this.ThoroughfareName = row["TN"].ToString().Trim();
                this.DependentThoroughfareName = row["DR"].ToString().Trim();
                this.PostTown = row["PT"].ToString().Trim();
                this.DependentLocalityName = row["DL"].ToString().Trim();
                //this.DoubleDependentLocalityName = row["DD"].ToString().Trim();
                this.PostalCounty = row["CN"].ToString().Trim();
                this.Postcode = row["PC"].ToString().Trim();
            }
        }

        /// <summary>
        /// Get a 5-line Simple Address from the ADDRESS-POINT address, formatted according to Royal Mail guidelines
        /// </summary>
        /// <returns></returns>
        public SimpleAddress GetSimpleAddress()
        {
            // *** Build six elements of an ADDRESS-POINT postal address ***
            TextInfo textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
            string separator = textInfo.ListSeparator + " ";

            // *** Organisation elements ***
            StringBuilder orgElement = new StringBuilder(textInfo.ToTitleCase(this.organisationName.ToLower(CultureInfo.CurrentCulture)));
            if (this.departmentName.Length > 0)
            {
                if (orgElement.Length > 0) orgElement.Append(separator);
                orgElement.Append(textInfo.ToTitleCase(this.departmentName.ToLower(CultureInfo.CurrentCulture)));
            }

            // *** PO box element *** goes here, but there's only one field so nothing to do

            // *** Premises elements ***
            StringBuilder premElement = new StringBuilder();

            bool subBuildingNameIsFormat1 = AddressPointAddress.IsFormat1Name(this.subBuildingName);
            bool buildingNameIsFormat1 = AddressPointAddress.IsFormat1Name(this.buildingName);
            bool premisesOnSeparateLine = ((this.subBuildingName.Length > 0 && !subBuildingNameIsFormat1) || (this.buildingName.Length > 0 && !buildingNameIsFormat1));

            // sub-building name should not exist without a building name or building number
            if (this.subBuildingName.Length > 0 && (this.buildingName.Length > 0 || this.buildingNumber.Length > 0))
            {
                premElement.Append(textInfo.ToTitleCase(this.subBuildingName.ToLower(CultureInfo.CurrentCulture)));
            }

            if (this.buildingName.Length > 0)
            {
                if (premElement.Length > 0)
                {
                    if (subBuildingNameIsFormat1) premElement.Append(" ");
                    else premElement.Append(separator);
                }
                premElement.Append(textInfo.ToTitleCase(this.buildingName.ToLower(CultureInfo.CurrentCulture)));
            }

            // *** Thoroughfare elements ***
            StringBuilder thoroElement = new StringBuilder();

            // Dependent thoroughfare name should not exist without thoroughfare name
            if (this.dependentThoroughfareName.Length > 0 && this.thoroughfareName.Length > 0) thoroElement.Append(textInfo.ToTitleCase(this.dependentThoroughfareName.ToLower(CultureInfo.CurrentCulture)));

            if (this.thoroughfareName.Length > 0)
            {
                if (thoroElement.Length > 0) thoroElement.Append(separator);
                thoroElement.Append(textInfo.ToTitleCase(this.thoroughfareName.ToLower(CultureInfo.CurrentCulture)));
            }

            // *** Locality elements ***
            StringBuilder locaElement = new StringBuilder();

            // Double-dependent locality should not exist without a dependent locality
            if (this.doubleDependentLocalityName.Length > 0 && this.dependentLocalityName.Length > 0) locaElement.Append(textInfo.ToTitleCase(this.doubleDependentLocalityName.ToLower(CultureInfo.CurrentCulture)));

            if (this.dependentLocalityName.Length > 0)
            {
                if (locaElement.Length > 0) locaElement.Append(separator);
                locaElement.Append(textInfo.ToTitleCase(this.dependentLocalityName.ToLower(CultureInfo.CurrentCulture)));
            }
            if (this.postTown.Length > 0)
            {
                if (locaElement.Length > 0) locaElement.Append(separator);
                locaElement.Append(textInfo.ToTitleCase(this.postTown.ToLower(CultureInfo.CurrentCulture)));
            }
            if (this.postalCounty.Length > 0)
            {
                if (locaElement.Length > 0) locaElement.Append(separator);
                locaElement.Append(textInfo.ToTitleCase(this.postalCounty.ToLower(CultureInfo.CurrentCulture)));
            }

            // Building number goes in front of thoroughfare element or locality element
            if (this.buildingNumber.Length > 0)
            {
                if (thoroElement.Length > 0) thoroElement.Insert(0, this.buildingNumber + " ");
                else if (locaElement.Length > 0) locaElement.Insert(0, this.buildingNumber + " ");
            }

            // Sub-building name and building name may have to go in front again if Format 1
            if (!premisesOnSeparateLine && premElement.Length > 0)
            {
                if (thoroElement.Length > 0)
                {
                    thoroElement.Insert(0, premElement.ToString() + " ");
                    premElement.Remove(0, premElement.Length);
                }
                else if (locaElement.Length > 0)
                {
                    locaElement.Insert(0, premElement.ToString() + " ");
                    premElement.Remove(0, premElement.Length);
                }
            }


            // *** Postcode element *** goes here, but there's only one field so nothing to do

            // Now combine into five lines
            return BuildSimpleAddress(orgElement, premElement, thoroElement, locaElement);
        }

        /// <summary>
        /// Builds a simple address as part of <seealso cref="GetSimpleAddress()"/>.
        /// </summary>
        /// <param name="orgElement">The org element.</param>
        /// <param name="premElement">The prem element.</param>
        /// <param name="thoroElement">The thoro element.</param>
        /// <param name="locaElement">The loca element.</param>
        /// <returns></returns>
        private SimpleAddress BuildSimpleAddress(StringBuilder orgElement, StringBuilder premElement, StringBuilder thoroElement, StringBuilder locaElement)
        {
            string[] lines = new string[5];
            int lineCount = 0;

            if (orgElement.Length > 0)
            {
                lines[lineCount] = orgElement.ToString();
                lineCount++;
            }

            if (this.pOBoxNumber.Length > 0)
            {
                lines[lineCount] = Resources.POBoxPrefix + this.pOBoxNumber;
                lineCount++;
            }

            if (premElement.Length > 0)
            {
                lines[lineCount] = premElement.ToString();
                lineCount++;
            }

            if (thoroElement.Length > 0)
            {
                lines[lineCount] = thoroElement.ToString();
                lineCount++;
            }

            if (locaElement.Length > 0)
            {
                lines[lineCount] = locaElement.ToString();
                lineCount++;
            }

            if (this.postcode.Length > 0)
            {
                if (lineCount < 4) lines[lineCount] = AddressInfo.AddSpaceToPostcode(this.postcode);
                else lines[4] += "  " + AddressInfo.AddSpaceToPostcode(this.postcode);
            }

            // Put 5 lines into simple address
            SimpleAddress addr = new SimpleAddress();
            addr.AddressLine1 = lines[0];
            addr.AddressLine2 = lines[1];
            addr.AddressLine3 = lines[2];
            addr.AddressLine4 = lines[3];
            addr.AddressLine5 = lines[4];
            return addr;
        }


        /// <summary>
        /// Gets whether the specified name is a "Format 1" name: ie, whether it is a building name or sub-building name which should on the same line as other address elements
        /// </summary>
        /// <param name="name">A building name or sub-building name</param>
        /// <returns>True if name begins and ends with a number, or begins with a number and ends with a number and alphanumeric character; false otherwise</returns>
        private static bool IsFormat1Name(string name)
        {
            string firstChar = "";
            string lastChar = "";
            string lastPair = "";

            if (name.Length >= 1)
            {
                firstChar = name.Substring(0, 1);
                lastChar = name.Substring(name.Length - 1);
                if (name.Length >= 2) lastPair = name.Substring(name.Length - 2);
            }
            else return false;

            Regex numeric = new Regex("^[0-9]$");
            Regex numericAlpha = new Regex("^[0-9][A-Z]$", RegexOptions.IgnoreCase);

            return (numeric.IsMatch(firstChar) && (numeric.IsMatch(lastChar) || numericAlpha.IsMatch(lastPair)));
        }

        #endregion
    }
}
