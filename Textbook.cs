using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAC_1_TASK_3
{
    internal class Textbook
    {
        /*
Name: Textbook
Purpose: A class to store the data of induvidual textbooks
Properties: Name (string), Subject (string), Seller (string), Purchase Price (float), 
Purchaser (string), Sale Price (float), Rating (int)
*/
        public Textbook(string name, string subject, string seller, float purchasePrice, string purchaser, float salePrice, int rating)
        {
            Name = name;
            Subject = subject;
            Seller = seller;
            PurchasePrice = purchasePrice;
            Purchaser = purchaser;
            SalePrice = salePrice;
            Rating = rating;
        }


        public string Name { get; set; }    //Name of the textbook
        public string Subject { get; set; }
        public string Seller { get; set; }
        public float PurchasePrice { get; set; }   //Price it was purchased for
        public string Purchaser { get; set; }
        public float SalePrice { get; set; }     //PRice it was sold for
        public int Rating { get; set; }
    }
}
