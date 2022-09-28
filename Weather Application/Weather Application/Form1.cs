using System;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Net;
using System.Data;

namespace Weather_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        String city;
        private void btnShow1_Click(object sender, EventArgs e)
        {

            city = txtCity.Text;



            string uri = string.Format("http://api.apixu.com/v1/forecast.xml?key=948cf95c6d9f4b26a99123418191804&q={0}&days=1", city);

            XDocument doc = XDocument.Load(uri);

            string iconUri = (string)doc.Descendants("icon").FirstOrDefault();

            WebClient client = new WebClient();

            byte[] image = client.DownloadData("http:" + iconUri);

            MemoryStream stream = new MemoryStream(image);



            Bitmap newBitMap = new Bitmap(stream);
            string maxtemp = (string)doc.Descendants("maxtemp_c").FirstOrDefault();
            string mintemp = (string)doc.Descendants("mintemp_c").FirstOrDefault();

            string maxwindm = (string)doc.Descendants("maxwind_mph").FirstOrDefault();
            string maxwindk = (string)doc.Descendants("maxwind_kph").FirstOrDefault();
            string humidity = (string)doc.Descendants("avghumidity").FirstOrDefault();


            string country = (string)doc.Descendants("country").FirstOrDefault();

            string cloud = (string)doc.Descendants("text").FirstOrDefault();

            Bitmap icon = newBitMap;


            txtMax.Text = maxtemp;
            txtMin.Text = mintemp;

            txtWindMph.Text = maxwindm;
            txtWindKph.Text = maxwindk;

            txtHumidity.Text = humidity;

            label7.Text = cloud;
            txtcountry.Text = country;
            pictureBox1.Image = icon;








        }


        private void getweather(string city)
        {


        }

        private void btnShow2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("country", typeof(string));
            dt.Columns.Add("Date", typeof(string));
            dt.Columns.Add("Max Temp", typeof(string));
            dt.Columns.Add("Min Temp", typeof(string));
            dt.Columns.Add("Maxwindmph", typeof(string));
            dt.Columns.Add("Maxwindkph", typeof(string));
            dt.Columns.Add("Humidity", typeof(string));
            dt.Columns.Add("Cloud", typeof(string));
            dt.Columns.Add("Icon", typeof(Bitmap));

            city = txtCity.Text;



            string uri = string.Format("http://api.apixu.com/v1/forecast.xml?key=948cf95c6d9f4b26a99123418191804&q={0}&days=7", city);

            XDocument doc = XDocument.Load(uri);


            foreach (var npc in doc.Descendants("forecastday"))
            {
                string iconUri = (string)npc.Descendants("icon").FirstOrDefault();
                WebClient client = new WebClient();
                byte[] image = client.DownloadData("http:" + iconUri);
                MemoryStream stream = new MemoryStream(image);


                Bitmap newBitmap = new Bitmap(stream);


                dt.Rows.Add(new object[]
                {
                     (string)doc.Descendants("country").FirstOrDefault(),
                      (string)npc.Descendants("date").FirstOrDefault(),

                   (string)npc.Descendants("maxtemp_c").FirstOrDefault(),
                   (string)npc.Descendants("mintemp_c").FirstOrDefault(),
                    (string)npc.Descendants("maxwind_mph").FirstOrDefault(),
                   (string)npc.Descendants("maxwind_kph").FirstOrDefault(),
                   (string)npc.Descendants("avghumidity").FirstOrDefault(),
                     (string)npc.Descendants("text").FirstOrDefault(),
                     newBitmap


                });
            }
            dataGridView1.DataSource = dt;

        }

       
    }
}