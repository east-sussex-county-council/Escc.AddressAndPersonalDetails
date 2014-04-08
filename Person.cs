#region Using Directives
using System.Xml.Serialization;
using System;
#endregion

namespace EsccWebTeam.Gdsc
{
    /// <summary>
    /// Person definition from the <a href="http://www.govtalk.gov.uk/schemasstandards/schemalibrary_list.asp?subjects=17">Address and Personal Details Standard v2.0</a>
    /// </summary>
    [Serializable]
    public class Person
    {

        #region Fields

        private PersonName name;

        private int id;

        private UKContactNumberCollection telephoneNumbers;
        private UKContactNumberCollection faxNumbers;
        private ContactEmailCollection emailAddresses;
        private BS7666AddressCollection addresses;

        #endregion Fields

        #region Properties




        /// <summary>
        /// Gets or sets the ID if the details are stored in a database.
        /// </summary>
        /// <value>The ID.</value>
        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public PersonName Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets the email addresses.
        /// </summary>
        /// <value>The email addresses.</value>
        public ContactEmailCollection EmailAddresses
        {
            get
            {
                return this.emailAddresses;
            }

        }


        /// <summary>
        /// Gets the telephone numbers.
        /// </summary>
        /// <value>The telephone numbers.</value>
        public UKContactNumberCollection TelephoneNumbers
        {
            get
            {
                return this.telephoneNumbers;
            }

        }

        /// <summary>
        /// Gets the fax numbers.
        /// </summary>
        /// <value>The fax numbers.</value>
        public UKContactNumberCollection FaxNumbers
        {
            get
            {
                return this.faxNumbers;
            }
        }

        /// <summary>
        /// Gets the person's addresses.
        /// </summary>
        /// <value>The addresses.</value>
        public BS7666AddressCollection Addresses
        {
            get
            {
                return this.addresses;
            }

        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
            this.Name = new PersonName();
            this.telephoneNumbers = new UKContactNumberCollection();
            this.faxNumbers = new UKContactNumberCollection();
            this.emailAddresses = new ContactEmailCollection();
            this.addresses = new BS7666AddressCollection();
        }

        #endregion Constructors
    }
}
