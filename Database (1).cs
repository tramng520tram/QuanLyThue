using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

public static class Database
{
    public static String ConnectionString = "SERVER=CTH-LTANH;User Id=sa; Password=Cntt4k4; DATABASE=HDDTCTH; Encrypt=FALSE;";
    //public static String ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=HDDTCTH;Integrated Security=True";
    //public static String ConnectionString = "Data Source=.\\SQLExpress;Integrated Security=True;User Instance=True;AttachDBFilename=|DataDirectory|NNTH_DB.MDF;";
    //public static String ConnectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=D:\Users\My Documents\Visual Studio 2005\WebSites\HVeStore\App_Data\bobo.mdf;Integrated Security=True;User Instance=True";
    //SqlConnection con = new SqlConnection("Data Source=MyPC;Initial Catalog=NTTestXDMT;Integrated Security=True");

    //public static String ConnectionString = "Data Source=MyPC;Initial Catalog=MTTest;Integrated Security=True";
    //SERVER=Server
    //public static String ConnectionString = "SERVER=cth-ltanh;User Id=sa; Password=Cntt4k4; DATABASE=MTTest; Encrypt=FALSE;";


    public static SqlConnection GetConnection()
	{
        return new SqlConnection(ConnectionString);
	}

    public static DataTable Fill(DataTable table, String sql, params Object[] parameters)
    {
        SqlCommand command = Database.CreateCommand(sql, parameters);
        new SqlDataAdapter(command).Fill(table);
        command.Connection.Close();

        return table;
    }

    public static SqlDataReader GetDataToReader(String sql, params Object[] parameters)
    {
        SqlCommand command = CreateCommand(sql, parameters);
        command.Connection.Open(); ;
        SqlDataReader dr = command.ExecuteReader();
        //command.Connection.Close();
        return dr;
    }    

    public static DataTable GetData(String sql, params Object[] parameters)
    {
        return Database.Fill(new DataTable(), sql, parameters);
    }

    public static int ExecuteNonQuery(String sql, params Object[] parameters)
    {
        SqlCommand command = Database.CreateCommand(sql, parameters);
        
        command.Connection.Open();
        int rows = command.ExecuteNonQuery();
        command.Connection.Close();
        
        return rows;
    }

    public static object ExecuteScalar(String sql, params Object[] parameters)
    {
        SqlCommand command = Database.CreateCommand(sql, parameters);
        
        command.Connection.Open();
        object value = command.ExecuteScalar();
        command.Connection.Close();

        return value;
    }

    private static SqlCommand CreateCommand(String sql, params Object[] parameters)
    {
        SqlCommand command = new SqlCommand(sql, Database.GetConnection());
        for (int i = 0; i < parameters.Length; i+=2)
        {
            command.Parameters.AddWithValue(parameters[i].ToString(), parameters[i + 1]);
        }
        return command;
    }

    public static string GetMd5Hash(string input)
    {
        MD5 md5Hash = MD5.Create();
        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        // Create a new Stringbuilder to collect the bytes
        // and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data 
        // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }

        // Return the hexadecimal string.
        return sBuilder.ToString();
    }    
}