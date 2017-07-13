using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

/*
These functions are made by using MySqlConnector version 6.8.9 in Visual Studio
To use thses, you must have MySql server and database already set
Remember to add MySql.Data to references in Visual Studio so it will work
There are still bits of code that could be tidyed up
*/

namespace sqlFunctions{

public class mySqlFunctions{

    String server = "127.0.0.1";    //THESE ARE TEST VARIABLES TO CONNECT INTO MYSQL DATABASE
    String port = "3306";           //FOR NOW EVERYTHING WORKS IN TEST DATABASE
    String database = "players";    
    String mysqlpassword = "Testi";
    Int32 rank = 100;
    String deviceId = "Testi4";
    String username = "Peetu";
    String inventory = "1:2:3";

        public void newUser(){

		string myConnectionString;
        myConnectionString = "Server=" + server + ";Port=" + port + ";Database=" + database + ";Uid=root;Password=" + mysqlpassword + ";";	
		MySqlConnection conn = new MySqlConnection(myConnectionString);
		MySqlCommand command = conn.CreateCommand();

		//command.CommandText = "insert into users (id,deviceId,username,rank,created,last_online) values(NULL, '" + deviceId +"', '" + username + "', NULL, NULL, NULL)";
		command.CommandText = "SELECT * FROM users WHERE deviceId=?deviceId";
		command.Parameters.AddWithValue("?deviceId", deviceId);

		try{
			conn.Open();
			//command.ExecuteNonQuery();
            //Checking if entry already in database
			object exists = command.ExecuteScalar(); //Returns null if not in database

			if(exists == null){
				command.CommandText = "insert into users (id,deviceId,username,rank,created,last_online) values(NULL,?deviceId,?username, NULL, NULL, NULL)";
				//command.Parameters.AddWithValue("?deviceId", deviceId);
				command.Parameters.AddWithValue("?username", username);
				command.ExecuteNonQuery();
			}else{
				Console.WriteLine("User with that deviceId already exists.");
			}
			
		}
		catch (MySqlException ex){
            Console.WriteLine(ex.Message);
        }
        conn.Close();
        }

    public void getUser(){

    	string myConnectionString;
        myConnectionString = "Server=" + server + ";Port=" + port + ";Database=" + database + ";Uid=root;Password=" + mysqlpassword + ";";	
		MySqlConnection conn = new MySqlConnection(myConnectionString);
		MySqlCommand command = conn.CreateCommand();

        command.CommandText = "SELECT * FROM users WHERE ?deviceId=deviceId";
		command.Parameters.AddWithValue("?deviceId", deviceId);


		try{
			conn.Open();
		
		}catch (MySqlException ex){
            Console.WriteLine(ex.Message);
        }
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
        {
                if (reader.HasRows)
                {
                    rank = reader.GetInt32("rank");
                    username = reader.GetString("username");
                    //inventory = reader.GetString("inventory");
                    //getPlayerInventory();
                    Console.WriteLine(rank);
                    Console.WriteLine(username);
                    Console.WriteLine(inventory.ToString());
                }
                else { Console.WriteLine("No such thing"); }
        }
        conn.Close();
		
	}

	public void updateRank(){
		string myConnectionString;
        myConnectionString = "Server=" + server + ";Port=" + port + ";Database=" + database + ";Uid=root;Password=" + mysqlpassword + ";";	
		MySqlConnection conn = new MySqlConnection(myConnectionString);
		MySqlCommand command = conn.CreateCommand();

		command.CommandText = "Update users SET rank=?rank WHERE deviceId=?deviceId";
		command.Parameters.AddWithValue("?rank", rank);
		command.Parameters.AddWithValue("?deviceId", deviceId);

		try{
			conn.Open();
			command.ExecuteNonQuery();
		}

		catch (MySqlException ex){
            Console.WriteLine(ex.Message);
        }
        conn.Close();

	}

	public void updateLastlogin(){
		string myConnectionString;
        myConnectionString = "Server=" + server + ";Port=" + port + ";Database=" + database + ";Uid=root;Password=" + mysqlpassword + ";";	
		MySqlConnection conn = new MySqlConnection(myConnectionString);
		MySqlCommand command = conn.CreateCommand();

		command.CommandText = "Update users SET last_online=CURRENT_TIMESTAMP WHERE deviceId=?deviceId";
		command.Parameters.AddWithValue("?deviceId", deviceId);

		try{
			conn.Open();
			command.ExecuteNonQuery();
		}

		catch (MySqlException ex){
            Console.WriteLine(ex.Message);
        }
        conn.Close();
	}

    public void updateUsername()
        {
            string myConnectionString;
            myConnectionString = "Server=" + server + ";Port=" + port + ";Database=" + database + ";Uid=root;Password=" + mysqlpassword + ";";
            MySqlConnection conn = new MySqlConnection(myConnectionString);
            MySqlCommand command = conn.CreateCommand();

            command.CommandText = "Update users SET username=?username WHERE deviceId=?deviceId";
            command.Parameters.AddWithValue("?username", username);
            command.Parameters.AddWithValue("?deviceId", deviceId);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
            }

            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
        }

    public void updateInventory(){
    	string myConnectionString;
        myConnectionString = "Server=" + server + ";Port=" + port + ";Database=" + database + ";Uid=root;Password=" + mysqlpassword + ";";
        MySqlConnection conn = new MySqlConnection(myConnectionString);
        MySqlCommand command = conn.CreateCommand();

        createInventoryString();
        command.CommandText = "Update users SET inventory=?inventory WHERE deviceId=?deviceId";
        command.Parameters.AddWithValue("?inventory", inventory);
        command.Parameters.AddWithValue("?deviceId", deviceId);

        try{
        	conn.Open();
        	command.ExecuteNonQuery();
        }catch (MySqlException ex){
        	Console.WriteLine(ex.Message)
        }
        conn.Close();
	}

	public void getPlayerInventory(){
		char[] separatingChars = { ':' };
		string[] playerInventory = inventory.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);
	}

	public void createInventoryString(){
		string inventory = string.Join(":", playerInventory);
	
	}
}}