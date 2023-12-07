namespace MVC_CRUD.Models
{
    public class AddProductViewModel
    {
        public string productName { get; set; }
        public int productQuantity { get; set; }
        public double productCost { get; set; }
        public string supplier { get; set; }
        public string description { get; set; }
        public string productImage { get; set; }
        public int categoryID { get; set; }
    }
}
