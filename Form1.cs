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

        string content;
        string content2;
        string thsell;
        string thbuy;
        string bmbuy;//for use when mtgox decides to fix itself up ;)
        string bmsell;//same as above.
        
       // 
        WebClient client = new WebClient();
        
        
        public Form1()
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
            InitializeComponent();
          
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                content = client.DownloadString(@"https://api.tradehill.com/APIv1/USD/Ticker");

                string[] split = content.Split(new char[] { '\"' });
                thsell = (split[5]);
                thbuy = (split[9]);
            }
            catch //in case api is inaccessable..
            {
                MessageBox.Show("Tradehill data could not be pulled.....setting values to zero instead....");
                thbuy = "0";
                thsell = "0";
            }
         lblTradeHillSell.Text = thsell;
         lblThBuy.Text = thbuy.ToString();
     // double thsellnum =double.Parse(thsell);for later use....
    //double thbuynum = double.Parse(thbuy); also for later use...
         try
         {

             content2 = client.DownloadString(@"https://bitmarket.eu/api/ticker");
             string[] split2 = content2.Split(new char[] { '\"' });
             bmbuy = (split2[57]);
             bmsell = (split2[53]);
         }
         catch
         {
             MessageBox.Show("bitmarket data could not be pulled.....setting values to zero instead....");
             bmbuy = "0";
             bmsell = "0";
         }
          lblbmsell.Text = bmsell;
          lblbmbuy.Text = bmbuy;

          //  MessageBox.Show(content2.ToString());
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a simple program used to pull the current price values from tradehill and bitmarket....support for MtGox (hopefully) coming soon.  Right now, there is no real validation so if the program cannot connect to one of the two sites, it may very well crash or cause unexpected results...this should be fixed in the future.  Feel free to donate to the supplied address, and I will continue to try and improve upon this in my spare time....");

        }
    }
}
