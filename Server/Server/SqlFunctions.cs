using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

/*
These functions are made with use of  MySqlConnector version 6.9.8.0 in Visual Studio
To use thses, you must have MySql server and database already set
Remember to add MySql.Data extension to references in Visual Studio so it will work
There are still bits of code that could be tidyed up
*/

//TODO: Methods to server for calling these methods

namespace sqlFunctions{

public class mySqlFunctions{

    String server = "127.0.0.1";    //THESE ARE TEST VARIABLES TO CONNECT INTO MYSQL DATABASE
    String port = "3306";           //FOR NOW EVERYTHING WORKS IN TEST DATABASE
    String database = "pelaajat";    
    String mysqlpassword = "Testi";
    Int32 rank = 100;
    String deviceId = "Testi4";
    String username = "Peetu";
    String inventory;
    String[] playerInventory;

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
				command.CommandText = "insert into users (id,deviceId,username,rank,inventory,last_online,created) values(NULL,?deviceId,?username, 0, '1:2:3', NULL, NULL)";
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
                    inventory = reader.GetString("inventory");
                    playerInventory = getPlayerInventory();
                    //Console.WriteLine(rank);
                    Console.WriteLine(username);
                    //Console.WriteLine(playerInventory.ToString());
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

        inventory = createInventoryString();
        command.CommandText = "Update users SET inventory=?inventory WHERE deviceId=?deviceId";
        command.Parameters.AddWithValue("?inventory", inventory);
        command.Parameters.AddWithValue("?deviceId", deviceId);

        try{
        	conn.Open();
        	command.ExecuteNonQuery();
        }catch (MySqlException ex){
                Console.WriteLine(ex.Message);
        }
        conn.Close();
	}

	public string[] getPlayerInventory(){
		char[] separatingChars = { ':' };
            string[] playerInventory = inventory.Split(separatingChars, StringSplitOptions.RemoveEmptyEntries);

            //List<string> test = new List<string>(playerInventory);
            //test.Add("9");
            //Console.WriteLine(test);

            //These are for testing
            Console.WriteLine("Entrys in database string");
            foreach (string s in playerInventory)
            {
                Console.WriteLine(s.ToString());
            }

            return playerInventory;
	}

	public string createInventoryString(){
            //string inventory = "4:5:6";
            string inventory = string.Join(":", playerInventory);
            //These prints are for testing
            Console.WriteLine("Transformed string");
            Console.WriteLine(inventory);
            return inventory;
	
	}
}}