using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using CSharpTo.Model;

namespace CSharpTo
{
    class Program
    {
        static void Main(string[] args)
        {
            //string connectStr = "server=127.0.0.1;database=mygamedb;user=root;password=root";
            //MySqlConnection conn = new MySqlConnection(connectStr);
            //conn.Open();

            //conn.Close();

            ISession session = null;
            ITransaction transaction = null;
            try {
                session = NHibernateHelper.OpenSession();

                User user = new User() { Username = "sl", Password = "131400" };

                transaction = session.BeginTransaction();
                session.Save(user);
                transaction.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally {
                if (transaction != null)
                    transaction.Dispose();
                if (session != null)
                    session.Close();
                if (NHibernateHelper.SessionFactory != null)
                    NHibernateHelper.SessionFactory.Close();

            }
                
              


            Console.ReadKey();
        }
    }
}
