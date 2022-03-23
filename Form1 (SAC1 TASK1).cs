using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SAC1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        float fltCollectionPrice = 0f;    //Create a variable to store the price of the collection
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private float CalculateWorth(float fltInitialPrice, float fltAge)    //Function to return value of textbook after a certain amount of years
        {
            /*
            Function Name: Calculate worth
            Purpose: To calculate the value of a textbook after a certain amount of years with a certain initial price
            Inputs: Initial price (float), Age of the textbook (float)
            Outputs: Current value of the textbook (float)
            */
            if (fltAge < 5)
            {
                return (fltInitialPrice) * (1.0f - 0.2f * fltAge);
            }
            else
            {
                return 0;
            }
            
        }
        private void btnCalculate_Click(object sender, EventArgs e)    //When calculate button is pressed, change label text to new value of textbook
        {
            float NewPrice = CalculateWorth((float)updownPrice.Value, (float)updownAge.Value);
            fltCollectionPrice += NewPrice;    //Add latest textbook to collection price
            lblTextbookWorth.Text = "This textbook is worth $" + NewPrice.ToString();
            lblCollectionWorth.Text = "This collection is worth $" + fltCollectionPrice.ToString(); //Update label
        }

        private void btnEnd_Click(object sender, EventArgs e)    //When end button is clicked, clear numeric updowns and labels
        {
            fltCollectionPrice = 0f;              //Also reset collection price to zero
            lblTextbookWorth.Text = "This textbook is worth $0";
            lblCollectionWorth.Text = "This collection is worth $0";
            updownAge.Value = 0;
            updownPrice.Value = 0;

        }
    }
}
