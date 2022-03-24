using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sac1_Task_2
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        string filepath;       //Create a null string called filepath
        private void Form1_Load(object sender, EventArgs e)
        {
            dgvTable.ColumnCount = 7;                   //Datagridview has 7 columns, name, subject, seller, purchaser, purchase price, sale price and profit
            dgvTable.Columns[0].Name = "Name";
            dgvTable.Columns[1].Name = "Subject";
            dgvTable.Columns[2].Name = "Seller";
            dgvTable.Columns[3].Name = "Purchaser";
            dgvTable.Columns[4].Name = "Purchase Price";
            dgvTable.Columns[5].Name = "Sale Price";
            dgvTable.Columns[6].Name = "Profit";
            FileDialog.Filter = "CSV Files|*.csv";    //Only allow user to select CSV Files
        }
        private float calculateProfit(string purchasePrice, string SalePrice)
        {
            /*
            Name: Calculate Profit
            Description: Takes the purchase and sale prices as strings, returns the profit if it was sold, 
            otherwise counts the purchase price as a loss
            Inputs: Purchase Price (string), Sale Price (string)
            Outputs: Profit (float)
            */
            if (SalePrice == "NA")
            {
                return 0f - float.Parse(purchasePrice);         //If item has not been sold, count purchase price as a loss
            }
            else
            {
                return float.Parse(SalePrice)-float.Parse(purchasePrice);
            }

        }
        private void btnCalculate_Click(object sender, EventArgs e)       //When the 'Calculate' button is clicked
        {
            while (filepath==null || filepath.Contains("csv")==false)     //If filepath is undefined, prompt user to enter filepath until they do
            {
                FileDialog.ShowDialog();
                filepath = FileDialog.FileName;
            }

            float fltProfitTotal = 0f;      //Create variable for total profit
            List<String> Lines = File.ReadAllLines(filepath).ToList();
            
            foreach (String Line in Lines)                   //For each line in the CSV file, split it, calculate the profit, and add to the datagridview
            {
                List<string> LineAsList = Line.Split(',').ToList();
                float fltTextbookProfit = calculateProfit(LineAsList[4], LineAsList[5]);   //Create a variable storing the profit for the current textbook

                fltTextbookProfit = (float)Math.Round((double)fltTextbookProfit,2); //Round Textbook profit to the nearest 2 decimal places (to account for floating point imprecision)

                dgvTable.Rows.Add(LineAsList[0], LineAsList[1], LineAsList[2], LineAsList[3], LineAsList[4], LineAsList[5], fltTextbookProfit);
                                                                                                   //^^Add the CSV data as well as the calculated profit to the data grid view
                fltProfitTotal+=fltTextbookProfit;                   //Add the current profit to the total profit
            }

            lblProfit.Text = "Total Profit: $"+fltProfitTotal.ToString();   //Change the profit label to the correct value

        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            FileDialog.ShowDialog();          //When choose file button is clicked, prompt user to choose a file
            filepath = FileDialog.FileName;
        }
    }
}
