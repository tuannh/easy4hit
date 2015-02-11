using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using easyhits4u.Domain;
using NHibernate;
using easyhits4u.Helper;

namespace easyhits4u.Code
{
    public class HelperData
    {
        public static List<Easyhits4u> GetAllEasyhits4u()
        {
            ISession session = SessionFactory.GetCurrentSession();
            List<Easyhits4u> lst = session.CreateQuery("from Easyhits4u u")
                                          .SetCacheable(true)
                                          .List<Easyhits4u>() as List<Easyhits4u>;

            return lst;
        }

        public static List<Easyhits4u> GeEasyhits4uByLength(int length)
        {
            ISession session = SessionFactory.GetCurrentSession();
            List<Easyhits4u> lst = session.CreateQuery("from Easyhits4u u where u.Length = :length and u.IsApproved = true")
                                          .SetInt32("length", length)
                                          .SetCacheable(true)
                                          .List<Easyhits4u>() as List<Easyhits4u>;

            return lst;
        }

        public static List<Easyhits4u> GetAllCheckData()
        {
            ISession session = SessionFactory.GetCurrentSession();
            List<Easyhits4u> lst = session.CreateQuery("from Easyhits4u u where u.IsApproved = true")
                                          .SetCacheable(true)
                                          .List<Easyhits4u>() as List<Easyhits4u>;

            return lst;
        }
    }
}