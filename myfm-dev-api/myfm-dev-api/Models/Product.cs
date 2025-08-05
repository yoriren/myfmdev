namespace myfm_dev_api.Models
{
    public class Product
    {
        public string productId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal unitPrice { get; set; }
        public decimal price { get; set; }
        public int maximumQuantity { get; set; }
    }
}
