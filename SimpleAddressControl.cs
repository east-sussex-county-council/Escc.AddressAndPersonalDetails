using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace EsccWebTeam.Gdsc
{
    /// <summary>
    /// Displays a BS7666 address using the Royal Mail's "Simple Address" rules and adr microformat
    /// </summary>
    public class SimpleAddressControl : WebControl
    {
        private string separator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAddressControl"/> class.
        /// </summary>
        public SimpleAddressControl() : base(HtmlTextWriterTag.Span) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAddressControl"/> class.
        /// </summary>
        /// <param name="tag">An HTML tag.</param>
        public SimpleAddressControl(string tag) : base(tag) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAddressControl"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        public SimpleAddressControl(HtmlTextWriterTag tag) : base(tag) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAddressControl"/> class.
        /// </summary>
        /// <param name="address">The address.</param>
        public SimpleAddressControl(BS7666Address address) : base(HtmlTextWriterTag.Span) { this.Address = address; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAddressControl"/> class.
        /// </summary>
        /// <param name="tag">An HTML tag.</param>
        /// <param name="address">The address.</param>
        public SimpleAddressControl(string tag, BS7666Address address) : base(tag) { this.Address = address; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleAddressControl"/> class.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="address">The address.</param>
        public SimpleAddressControl(HtmlTextWriterTag tag, BS7666Address address) : base(tag) { this.Address = address; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public BS7666Address Address { get; set; }

        /// <summary>
        /// Gets or sets the separator to place between address components
        /// </summary>
        /// <value>The separator.</value>
        public string Separator
        {
            get
            {
                if (separator != null) return separator;
                return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator + " ";
            }
            set
            {
                this.separator = value;
            }
        }

        /// <summary>
        /// Gets an XHTML line break which can be used as a separator.
        /// </summary>
        /// <value>The line break.</value>
        public static string SeparatorLineBreak { get { return "<br />"; } }

        /// <summary>
        /// Gets a hidden comma and an XHTML line break to use as a separator which displays new lines on a web page but commas in an iCalendar download.
        /// </summary>
        /// <value>The separator line break.</value>
        public static string SeparatorHCalendar { get { return "<span class=\"aural\">, </span><br />"; } }

        /// <summary>
        /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
        /// </summary>
        protected override void CreateChildControls()
        {
            // Ensure there is an address
            if (this.Address == null) return;

            // Ensure we use the "adr" class for the "adr" microformat
            this.CssClass = (this.CssClass += " adr").TrimStart();

            // Create a copy of the address so we can correct the case without affecting the external properties
            BS7666Address addrCopy = new BS7666Address(String.IsNullOrEmpty(this.Address.Paon) ? String.Empty : this.Address.Paon.Trim(),
                String.IsNullOrEmpty(this.Address.Saon) ? String.Empty : this.Address.Saon.Trim(),
                String.IsNullOrEmpty(this.Address.StreetName) ? String.Empty : this.Address.StreetName.Trim(),
                String.IsNullOrEmpty(this.Address.Locality) ? String.Empty : this.Address.Locality.Trim(),
                String.IsNullOrEmpty(this.Address.Town) ? String.Empty : this.Address.Town.Trim(),
                String.IsNullOrEmpty(this.Address.AdministrativeArea) ? String.Empty : this.Address.AdministrativeArea.Trim(),
                String.IsNullOrEmpty(this.Address.Postcode) ? String.Empty : this.Address.Postcode.Trim());

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

            // Add space to postcode if missing
            if (addrCopy.Postcode != null && addrCopy.Postcode.IndexOf(" ", StringComparison.Ordinal) == -1)
            {
                if (addrCopy.Postcode.Length == 6)
                {
                    addrCopy.Postcode = addrCopy.Postcode.Substring(0, 3) + " " + addrCopy.Postcode.Substring(3, 3);
                }
                else if (addrCopy.Postcode.Length == 7)
                {
                    addrCopy.Postcode = addrCopy.Postcode.Substring(0, 4) + " " + addrCopy.Postcode.Substring(4, 3);
                }
            }


            // Create a presentational address based on PAF rules for a Simple Address

            // 1st line of simple address contains the SAON, if there is one
            bool hasSaon = (addrCopy.Saon != null && addrCopy.Saon.Trim().Length > 0);
            if (hasSaon)
            {
                using (HtmlGenericControl saon = new HtmlGenericControl("span"))
                {
                    saon.Attributes["class"] = "extended-address"; // adr
                    saon.InnerHtml = HighlightFormat1Name(addrCopy.Saon);
                    this.Controls.Add(saon);
                    this.Controls.Add(new LiteralControl(Separator));
                }
            }

            if (!hasSaon && !SimpleAddress.IsFormat1Name(addrCopy.Paon))
            {
                // If PAON is more than just a number and line one still spare, we have enough lines
                // to put PAON and street address on their own separate lines (1st line and 2nd line)
                if (addrCopy.Paon.Trim().Length > 0)
                {
                    using (HtmlGenericControl paon = new HtmlGenericControl("span"))
                    {
                        paon.Attributes["class"] = "street-address"; // adr
                        paon.InnerHtml = HighlightFormat1Name(addrCopy.Paon);
                        this.Controls.Add(paon);
                        this.Controls.Add(new LiteralControl(Separator));
                    }
                }

                if (addrCopy.StreetName.Trim().Length > 0)
                {
                    using (HtmlGenericControl street = new HtmlGenericControl("span"))
                    {
                        street.Attributes["class"] = "street-address"; // adr
                        street.InnerHtml = HighlightFormat1Name(addrCopy.StreetName);
                        this.Controls.Add(street);
                        this.Controls.Add(new LiteralControl(Separator));
                    }
                }
            }
            else
            {
                // If line 1 used by SAON, or PAON is just a number, cram PAON and StreetName onto one line
                using (HtmlGenericControl street = new HtmlGenericControl("span"))
                {
                    street.Attributes["class"] = "street-address"; // adr

                    bool hasPaon = (addrCopy.Paon != null && addrCopy.Paon.Length > 0);
                    if (hasPaon && addrCopy.StreetName != null && addrCopy.StreetName.Length > 0)
                    {
                        if (Regex.IsMatch(this.Address.Paon, "^[0-9]{1,2}[-/]?[0-9]{0,2}[A-Z]?$", RegexOptions.IgnoreCase))
                        {
                            street.InnerHtml = String.Format(CultureInfo.CurrentCulture, "{0} {1}", HighlightFormat1Name(addrCopy.Paon), HighlightFormat1Name(addrCopy.StreetName));
                        }
                        else
                        {
                            street.InnerHtml = String.Format(CultureInfo.CurrentCulture, "{0}, {1}", HighlightFormat1Name(addrCopy.Paon), HighlightFormat1Name(addrCopy.StreetName));
                        }

                    }
                    if (hasPaon && (addrCopy.StreetName == null || addrCopy.StreetName.Length == 0))
                    {
                        street.InnerHtml = HighlightFormat1Name(addrCopy.Paon);
                    }
                    else if ((addrCopy.Paon == null || addrCopy.Paon.Length == 0) && (addrCopy.StreetName != null && addrCopy.StreetName.Length > 0))
                    {
                        street.InnerHtml = HighlightFormat1Name(addrCopy.StreetName);
                    }

                    if (street.InnerText.Length > 0)
                    {
                        this.Controls.Add(street);
                        this.Controls.Add(new LiteralControl(Separator));
                    }
                }
            }

            // 3rd line of simple address contains the locality
            if (addrCopy.Locality != null && addrCopy.Locality.Trim().Length > 0)
            {
                using (HtmlGenericControl locality = new HtmlGenericControl("span"))
                {
                    locality.Attributes["class"] = "street-address"; // adr
                    locality.InnerText = addrCopy.Locality;
                    this.Controls.Add(locality);
                    this.Controls.Add(new LiteralControl(Separator));
                }
            }

            // 4th line of simple address contains the town
            if (addrCopy.Town != null && addrCopy.Town.Trim().Length > 0)
            {
                using (HtmlGenericControl town = new HtmlGenericControl("span"))
                {
                    town.Attributes["class"] = "locality"; // adr
                    town.InnerText = addrCopy.Town;
                    this.Controls.Add(town);
                    this.Controls.Add(new LiteralControl(Separator));
                }
            }

            // 5th line of simple address contains administrative area and postcode
            bool hasAdministrativeArea = (addrCopy.AdministrativeArea != null && addrCopy.AdministrativeArea.Trim().Length > 0);
            bool hasPostcode = (addrCopy.Postcode != null && addrCopy.Postcode.Trim().Length > 0);
            if (hasAdministrativeArea)
            {
                using (HtmlGenericControl administrativeArea = new HtmlGenericControl("span"))
                {
                    administrativeArea.Attributes["class"] = "region"; // adr
                    administrativeArea.InnerText = addrCopy.AdministrativeArea;
                    this.Controls.Add(administrativeArea);
                }
            }
            if (hasAdministrativeArea && hasPostcode)
            {
                this.Controls.Add(new LiteralControl(" "));
            }
            if (hasPostcode)
            {
                using (HtmlGenericControl postcode = new HtmlGenericControl("span"))
                {
                    postcode.Attributes["class"] = "postal-code"; // adr
                    postcode.InnerText = addrCopy.Postcode;
                    this.Controls.Add(postcode);
                }
            }

            // Ensure we don't end with a separator
            if (this.Controls.Count > 1)
            {
                while (this.Controls[this.Controls.Count - 1].GetType() == typeof(LiteralControl)) this.Controls.Remove(this.Controls[this.Controls.Count - 1]);
            }

            // Ensure the control isn't visible if it has no content
            if (this.Controls.Count == 0) this.Visible = false;
        }

        /// <summary>
        /// Highlights the PAF "Format 1 name" (ie: building number) at the start of an address component, if there is one.
        /// </summary>
        /// <param name="xhtml">The XHTML which may contain the format 1 name.</param>
        /// <returns></returns>
        private static string HighlightFormat1Name(string xhtml)
        {
            return Regex.Replace(HttpUtility.HtmlEncode(xhtml), "^([0-9]+[A-Z]?) ", "<span class=\"format1\">$1</span> ", RegexOptions.IgnoreCase);
        }
    }
}
