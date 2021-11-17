using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace HelloWorldService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        SqlConnectionStringBuilder builder;
        SqlConnection connection;

        public EmployeeController()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = "demoserver17.database.windows.net";
            builder.UserID = "jitesh17";
            builder.Password = "Jitesh@2";
            builder.InitialCatalog = "EmpDatabase";
            connection = new SqlConnection(builder.ConnectionString);
        }

        [HttpGet]
        public string Get()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Employee", connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            if (dt.Rows.Count < 1)
            {
                return "No Data Found";
            }

            return JsonConvert.SerializeObject(dt);
        }

        // GET api/employee/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * from Employee where ID = '" + id + "'", connection);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            if (dt.Rows.Count < 1)
            {
                return "No Data Found";
            }

            return JsonConvert.SerializeObject(dt);
        }

        // POST api/employee
        [HttpPost]
        public string Post([FromBody] string value)
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO Employee(Name) VALUES('" + value + "')", connection);
            connection.Open();
            int result = cmd.ExecuteNonQuery(); // return the number of rows affected
            connection.Close();
            if (result > 0)
            {
                return "Record inserted with the value as: " + value;
            }

            return "Try Again. No Data Inserted";
        }

        // PUT api/employee/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] string value)
        {
            SqlCommand cmd = new SqlCommand("UPDATE Employee SET NAME = '" + value + "' where ID = '" + id + "' ", connection);
            connection.Open();
            int result = cmd.ExecuteNonQuery(); // return the number of rows affected
            connection.Close();
            if (result > 0)
            {
                return "Record updated with the value as: " + value;
            }

            return "Try Again. No Data Updated";
        }
    }
}
