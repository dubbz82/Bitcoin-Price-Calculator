using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {

        //some of these will be later casted on the fly to actual numbers for comparison....that will probably be implemented later on....
        string content;
        string content2;
        string mtgoxsell;
        string mtgoxbuy;
        string bmbuy;
        string bmsell;
        string thbuy;
        string thsell;
        
      
        WebClient client = new WebClient();
        
        
        public Form1()
        {
          
            InitializeComponent();
          
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            PullData();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a simple program used to pull the current price values from MtGox (finally), tradehill, and bitmarket.  Autorefreshing coming soon.  Feel free to donate to the supplied address, and I will continue to try and improve upon this in my spare time....");

        }





        public void PullData()
        {
            try
            {
                //this took a good amount of googling to figure out how to get it to work correctly....thank you mtgox for keeping to standard API's that just work like the rest of the world :/
                // used to build entire input
                StringBuilder sb = new StringBuilder();

                // used on each read operation
                byte[] buf = new byte[8192];

                // prepare the web page we will be asking for
                HttpWebRequest request = (HttpWebRequest)
                    WebRequest.Create("https://mtgox.com/code/data/ticker.php");
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "POST";
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)";

                // execute the request
                HttpWebResponse response = (HttpWebResponse)
                    request.GetResponse();

                // read data via the response stream
                System.IO.Stream resStream = response.GetResponseStream();

                string tempString = null;
                int count = 0;

                do
                {
                    // fill the buffer with data
                    count = resStream.Read(buf, 0, buf.Length);

                    // make sure we read some data
                    if (count != 0)
                    {
                        // translate from bytes to ASCII text
                        tempString = Encoding.ASCII.GetString(buf, 0, count);

                        // continue building the string
                        sb.Append(tempString);
                    }
                }
                while (count > 0); // any more data to read?

                

                string[] split = sb.ToString().Split(new char[] { ':' });  //pull buy and sell data from imported data
                mtgoxbuy = split[7];
                mtgoxsell = split[8];
                split = mtgoxbuy.Split(new char[] { ',' });
                mtgoxbuy = split[0];
                split = mtgoxsell.Split(new char[] { '}' });
                mtgoxsell = split[0];
              
               
            }
            catch //in case api is inaccessable..
            {
                MessageBox.Show("Mtgox data could not be pulled.....setting values to zero instead....");
                mtgoxbuy = "0";
                mtgoxsell = "0";
            }
            lblMtGoxSell.Text = mtgoxsell;
            lblMtGoxBuy.Text = mtgoxbuy;

            try
            {


                content2 = client.DownloadString(@"https://bitmarket.eu/api/ticker");


                string[] split2 = content2.Split(new char[] { '\"' });
                bmbuy = (split2[57]);
                bmsell = (split2[53]);
            }
            catch //in case api is inaccessable
            {
                MessageBox.Show("bitmarket data could not be pulled.....setting values to zero instead....");
                bmbuy = "0";
                bmsell = "0";
            }
            lblbmsell.Text = bmsell;
            lblbmbuy.Text = bmbuy;

            try
            {
                content = client.DownloadString(@"https://api.tradehill.com/APIv1/USD/Ticker");
                string[] split3 = content.Split(new char[] { '"' });
                thbuy = split3[9];
                thsell = split3[5];
            }

            catch //in case api is inaccessable
            { 
                MessageBox.Show("tradehill data could not be pulled......setting values to zero instead....");
                thbuy = "0";  
                thsell = "0"; 
               
            }
            lblthbuy.Text = thbuy;
            lblthsell.Text = thsell;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            PullData();
        }
    }
}
