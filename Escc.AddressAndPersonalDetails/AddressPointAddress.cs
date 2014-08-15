using System;
using System.Data;
using System.Globalization;
using System.Text;
using Escc.AddressAndPersonalDetails.Properties;

namespace Escc.AddressAndPersonalDetails
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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets an abbreviated form of address referring to one or a collection of addresses that conforms to a specification set by Royal Mail
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// Gets or sets the county name (no longer used by Address Point)
        /// </summary>
        public string PostalCounty { get; set; }

        /// <summary>
        /// Gets or sets the town or city in which is located the Royal Mail sorting office from which mail is delivered to its final recipient
        /// </summary>
        public string PostTown { get; set; }

        /// <summary>
        /// Gets or sets an area within a post town
        /// </summary>
        public string DependentLocalityName { get; set; }

        /// <summary>
        /// Gets or sets an area used to distinguish between similar or same thoroughfares within a dependent locality
        /// </summary>
        public string DoubleDependentLocalityName { get; set; }

        /// <summary>
        /// Gets or sets a road, track or named access route on which there are Royal Mail delivery points
        /// </summary>
        public string ThoroughfareName { get; set; }

        /// <summary>
        /// Gets or sets a named thoroughfare which is within another named thoroughfare
        /// </summary>
        public string DependentThoroughfareName { get; set; }

        /// <summary>
        /// Gets or sets a number given to a single building or small group of buildings which identifies it from its neighbours; aka postal number
        /// </summary>
        public string BuildingNumber { get; set; }

        /// <summary>
        /// Gets or sets a description applied to a single building or small group of buildings
        /// </summary>
        public string BuildingName { get; set; }

        /// <summary>
        /// Gets or sets a name and/or number identifying a subdivision of a property
        /// </summary>
        /// <example>Flat 3</example>
        public string SubBuildingName { get; set; }

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
            InitialiseControl(null);
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
            SubBuildingName = "";
            BuildingName = "";
            BuildingNumber = "";
            DependentThoroughfareName = "";
            ThoroughfareName = "";
            DoubleDependentLocalityName = "";
            DependentLocalityName = "";
            PostTown = "";
            PostalCounty = "";
            Postcode = "";

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

            bool subBuildingNameIsFormat1 = SimpleAddress.IsFormat1Name(this.SubBuildingName);
            bool buildingNameIsFormat1 = SimpleAddress.IsFormat1Name(this.BuildingName);
            bool premisesOnSeparateLine = ((this.SubBuildingName.Length > 0 && !subBuildingNameIsFormat1) || (this.BuildingName.Length > 0 && !buildingNameIsFormat1));

            // sub-building name should not exist without a building name or building number
            if (this.SubBuildingName.Length > 0 && (this.BuildingName.Length > 0 || this.BuildingNumber.Length > 0))
            {
                premElement.Append(textInfo.ToTitleCase(this.SubBuildingName.ToLower(CultureInfo.CurrentCulture)));
            }

            if (this.BuildingName.Length > 0)
            {
                if (premElement.Length > 0)
                {
                    if (subBuildingNameIsFormat1) premElement.Append(" ");
                    else premElement.Append(separator);
                }
                premElement.Append(textInfo.ToTitleCase(this.BuildingName.ToLower(CultureInfo.CurrentCulture)));
            }

            // *** Thoroughfare elements ***
            StringBuilder thoroElement = new StringBuilder();

            // Dependent thoroughfare name should not exist without thoroughfare name
            if (this.DependentThoroughfareName.Length > 0 && this.ThoroughfareName.Length > 0) thoroElement.Append(textInfo.ToTitleCase(this.DependentThoroughfareName.ToLower(CultureInfo.CurrentCulture)));

            if (this.ThoroughfareName.Length > 0)
            {
                if (thoroElement.Length > 0) thoroElement.Append(separator);
                thoroElement.Append(textInfo.ToTitleCase(this.ThoroughfareName.ToLower(CultureInfo.CurrentCulture)));
            }

            // *** Locality elements ***
            StringBuilder locaElement = new StringBuilder();

            // Double-dependent locality should not exist without a dependent locality
            if (this.DoubleDependentLocalityName.Length > 0 && this.DependentLocalityName.Length > 0) locaElement.Append(textInfo.ToTitleCase(this.DoubleDependentLocalityName.ToLower(CultureInfo.CurrentCulture)));

            if (this.DependentLocalityName.Length > 0)
            {
                if (locaElement.Length > 0) locaElement.Append(separator);
                locaElement.Append(textInfo.ToTitleCase(this.DependentLocalityName.ToLower(CultureInfo.CurrentCulture)));
            }
            if (this.PostTown.Length > 0)
            {
                if (locaElement.Length > 0) locaElement.Append(separator);
                locaElement.Append(textInfo.ToTitleCase(this.PostTown.ToLower(CultureInfo.CurrentCulture)));
            }
            if (this.PostalCounty.Length > 0)
            {
                if (locaElement.Length > 0) locaElement.Append(separator);
                locaElement.Append(textInfo.ToTitleCase(this.PostalCounty.ToLower(CultureInfo.CurrentCulture)));
            }

            // Building number goes in front of thoroughfare element or locality element
            if (this.BuildingNumber.Length > 0)
            {
                if (thoroElement.Length > 0) thoroElement.Insert(0, this.BuildingNumber + " ");
                else if (locaElement.Length > 0) locaElement.Insert(0, this.BuildingNumber + " ");
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

            if (this.Postcode.Length > 0)
            {
                if (lineCount < 4) lines[lineCount] = AddressInfo.AddSpaceToPostcode(this.Postcode);
                else lines[4] += "  " + AddressInfo.AddSpaceToPostcode(this.Postcode);
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

        #endregion
    }
}
