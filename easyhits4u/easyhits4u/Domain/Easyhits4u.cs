
using System;
using System.Collections;
using System.Web.UI.WebControls;

namespace easyhits4u.Domain
{
	#region Easyhits4u

	/// <summary>
	/// Easyhits4u object for NHibernate mapped table 'Easyhits4u'.
	/// </summary>
	public class Easyhits4u
		{
		#region Member Variables
		
		protected int _id;
		protected string _name;
		protected byte[] _image;
		protected int _length;
		protected bool _isApproved;
		protected Easyhits4uType _easyhits4uType;
		
		#endregion

		#region Constructors

		public Easyhits4u() { }

		public Easyhits4u( string name, byte[] image, int length, bool isApproved, Easyhits4uType easyhits4uType )
		{
			this._name = name;
			this._image = image;
			this._length = length;
			this._isApproved = isApproved;
			this._easyhits4uType = easyhits4uType;
		}

		#endregion

		#region Public Properties

		public virtual int Id
		{
			get {return _id;}
			set {_id = value;}
		}

		public virtual string Name
		{
			get { return _name; }
			set
			{
				if ( value != null && value.Length > 50)
					throw new ArgumentOutOfRangeException("Invalid value for Name", value, value.ToString());
				_name = value;
			}
		}

		public virtual byte[] Image
		{
			get { return _image; }
			set { _image = value; }
		}

		public virtual int Length
		{
			get { return _length; }
			set { _length = value; }
		}

		public virtual bool IsApproved
		{
			get { return _isApproved; }
			set { _isApproved = value; }
		}

        public virtual int Count
        {
            get;
            set;
        }

        public virtual string Note
        {
            get;
            set;
        }

		public virtual Easyhits4uType Easyhits4uType
		{
			get { return _easyhits4uType; }
			set { _easyhits4uType = value; }
		}

        
		#endregion		       
	}

	#endregion
}
