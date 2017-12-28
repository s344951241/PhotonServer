using NHibernate;
using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slServer
{
    class NHibernateHelper
    {
        public static ISessionFactory _sessionFactory = null;
        public static ISessionFactory SessionFactory {
            get {
                if (_sessionFactory == null)
                {
                    Configuration configration = new Configuration();
                    configration.Configure();
                    configration.AddAssembly("slServer");
                    _sessionFactory = configration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
