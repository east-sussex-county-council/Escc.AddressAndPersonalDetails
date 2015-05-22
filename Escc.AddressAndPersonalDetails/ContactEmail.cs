using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Escc.AddressAndPersonalDetails.Properties;

namespace Escc.AddressAndPersonalDetails
{
    /// <summary>
    /// The string of characters that identifies an addressee's post box on the Internet. (In other words it's an email address.)
    /// </summary>
    /// <remarks>Format is from the <a href="http://www.govtalk.gov.uk/schemasstandards/schemalibrary_list.asp?subjects=17">Address and Personal Details Standard v2.0</a></remarks>
    public class ContactEmail
    {

        #region Fields

        private string emailAddress;
        private string displayName;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UKContactNumber"/> is preferred.
        /// </summary>
        /// <value><c>true</c> if preferred; otherwise, <c>false</c>.</value>
        public bool Preferred { get; set; }

        /// <summary>
        /// Gets or sets the usage.
        /// </summary>
        /// <value>The usage.</value>
        public ContactUsage Usage { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>The email address.</value>
        public string EmailAddress
        {
            get
            {
                return this.emailAddress;
            }
            set
            {
                // Regex taken from EmailAddressType in the Address and Personal Details v2.0 standard.
                // http://www.govtalk.gov.uk/gdsc/schemaHtml/CommonSimpleTypes-v1-3-xsd-EmailAddressType.htm
                string pattern = @"[0-9A-Za-z'\.\-_]{1,127}@[0-9A-Za-z'\.\-_]{1,127}";

                if (Regex.IsMatch(value, pattern))
                {

                    this.emailAddress = value;
                }
                else
                {
                    throw new FormatException(String.Format(CultureInfo.InvariantCulture, Resources.ErrorEmailInvalid, pattern));
                }
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        /// <remarks>This is not part of the Address and Personal Details v2.0 standard which is why it's set not to serialise.</remarks>
        [XmlIgnore]
        public string DisplayName
        {
            get
            {
                if (!String.IsNullOrEmpty(this.displayName)) return this.displayName;
                else return this.emailAddress;
            }
            set
            {
                this.displayName = value;
            }
        }

        #endregion Properties


        #region Constructors


        /// <summary>
        /// Initializes a new instance of the <see cref="ContactEmail"/> class.
        /// </summary>
        public ContactEmail()
        {
            this.Initialise();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactEmail"/> class.
        /// </summary>
        /// <param name="emailAddress">The email address</param>
        public ContactEmail(string emailAddress)
        {
            this.Initialise();
            this.emailAddress = emailAddress;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContactEmail"/> class.
        /// </summary>
        /// <param name="emailAddress">The email address</param>
        /// <param name="displayName">The display name.</param>
        public ContactEmail(string emailAddress, string displayName)
        {
            this.Initialise();
            this.emailAddress = emailAddress;
            this.displayName = displayName;
        }

        /// <summary>
        /// Initialises this instance.
        /// </summary>
        private void Initialise()
        {
            this.Usage = ContactUsage.NotSpecified;
        }

        #endregion Constructors


        #region Methods

        /// <summary>
        /// Gets the email address
        /// </summary>
        /// <returns>The email address</returns>
        public override string ToString()
        {
            return this.emailAddress;
        }

        #endregion Methods

    }
}
