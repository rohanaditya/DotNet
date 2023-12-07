namespace MVC_CRUD.Models.Domain
{
    public class Product
    {
        public Guid productID { get; set; }
        public string productName { get; set; }
        public int productQuantity { get; set; }
        public double productCost { get; set; }
        public string supplier { get; set; }
        public string productImage { get; set; }
        public string description { get; set; }
        public int categoryID { get; set; }
    }
}


