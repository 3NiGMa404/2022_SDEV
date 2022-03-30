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

namespace SAC_1_TASK_3
{
    public partial class Form1 : Form
    {
        string filepath;    //Create a null string for the file path
        List<Textbook> Textbooks = new List<Textbook>();     //Create an empty list of textbooks called Textbooks
        public Form1()
        {
            InitializeComponent();
            fileDialog.Filter = "CSV Files|*.csv";    //Only allow user to select CSV Files
            dgvTable.ColumnCount = 7;                   //Datagridview has 7 columns, name, subject, seller, purchaser, purchase price, sale price and profit
            dgvTable.Columns[0].Name = "Name";
            dgvTable.Columns[1].Name = "Subject";
            dgvTable.Columns[2].Name = "Seller";
            dgvTable.Columns[3].Name = "Purchaser";
            dgvTable.Columns[4].Name = "Purchase Price ($)";
            dgvTable.Columns[5].Name = "Sale Price ($)";
            dgvTable.Columns[6].Name = "Rating";
        }
        private void Search(DataGridView dgvCurrentDGV, List<Textbook> lstTextbooks, string strSearchTerm, bool boolSubject)
        {
            /*
            Name: Search
            Purpose: To search for textbooks that match either the subject or name in a search term, and update a specified 
            datagridview with the data
            Inputs: dgvCurrentDGV (datagridview), lstTextbooks (list of textbooks), strSearchTerm (string), boolSubject (boolean)
            */
            dgvCurrentDGV.Rows.Clear();
            foreach (Textbook current_textbook in lstTextbooks)
            {
                if (boolSubject)
                {
                    if (current_textbook.Subject.ToLower().Contains(strSearchTerm))
                    {
                        dgvCurrentDGV.Rows.Add(current_textbook.Name, current_textbook.Subject, current_textbook.Seller, current_textbook.Purchaser, current_textbook.PurchasePrice.ToString(), current_textbook.SalePrice.ToString(), current_textbook.Rating.ToString());
                    }
                }
                else
                {
                    if (current_textbook.Name.ToLower().Contains(strSearchTerm))
                    {
                        dgvCurrentDGV.Rows.Add(current_textbook.Name, current_textbook.Subject, current_textbook.Seller, current_textbook.Purchaser, current_textbook.PurchasePrice.ToString(), current_textbook.SalePrice.ToString(), current_textbook.Rating.ToString());
                    }

                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            while (filepath == null)
            {
                fileDialog.ShowDialog();          //Prompt user to choose a file until a file is chosen
                filepath = fileDialog.FileName;
            }
            List<String> Lines = File.ReadAllLines(filepath).ToList();    //Read the selected CSV file into a list of strings

            foreach (String Line in Lines)                    //Iterte through each line in the CSV file
            {
                List<string> SplitLine = Line.Split(',').ToList();              //Split the current line of the file at every comma
                float fltTempSalePrice;
                int intTempRating;
                if ((float.TryParse(SplitLine[5], out fltTempSalePrice)) == false)
                {
                    fltTempSalePrice = 0f;                                            //Attempt to convert the sale price to a float, otherwise set it to 0
                }
                if ((Int32.TryParse(SplitLine[6], out intTempRating)) == false)
                {
                    intTempRating = -1;                                    //Attempt to convert the rating to an integer, otherwise set it to -1 to reflect that it has not been rated
                }
                Textbook CurrentTextbook = new Textbook(SplitLine[0], SplitLine[1], SplitLine[2], float.Parse(SplitLine[4]), SplitLine[3], fltTempSalePrice, intTempRating);  //Create a textbook object with the values from the current line
            
                Textbooks.Add(CurrentTextbook);   //Add the current textbook to the list of textbooks
                dgvTable.Rows.Add(SplitLine[0], SplitLine[1], SplitLine[2], SplitLine[3], SplitLine[4], SplitLine[5], SplitLine[6]);

            }

        }
        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();          //When choose file button is clicked, prompt user to choose a file
            filepath = fileDialog.FileName;
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboFilter.SelectedIndex == 0)
            {
                Search(dgvTable, Textbooks, textSearch.Text.ToLower(), false);   //Search for textbook by title if "Name" is selected
            }
            else if(comboFilter.SelectedIndex == 1)
            {
                Search(dgvTable, Textbooks, textSearch.Text.ToLower(), true);     //Search by subject if "Subject" is selected
            }

        }
        private List<Textbook> SelectionSort(List<Textbook> lst)              //Selection Sort algorithm, takes a list of textbooks as input and outputs a list of textbooks
        {
            /*
            Name: SelectionSort
            Purpose: To sort the provided textbook list by rating
            Inputs: List of textbooks
            Outputs: Sorted list of textbooks
            */
            for (int i = 0; i < lst.Count - 1; i++)
            {
                
                Textbook temp = lst[i];
                int min_index = 0;
                int min_value = 9999999;
                Textbook min_textbook = new Textbook("", "", "", 0, "", 0, 9999999);    //Arbitrary initialisation value
                
                for (int j = i; j < lst.Count; j++)
                {


                    if (lst[j].Rating < min_value)   //If current textbook rating is less than the minimum, current textbook is the new minimum
                    {
                        min_index = j;
                        min_value = lst[j].Rating;
                        min_textbook = lst[j];
                    }

                }
                lst[i] = min_textbook;                    //Swap the minimum with the temporary textbook
                
                lst[min_index] = temp;

            }
            return lst;                           //Return the list of textbooks
        }
        private void comboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFilter.SelectedIndex == 2)                  //If the index is changed to the "Rating" option, sort by rating
            {
                
                Textbooks =SelectionSort(Textbooks);                //Sort textbooks using my selection sort algorithm
                dgvTable.Rows.Clear();
                
                foreach (Textbook current_tb in Textbooks)
                {
                    string strTempRating = "none";
                    if (current_tb.Rating != -1)                     //If the textbook is unrated, its rating will be listed as -1, and this will display as "none"
                    {
                        strTempRating= current_tb.Rating.ToString();
                    }
                    dgvTable.Rows.Add(current_tb.Name, current_tb.Subject, current_tb.Seller, current_tb.Purchaser, current_tb.PurchasePrice.ToString(), current_tb.SalePrice.ToString(), strTempRating);
                }
            }

        }
    }
}
