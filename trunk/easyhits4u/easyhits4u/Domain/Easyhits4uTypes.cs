
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace easyhits4u.Domain
{
	#region Easyhits4uType

	/// <summary>
	/// Easyhits4uType object for NHibernate mapped table 'Easyhits4uTypes'.
	/// </summary>
	public class Easyhits4uType
		{
		#region Member Variables
		
		protected int _id;
		protected string _easyhits4uTypeName;
		protected IList _easyhits4us;
		
		#endregion

		#region Constructors

		public Easyhits4uType() { }

		public Easyhits4uType( string easyhits4uTypeName )
		{
			this._easyhits4uTypeName = easyhits4uTypeName;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string Easyhits4uTypeName
		{
			get { return _easyhits4uTypeName; }
			set
			{
				if ( value != null && value.Length > 150)
					throw new ArgumentOutOfRangeException("Invalid value for Easyhits4uTypeName", value, value.ToString());
				_easyhits4uTypeName = value;
			}
		}

		public virtual IList Easyhits4us
		{
			get
			{
				if (_easyhits4us==null)
				{
					_easyhits4us = new ArrayList();
				}
				return _easyhits4us;
			}
			set { _easyhits4us = value; }
		}

        
		#endregion		       
	}

	#endregion
}
