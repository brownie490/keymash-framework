using System.Data.SqlClient;


namespace Automation
{
	class Database
	{

		public static string ConnectionString = "Data Source=" + Config.current.database.source + ";Database=" + Config.current.database.dbName + ";Integrated Security=" + Config.current.database.integratedSecurity + ";User ID=" + Config.current.database.userID + ";Password=" + Config.current.database.password + ";";


		public static string GetValue(string Column, string Value)
		{

			string SQL			= "select top 1 " + Column + " from Table where Value = '" + Value + "'";
			string ReturnValue	= null;

			// Create connection
			using (SqlConnection conn = new SqlConnection(ConnectionString))
			{

				// Set up the Query
				SqlCommand cmd = new SqlCommand(SQL, conn);

				// Open the connection
				conn.Open();

				// Declare the SqlDataReader
				SqlDataReader reader;

				// Using the Data Reader
				using (reader = cmd.ExecuteReader())
				{

					// Attempt to get the value
					try
					{

						// If the database was found
						if (reader.Read())
						{

							ReturnValue = (string)reader["Value"];

						}

					}

					finally
					{

						// Close the connection
						conn.Close();

					}

				}

			}

			return ReturnValue;

		}


		public static void UpdateValue(string Value)
		{

			string SQL						= "update table set Column1 = 'updated' where Value = '" + Value + "'";
			using SqlConnection connection	= new SqlConnection(ConnectionString);
			using SqlCommand command		= connection.CreateCommand();


			// Attempt to update the database
			try
			{

				command.CommandText = SQL;
				connection.Open();
				command.ExecuteNonQuery();

			}

			finally
			{

				// Close the connection
				connection.Close();

			}

		}

		public static void InsertData()
		{

			// Insert the new Account
			string SQL						= "Insert into Table (Id, FirstName, LastName Values(Newid(), FirstName, LastName);";
			using SqlConnection connection	= new(ConnectionString);
			using SqlCommand command		= connection.CreateCommand();


			//Attempt to update the db 
			try
			{
				
				command.CommandText = SQL;
				connection.Open();
				command.ExecuteNonQuery();

			}

			finally
			{

				// Close the connection
				connection.Close();

			}

		}


		public static void DeleteData()
		{

			string SQL						= "delete from Table where FirstName = 'FirstName'";
			using SqlConnection connection	= new SqlConnection(ConnectionString);
			using SqlCommand command		= connection.CreateCommand();


			// Attempt to delete the data
			try
			{

				command.CommandText = SQL;
				connection.Open();
				command.ExecuteNonQuery();

			}

			finally
			{

				// Close the connection
				connection.Close();

			}

		}

	}

}