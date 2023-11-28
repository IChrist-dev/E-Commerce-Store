using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace INET_2005_Final_Project.Models
{
    public class APIContent
    {
        [DisplayName("Name")]
        public string name { get; set; } = string.Empty;

        [DisplayName("Shipping Address")]
        public string address { get; set; } = string.Empty;

        [DisplayName("City")]
        public string city { get; set; } = string.Empty;

        [DisplayName("Province")]
        public string province { get; set; } = string.Empty;

        [DisplayName("Postal Code")]
        public string postalCode { get; set; } = string.Empty;

        [DisplayName("Country")]
        public string country { get; set; } = string.Empty;

        [DisplayName("Credit Card")]
        public long ccNumber { get; set; }

        [DisplayName("Expiration")]
        public string ccExpiryDate { get; set; } = string.Empty;

        [DisplayName("CVV")]
        public int cvv { get; set; }

        public string products { get; set; } = string.Empty;
    }
}
