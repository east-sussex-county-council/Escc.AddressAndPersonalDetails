using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Escc.AddressAndPersonalDetails
{
    /// <summary>
    /// A number, including any exchange or location code, at which a person or organisation can be contacted in the UK by telephonic means.
    /// </summary>
    /// <remarks>Format is from the <a href="http://www.govtalk.gov.uk/schemasstandards/schemalibrary_list.asp?subjects=17">Address and Personal Details Standard v2.0</a></remarks>
    public class UKContactNumber
    {
        private string nationalNumber;

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UKContactNumber"/> is preferred.
        /// </summary>
        /// <value><c>true</c> if preferred; otherwise, <c>false</c>.</value>
        public bool Preferred { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UKContactNumber"/> is a mobile phone.
        /// </summary>
        /// <value><c>true</c> if a mobile phone; otherwise, <c>false</c>.</value>
        public bool Mobile { get; set; }


        /// <summary>
        /// Gets or sets the usage.
        /// </summary>
        /// <value>The usage.</value>
        public ContactUsage Usage { get; set; }


        /// <summary>
        /// Gets or sets the country code.
        /// </summary>
        /// <value>The country code.</value>
        public string CountryCode { get; set; }


        /// <summary>
        /// Gets or sets the extension number.
        /// </summary>
        /// <value>The extension number.</value>
        public string ExtensionNumber { get; set; }


        /// <summary>
        /// Gets or sets the national number.
        /// </summary>
        /// <value>The national number.</value>
        /// <example>01273 481000</example>
        public string NationalNumber
        {
            get
            {
                return this.nationalNumber;
            }
            set
            {
                if (value != null)
                {
                    this.nationalNumber = value.Replace("(", "").Replace(")", "").Replace("-", " ");
                    if (Regex.IsMatch(this.nationalNumber, "^[0-9]{11,11}$")) this.nationalNumber = this.nationalNumber.Substring(0, 5) + " " + this.nationalNumber.Substring(5);
                }
                else
                {
                    this.nationalNumber = String.Empty;
                }
            }
        }

        #endregion Properties


        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UKContactNumber"/> class.
        /// </summary>
        public UKContactNumber()
        {
            this.Initialise();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UKContactNumber"/> class.
        /// </summary>
        /// <param name="nationalNumber">The national number (eg 01273 481000)</param>
        public UKContactNumber(string nationalNumber)
        {
            this.Initialise();
            this.NationalNumber = nationalNumber;
        }

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        private void Initialise()
        {
            this.CountryCode = "44";
            this.Usage = ContactUsage.NotSpecified;
        }

        #endregion Constructors


        #region Methods

        /// <summary>
        /// Gets the complete telephone number, including the extension, as it should be called from within the UK
        /// </summary>
        /// <returns>Telephone number</returns>
        /// <example>01273 481000 ext 1001</example>
        public string ToUKString()
        {
            StringBuilder num = new StringBuilder();
            num.Append(this.nationalNumber);
            if (this.ExtensionNumber != null && this.ExtensionNumber.Length > 0)
            {
                num.Append(Resources.ContactExtension);
                num.Append(this.ExtensionNumber);
            }

            return num.ToString();
        }

        /// <summary>
        /// Gets the complete telephone number as it always should be transferred for international operation and clarity as to which country (in this case UK) the number belongs.
        /// </summary>
        /// <returns>The telephone number</returns>
        /// <example>+441273481000</example>
        public override string ToString()
        {
            StringBuilder num = new StringBuilder();
            num.Append("+");
            num.Append(this.CountryCode);

            if (this.nationalNumber.StartsWith("0", StringComparison.Ordinal))
            {
                num.Append(this.nationalNumber.Substring(1));
            }
            else
            {
                num.Append(this.nationalNumber);
            }

            return num.ToString();
        }

        #endregion Methods

    }
}
