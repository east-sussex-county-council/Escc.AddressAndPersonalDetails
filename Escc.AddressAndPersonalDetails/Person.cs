#region Using Directives

using System;

#endregion

namespace Escc.AddressAndPersonalDetails
{
    /// <summary>
    /// Person definition from the <a href="http://www.govtalk.gov.uk/schemasstandards/schemalibrary_list.asp?subjects=17">Address and Personal Details Standard v2.0</a>
    /// </summary>
    public class Person
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ID if the details are stored in a database.
        /// </summary>
        /// <value>The ID.</value>
        public int Id { get; set; }


        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public PersonName Name { get; set; }

        /// <summary>
        /// Gets the email addresses.
        /// </summary>
        /// <value>The email addresses.</value>
        public ContactEmailCollection EmailAddresses { get; private set; }


        /// <summary>
        /// Gets the telephone numbers.
        /// </summary>
        /// <value>The telephone numbers.</value>
        public UKContactNumberCollection TelephoneNumbers { get; private set; }

        /// <summary>
        /// Gets the fax numbers.
        /// </summary>
        /// <value>The fax numbers.</value>
        public UKContactNumberCollection FaxNumbers { get; private set; }

        /// <summary>
        /// Gets the person's addresses.
        /// </summary>
        /// <value>The addresses.</value>
        public BS7666AddressCollection Addresses { get; private set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        public Person()
        {
            this.Name = new PersonName();
            this.TelephoneNumbers = new UKContactNumberCollection();
            this.FaxNumbers = new UKContactNumberCollection();
            this.EmailAddresses = new ContactEmailCollection();
            this.Addresses = new BS7666AddressCollection();
        }

        #endregion Constructors
    }
}
