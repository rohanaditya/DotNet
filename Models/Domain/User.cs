﻿namespace MVC_CRUD.Models.Domain
{
    public class User
    {
        public Guid userID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public int pincode { get; set; }
        public long mobile { get; set; }
        public double walletBalance { get; set; }



    }
}
