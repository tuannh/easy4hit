using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using easyhits4u.Domain;

namespace easyhits4u.Helper
{
    public class DataEntry
    {
        public int Index
        {
            get;
            set;
        }

        public Bitmap Data
        { 
            get; 
            set; 
        }

        public Easyhits4u Easyhits4u
        {
            get;
            set;
        }
    }
}