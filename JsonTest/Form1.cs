using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web;

namespace JsonTest
{
    public partial class Form1 : Form
    {

        public class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            WebClient client = new WebClient();
            string data = "";
            client.Encoding = System.Text.Encoding.UTF8;

            string reply = client.UploadString("http://ip.jsontest.com/?callback=showMyIP", data);

            Result.Text = reply;
            */

            //////

            Employee employee = new Employee
            {
                FirstName = "Jalpesh",
                LastName = "Vadgama"
            };

            var javaScriptSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string jsonString = javaScriptSerializer.Serialize(employee);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://gurujsonrpc.appspot.com/guru");

            
            request.ContentType = "application/json; charset=utf-8";
            request.Accept = "application/json, text/javascript, */*";
            request.Method = "POST";
            request.Headers.Add("Authorization","Bearer a503faf9-45b5-4fec-8334-337284a66ea4");
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
               // writer.Write("{id : 'test Orpak'}");
                writer.Write(@"
                {""StationId"" : 55, 
                 ""RequestId"": ""e1234"" 
""DateTime"": "" 2016-10-25 14:28:09.977"",
""PumpId"": 2,
""Fuel_type_code"": 1,
""NR_ID"" : 3,
""Cpass_UTN"": ""01234567""
""TAGID"" : ""12340000"",
""Odometer"":22,
""Engine_Hours"" :7
}
");
            }

            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();

            string json = "";

            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    json += reader.ReadLine();
                }
            }

            Result.Text = json;

            /////

        }
    }
}
