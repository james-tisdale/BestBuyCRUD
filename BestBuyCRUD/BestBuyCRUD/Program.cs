using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace BestBuyCRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            var departments = GetAllDepartments(); //a list of departments from the database

            foreach(var department in departments) // for each item in our list of departments, print the department item
            {
                Console.WriteLine(department);
            }
        }
        static List<string> GetAllDepartments()
        {
            MySqlConnection conn = new MySqlConnection();

            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.txt");

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT Name FROM Departments;";

            using (conn)
            {
                conn.Open();
                List<string> allDepartments = new List<string>();

                MySqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read() == true)
                {
                    var currentDepartment = reader.GetString("Name");
                    allDepartments.Add(currentDepartment);
                }

                return allDepartments;
            }
        }

        static void InsertNewDepartment(string newDepartmentName)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.txt");

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "INSERT INTO departments (Name) VALUES (@deptname);";
            cmd.Parameters.AddWithValue("deptname", newDepartmentName);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        static void DeleteDepartment(string departmentName)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.txt");

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM departments WHERE Name = @departmentName;";
            cmd.Parameters.AddWithValue("departmentname", departmentName);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        static void UpdateDepartment(string newName, string oldName)
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = System.IO.File.ReadAllText("connectionString.txt");

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE departments SET Name = @newName WHERE Name = @oldName";
            cmd.Parameters.AddWithValue("newName", newName);
            cmd.Parameters.AddWithValue("oldName", oldName);

            using(conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
