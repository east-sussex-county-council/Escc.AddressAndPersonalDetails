using System;

namespace EsccWebTeam.Gdsc
{
	/// <summary>
	/// Event arguments which allow the passing of an E-GIF/BS7666-compliant address
	/// </summary>
	public class AddressEventArgs : System.EventArgs
	{

		private SimpleAddress simpleAddress;
		private bool hasChanged;
		
		/// <summary>
		/// Gets or sets whether the address has been changed
		/// </summary>
		public bool HasChanged
		{
			get
			{
				return this.hasChanged;
			}
			set
			{
				this.hasChanged = value;
			}
		}

		/// <summary>
		/// Property SimpleAddress (SimpleAddress)
		/// </summary>
		public SimpleAddress SimpleAddress
		{
			get
			{
				return this.simpleAddress;
			}
			set
			{
				this.simpleAddress = value;
			}
		}

		private string oA;
		
		/// <summary>
		/// Gets or sets the unique ID for every postal address
		/// </summary>
		public string OA
		{
			get
			{
				return this.oA;
			}
			set
			{
				this.oA = value;
			}
		}

		private BS7666Address bs7666Address;
		
		/// <summary>
		/// Property BS7666Address (BS7666Address)
		/// </summary>
		public BS7666Address BS7666Address
		{
			get
			{
				return this.bs7666Address;
			}
			set
			{
				this.bs7666Address = value;
			}
		}
		
		/// <summary>
		/// Event arguments which allow the passing of an E-GIF/BS7666-compliant address
		/// </summary>
		public AddressEventArgs()
		{
			this.simpleAddress = new SimpleAddress();
			this.bs7666Address = new BS7666Address();			
		}
	}
}
