using Cassandra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Controllers
{
    public class Logging
    {
        public int GetMaxId()
        {
            Cluster cluster2 = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session2 = cluster2.Connect("hotel");
            int maxId = 0;
            string query2 = "select MAX(LogId) from hotel.\"Logging\"";
            RowSet dataReader2 = session2.Execute(query2);
            foreach (Row row in dataReader2)
            {
                maxId = Convert.ToInt32(row[0].ToString());
            }
            return maxId;
        }
        public void LogGet(int maxId)
        {           
            maxId++;
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("hotel");
            string query = "Insert into hotel.\"Logging\"(logid,description,exception,requesttype,\"Time\") values(" + maxId + ",'Request to fetch All hotels','none','GET','"+DateTime.Now.ToString()+"')";
            session.Execute(query);
        }
        public void LogGetDone(int maxId)
        {
            maxId++;
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("hotel");
            string query = "Insert into hotel.\"Logging\"(logid,description,exception,requesttype,\"Time\") values(" + maxId + ",'All hotels fetched successfully','none','GET','" + DateTime.Now.ToString() + "')";
            session.Execute(query);
        }
        public void LogGetById(int maxId)
        {
            maxId++;
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("hotel");
            string query = "Insert into hotel.\"Logging\"(logid,description,exception,requesttype,\"Time\") values(" + maxId + 1 + ",'Request to fetch All rooms of selected hotel','none','GET','" + DateTime.Now.ToString() + "')";
            session.Execute(query);
        }
        public void LogGetByIdDone(int maxId)
        {
            maxId++;
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("hotel");
            string query = "Insert into hotel.\"Logging\"(logid,description,exception,requesttype,\"Time\") values(" + maxId + 1 + ",'All rooms of selected hotel fetched successfully','none','GET','" + DateTime.Now.ToString() + "')";
            session.Execute(query);
        }
        public void LogPost(int maxId)
        {
            maxId++;
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("hotel");
            string query = "Insert into hotel.\"Logging\"(logid,description,exception,requesttype,\"Time\") values(" + maxId + 1 + ",'Saving Booking details in booking Database','none','POST','" + DateTime.Now.ToString() + "')";
            session.Execute(query);
        }
        public void LogPostDone(int maxId)
        {
            
            maxId++;
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("hotel");
            string query = "Insert into hotel.\"Logging\"(logid,description,exception,requesttype,\"Time\") values(" + maxId + 1 + ",'Booking details successfully saved in database','none','POST','" + DateTime.Now.ToString() + "')";
            session.Execute(query);
        }
    }
}