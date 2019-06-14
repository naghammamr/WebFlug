using notification29.Hubs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using WebFlug.Models;

namespace WebFlug.Servicese
{
    public class SendNotifications
    {
          static readonly string connString = @"data source=(localDb)\localdb;initial catalog=NotificationSystem;integrated security=True;MultipleActiveResultSets=True;";

        internal static SqlCommand command = null;
        internal static SqlDependency dependency = null;


        public static string GetNotification()
        {

            var messages = new List<Offers>();
            using (var connection = new SqlConnection(connString))
            {

                connection.Open();

                using (command = new SqlCommand(@"SELECT [ContactID],[ContactName],[ContactNo] FROM [dbo].[Contacts]", connection))
                {
                    command.Notification = null;

                    if (dependency == null)
                    {
                        dependency = new SqlDependency(command);
                        dependency.OnChange += new OnChangeEventHandler(Dependency_OnChange);
                    }

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(item: new Offers
                        {
                            Offer_Id = (int)reader["Offer_ID"],
                            TravellerReward = reader["TravellerReward"] != DBNull.Value ? (string)reader["TravellerReward"] : "",
                        });
                    }
                }

            }
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(messages);
            return json;

        }
            //catch (Exception ex)
            //{

            //    return null;
            //}


        private static void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (dependency != null)
            {
                dependency.OnChange -= Dependency_OnChange;
                dependency = null;
            }
            if (e.Type == SqlNotificationType.Change)
            {
                MyHub.Send();
            }
        }

    }
}
