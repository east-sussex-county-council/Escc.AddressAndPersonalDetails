using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Threading;


namespace EsccWebTeam.Gdsc
{
    /// <summary>
    /// A UK Postal Address is a standard 5-line address format, which is e-GIF compliant (though BS7666 is preferred)
    /// </summary>
    [Serializable]
    public class SimpleAddress
    {
        #region Fields

        /// <summary>
        /// Store line 1 of the Simple Address
        /// </summary>
        private string addressLine1 = "";

        /// <summary>
        /// Store line 2 of the Simple Address
        /// </summary>
        private string addressLine2 = "";

        /// <summary>
        /// Store line 3 of the Simple Address
        /// </summary>
        private string addressLine3 = "";

        /// <summary>
        /// Store line 4 of the Simple Address
        /// </summary>
        private string addressLine4 = "";

        /// <summary>
        /// Store line 5 of the Simple Address
        /// </summary>
        private string addressLine5 = "";

        /// <summary>
        /// Store line 6 of Simple Address
        /// </summary>
        private string addressLine6 = "";

        /// <summary>
        /// Store line 7 of Simple Address
        /// </summary>
        private string addressLine7 = "";



        #endregion

        #region Properties

        /// <summary>
        /// Line 1 of the Simple Address
        /// </summary>
        public string AddressLine1
        {
            get
            {
                return this.addressLine1;
            }
            set
            {
                this.addressLine1 = value;
            }
        }

        /// <summary>
        /// Line 2 of the Simple Address
        /// </summary>
        public string AddressLine2
        {
            get
            {
                return this.addressLine2;
            }
            set
            {
                this.addressLine2 = value;
            }
        }

        /// <summary>
        /// Line 3 of the Simple Address
        /// </summary>
        public string AddressLine3
        {
            get
            {
                return this.addressLine3;
            }
            set
            {
                this.addressLine3 = value;
            }
        }

        /// <summary>
        /// Line 4 of the Simple Address
        /// </summary>
        public string AddressLine4
        {
            get
            {
                return this.addressLine4;
            }
            set
            {
                this.addressLine4 = value;
            }
        }

        /// <summary>
        /// Line 5 of the Simple Address
        /// </summary>
        public string AddressLine5
        {
            get
            {
                return this.addressLine5;
            }
            set
            {
                this.addressLine5 = value;
            }
        }

        /// <summary>
        /// Line 6 of the Simple Address
        /// </summary>
        public string AddressLine6
        {
            get
            {
                return this.addressLine6;
            }
            set
            {
                this.addressLine6 = value;
            }
        }

        /// <summary>
        /// Line 7 of the Simple Address
        /// </summary>
        public string AddressLine7
        {
            get
            {
                return this.addressLine7;
            }
            set
            {
                this.addressLine7 = value;
            }
        }



        #endregion

        #region Constructors

        /// <summary>
        /// A Simple Address is a standard 5-line address format, which is e-GIF compliant (though BS7666 is preferred)
        /// </summary>
        public SimpleAddress() { }

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
            this.addressLine1 = line1;
            this.addressLine2 = line2;
            this.addressLine3 = line3;
            this.addressLine4 = line4;
            this.addressLine5 = line5;
        }

        /// <summary>
        /// A Simple Address is a 7-line address format
        /// </summary>
        /// <param name="addressLines">Lines of the simple address</param>
        public SimpleAddress(params string[] addressLines)
        {
            if (addressLines != null)
            {
                if (addressLines.Length > 0) this.addressLine1 = addressLines[0];
                if (addressLines.Length > 1) this.addressLine2 = addressLines[2];
                if (addressLines.Length > 2) this.addressLine3 = addressLines[3];
                if (addressLines.Length > 3) this.addressLine4 = addressLines[4];
                if (addressLines.Length > 4) this.addressLine5 = addressLines[5];
                if (addressLines.Length > 5) this.addressLine6 = addressLines[6];
                if (addressLines.Length > 6) this.addressLine7 = addressLines[7];
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
            StringBuilder sb = new StringBuilder(this.addressLine1);
            if (this.addressLine2 != null && this.addressLine2.Length > 0)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(this.addressLine2);
            }

            if (this.addressLine3 != null && this.addressLine3.Length > 0)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(this.addressLine3);
            }

            if (this.addressLine4 != null && this.addressLine4.Length > 0)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(this.addressLine4);
            }

            if (this.addressLine5 != null && this.addressLine5.Length > 0)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(this.addressLine5);
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
        /// Gets the address as a string of XHTML.
        /// </summary>
        /// <param name="multipleLines">Break the separate lines of the address with an XHTML line break (&gt;br /&lt;)</param>
        /// <returns>If <c>multipleLines</c> is <c>true</c>, a paragraph with line breaks is returned; if <c>false</c>, a single line of XHTML</returns>
        public string ToXhtmlString(bool multipleLines)
        {
            return this.ToXhtmlString(multipleLines, multipleLines);
        }

        /// <summary>
        /// Gets the address as a string of XHTML.
        /// </summary>
        /// <param name="multipleLines">Break the separate lines of the address with an XHTML line break (&lt;br /&gt;)</param>
        /// <param name="withPara">If <c>true</c>, encloses the address in a paragraph element</param>
        /// <returns>If <c>multipleLines</c> is <c>true</c>, an address with line breaks is returned; if <c>false</c>, a single line of XHTML. If <c>withPara</c> is <c>true</c>, the address will be enclosed in a paragraph. If there's no address, an empty string is returned.</returns>
        public string ToXhtmlString(bool multipleLines, bool withPara)
        {
            string xhtml = "";
            if (multipleLines)
            {
                xhtml = this.ToString("<br />");
            }
            else
            {
                xhtml = Regex.Replace(this.ToString(), "^([0-9]+[A-Z]?) ", "<span class=\"addrBuildingNumber\">$1</span> ", RegexOptions.IgnoreCase);
            }

            if (xhtml.Length > 0 && withPara) xhtml = "<p>" + xhtml + "</p>";
            return xhtml;
        }


        /// <summary>
        /// Gets whether the specified name is a "Format 1" name: ie, whether it is a building name or sub-building name which should on the same line as other address elements
        /// </summary>
        /// <param name="name">A building name or sub-building name</param>
        /// <returns>True if name begins and ends with a number, or begins with a number and ends with a number and alphanumeric character; false otherwise</returns>
        /// <remarks>"Format 1" is PAF terminology, and code here is based on PAF rules</remarks>
        internal static bool IsFormat1Name(string name)
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
