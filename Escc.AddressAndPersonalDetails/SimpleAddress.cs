using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Escc.AddressAndPersonalDetails
{
    /// <summary>
    /// A UK Postal Address is a standard 5-line address format, which is e-GIF compliant (though BS7666 is preferred)
    /// </summary>
    [Serializable]
    public class SimpleAddress
    {
        #region Properties

        /// <summary>
        /// Line 1 of the Simple Address
        /// </summary>
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Line 2 of the Simple Address
        /// </summary>
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Line 3 of the Simple Address
        /// </summary>
        public string AddressLine3 { get; set; }

        /// <summary>
        /// Line 4 of the Simple Address
        /// </summary>
        public string AddressLine4 { get; set; }

        /// <summary>
        /// Line 5 of the Simple Address
        /// </summary>
        public string AddressLine5 { get; set; }

        /// <summary>
        /// Line 6 of the Simple Address
        /// </summary>
        public string AddressLine6 { get; set; }

        /// <summary>
        /// Line 7 of the Simple Address
        /// </summary>
        public string AddressLine7 { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// A Simple Address is a standard 5-line address format, which is e-GIF compliant (though BS7666 is preferred)
        /// </summary>
        public SimpleAddress()
        {
            Initialise();
        }

        private void Initialise()
        {
            AddressLine7 = "";
            AddressLine6 = "";
            AddressLine5 = "";
            AddressLine4 = "";
            AddressLine3 = "";
            AddressLine2 = "";
            AddressLine1 = "";
        }

        /// <summary>
        /// A Simple Address is a standard 5-line address format, which is e-GIF compliant (though BS7666 is preferred)
        /// </summary>
        /// <param name="line1">Line 1 of the address</param>
        /// <param name="line2">Line 2 of the address</param>
        /// <param name="line3">Line 3 of the address</param>
        /// <param name="line4">Line 4 of the address</param>
        /// <param name="line5">Line 5 of the address</param>
        /// <remarks>This constructor retained for backwards compatibility. Only known use is with SimpleAddressEditControl - when that's gone, this can go too.</remarks>
        public SimpleAddress(string line1, string line2, string line3, string line4, string line5)
        {
            Initialise();
            this.AddressLine1 = line1;
            this.AddressLine2 = line2;
            this.AddressLine3 = line3;
            this.AddressLine4 = line4;
            this.AddressLine5 = line5;
        }

        /// <summary>
        /// A Simple Address is a 7-line address format
        /// </summary>
        /// <param name="addressLines">Lines of the simple address</param>
        public SimpleAddress(params string[] addressLines)
        {
            Initialise();
            if (addressLines != null)
            {
                if (addressLines.Length > 0) this.AddressLine1 = addressLines[0];
                if (addressLines.Length > 1) this.AddressLine2 = addressLines[2];
                if (addressLines.Length > 2) this.AddressLine3 = addressLines[3];
                if (addressLines.Length > 3) this.AddressLine4 = addressLines[4];
                if (addressLines.Length > 4) this.AddressLine5 = addressLines[5];
                if (addressLines.Length > 5) this.AddressLine6 = addressLines[6];
                if (addressLines.Length > 6) this.AddressLine7 = addressLines[7];
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// A Simple Address object is converted into a ESCC House Style approved address
        /// </summary>
        /// <param name="separator">Used to divide the address elements</param>
        /// <returns>Formatted address that conforms to ESCC House Style</returns>
        public string ToString(string separator)
        {
            StringBuilder sb = new StringBuilder(this.AddressLine1);
            if (this.AddressLine2 != null && this.AddressLine2.Length > 0)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(this.AddressLine2);
            }

            if (this.AddressLine3 != null && this.AddressLine3.Length > 0)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(this.AddressLine3);
            }

            if (this.AddressLine4 != null && this.AddressLine4.Length > 0)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(this.AddressLine4);
            }

            if (this.AddressLine5 != null && this.AddressLine5.Length > 0)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(this.AddressLine5);
            }

            return sb.ToString();

        }

        /// <summary>
        /// When ToString() is called the overriden method fires and passes a default comma
        /// to separate the elements of the address. The ToString() can take any separtor.
        /// </summary>
        /// <returns>A formatted string</returns>
        /// <example>if default ToString() = 1 Rotten Row, Lewes, BN71SG</example>
        public override string ToString()
        {
            return this.ToString(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + " ");
        }


        /// <summary>
        /// Gets whether the specified name is a "Format 1" name: ie, whether it is a building name or sub-building name which should on the same line as other address elements
        /// </summary>
        /// <param name="name">A building name or sub-building name</param>
        /// <returns>True if name begins and ends with a number, or begins with a number and ends with a number and alphanumeric character; false otherwise</returns>
        /// <remarks>"Format 1" is PAF terminology, and code here is based on PAF rules</remarks>
        public static bool IsFormat1Name(string name)
        {
            if (name == null) return true; // if there's no info, no point taking up a line with it

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
