using System;
using System.Text;

namespace Escc.AddressAndPersonalDetails
{
    /// <summary>
    /// Person name definition from the <a href="http://www.govtalk.gov.uk/schemasstandards/schemalibrary_list.asp?subjects=17">Address and Personal Details Standard v2.0</a>
    /// </summary>
    public class PersonName
    {

        #region Fields

        private string familyName;
        private string initials;
        private string fullName;
        private string requestedName;

        #endregion Fields

        #region Properties

        /// <summary>
        /// The name a person wishes to use which is different from the values in Title, Given Name(s), Family Name and Name Suffix fields.
        /// </summary>
        /// <value>The requested name.</value>
        public string RequestedName
        {
            get
            {
                return this.requestedName;
            }
            set
            {
                string reqName = value;
                if (reqName != null)
                {
                    reqName = reqName.Trim();
                    if (reqName.Length > 70) reqName = reqName.Substring(0, 70);
                }

                this.requestedName = reqName;
            }
        }


        /// <summary>
        /// The full name of a person. This is an unstructured concatenation of some or all of the Person Title, Person Given Name, Person Family Name and Person Name Suffix elements, or other elements that make up a person's full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                string full = value;
                if (full != null)
                {
                    full = full.Trim();
                    if (full.Length > 70) full = full.Substring(0, 70);
                }

                this.fullName = full;
            }
        }

        /// <summary>
        /// Gets a collection of textual suffixes that may be added to the end of a person's name.
        /// </summary>
        /// <value>The suffixes.</value>
        /// <example>OBE, MBE, BSc, JP, GM</example>
        public PersonPartialNameCollection Suffixes { get; private set; }


        /// <summary>
        /// Gets or sets a person's initials for given names after the first given name.
        /// </summary>
        /// <value>A person's initials.</value>
        /// <example>For John Fred Harry Smith, the initials are "F H"</example>
        /// <remarks>Initials is defined in the XML schema but doesn't seem to be used anywhere. 
        /// It is shown on the <a href="http://www.govtalk.gov.uk/gdsc/html/frames/imagemaps/PersonNameUML.htm">UML diagram</a> as part of the structured person name.</remarks>
        public string Initials
        {
            get
            {
                return this.initials;
            }
            set
            {
                //TODO: implement "with a space between each initial"
                this.initials = value;
            }
        }


        /// <summary>
        /// Gets or sets that part of a person's name which is used to describe family, clan, tribal group, or marital association.
        /// </summary>
        /// <value>The family name.</value>
        /// <example>Smith</example>
        public string FamilyName
        {
            get
            {
                return this.familyName;
            }
            set
            {
                string family = value;
                if (family != null)
                {
                    family = family.Trim();
                    if (family.Length > 35) family = family.Substring(0, 35);
                }

                this.familyName = family;
            }
        }


        /// <summary>
        /// Gets the forenames or given names of a person.
        /// </summary>
        /// <value>The forenames or given names of a person.</value>
        /// <example>John</example>
        public PersonPartialNameCollection GivenNames { get; private set; }


        /// <summary>
        /// Gets the standard forms of address used to precede a person's name.
        /// </summary>
        /// <value>The titles.</value>
        /// <example>Mr, Mrs, Miss, Ms, Dr, Brigadier General, and so on</example>
        public PersonPartialNameCollection Titles { get; private set; }

        #endregion Properties




        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonName"/> class.
        /// </summary>
        public PersonName()
        {
            this.Titles = new PersonPartialNameCollection(35);
            this.GivenNames = new PersonPartialNameCollection(35);
            this.familyName = "";
            this.Suffixes = new PersonPartialNameCollection(35);
            this.initials = "";
        }
        #endregion Constructors

        /// <summary>
        /// Gets the full name as a concatenation of its parts, in the format Mr John Smith MBE
        /// </summary>
        /// <returns>The full name</returns>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// Gets the full name as a concatenation of its parts
        /// </summary>
        /// <param name="familyNameFirst">if set to <c>true</c> returns name in the format Smith, Mr John MBE; if <c>false</c> returns name in the format Mr John Smith MBE</param>
        /// <returns>The full name</returns>
        public string ToString(bool familyNameFirst)
        {
            StringBuilder sb = new StringBuilder();

            if (familyNameFirst)
            {
                sb.Append(this.FamilyName);
                if (sb.Length > 0) sb.Append(",");
            }

            if (sb.Length > 0) sb.Append(" ");
            sb.Append(this.Titles.ToString());
            if (sb.Length > 0) sb.Append(" ");
            sb.Append(this.GivenNames.ToString());
            if (!sb.ToString().EndsWith(" ", StringComparison.Ordinal)) sb.Append(" ");

            if (!familyNameFirst) sb.Append(this.FamilyName);

            if (!sb.ToString().EndsWith(" ", StringComparison.Ordinal)) sb.Append(" ");
            sb.Append(this.Suffixes);
            return sb.ToString().Trim();
        }
    }
}
