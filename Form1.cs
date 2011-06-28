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
        string mtgoxbuy;//for use when mtgox decides to fix itself up ;)
        string mtgoxsell;//same as above.
        
       // 
        WebClient client = new WebClient();
        
        
        public Form1()
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => { return true; };
            InitializeComponent();
          
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           
            content = client.DownloadString(@"https://api.tradehill.com/APIv1/USD/Ticker");
         
          string[] split = content.Split(new char [] {'\"'});
         thsell=(split[5]);
           lblTradeHillSell.Text = thsell;
         
         thbuy = (split[9]);
         lblThBuy.Text = thbuy.ToString();
      double thsellnum =double.Parse(thsell);//get numbers in proper format for calculations.....
         double thbuynum = double.Parse(thbuy);








         content2 = client.DownloadString(@"https://bitmarket.eu/api/ticker");
          string[] split2 = content2.Split(new char [] {'\"'});
         mtgoxbuy = (split2[57]);
        mtgoxsell = (split2[53]);
          lblmtgoxsell.Text = mtgoxsell;
           lblmtgoxbuy.Text = mtgoxbuy;

          //  MessageBox.Show(content2.ToString());
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a simple program used to pull the current price values from tradehill and bitmarket....support for MtGox (hopefully) coming soon.  Right now, there is no real validation so if the program cannot connect to one of the two sites, it may very well crash or cause unexpected results...this should be fixed in the future.  Feel free to donate to the supplied address, and I will continue to try and improve upon this in my spare time....");

        }
    }
}
