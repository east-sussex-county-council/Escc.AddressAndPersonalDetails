namespace Escc.AddressAndPersonalDetails
{
    /// <summary>
    /// Event arguments which allow the passing of an E-GIF/BS7666-compliant address
    /// </summary>
    public class AddressEventArgs : System.EventArgs
    {
        /// <summary>
        /// Gets or sets whether the address has been changed
        /// </summary>
        public bool HasChanged { get; set; }

        public SimpleAddress SimpleAddress { get; set; }

        /// <summary>
        /// Gets or sets the unique ID for every postal address
        /// </summary>
        public string OA { get; set; }

        public BS7666Address BS7666Address { get; set; }

        /// <summary>
        /// Event arguments which allow the passing of an E-GIF/BS7666-compliant address
        /// </summary>
        public AddressEventArgs()
        {
            this.SimpleAddress = new SimpleAddress();
            this.BS7666Address = new BS7666Address();
        }
    }
}
