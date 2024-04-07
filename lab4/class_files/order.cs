namespace classes{

    class Order{
        public String OrderId { get; set; }
        public String CustomerId { get; set; }
        public String EmployeeId { get; set; }
        public String OrderDate { get; set; } 
        public String RequiredDate { get; set; } 
        public String ShippedDate { get; set; }
        public String ShipVia { get; set; }
        public String Freight { get; set; }
        public String ShipName { get; set; }
        public String ShipAddress { get; set; }
        public String ShipCity { get; set; }
        public String ShipRegion { get; set; }
        public String ShipPostalCode { get; set; }
        public String ShipCountry { get; set; }

        public Order(String orderId, String customerId, String employeeId, String orderDate, String requiredDate, String shippedDate,
                 String shipVia, String freight, String shipName, String shipAddress, String shipCity, String shipRegion,
                 String shipPostalCode, String shipCountry)
        {
            OrderId = orderId;
            CustomerId = customerId;
            EmployeeId = employeeId;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            ShipVia = shipVia;
            Freight = freight;
            ShipName = shipName;
            ShipAddress = shipAddress;
            ShipCity = shipCity;
            ShipRegion = shipRegion;
            ShipPostalCode = shipPostalCode;
            ShipCountry = shipCountry;
        }

        public static Order FromCSVLine(string[] x)
        {
          return new Order(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9], x[10], x[11], x[12], x[13]);
        }
    }
}