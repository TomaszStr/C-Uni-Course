namespace classes{

    class OrderDetail{
        public String Orderid{get;set;}
        public String ProductId{get;set;}
        public String UnitPrice{get;set;}
        public String Quantity{get;set;}
        public String Discount{get;set;}

        public OrderDetail(String orderId,String productId,String unitprice,String quantity,String discount){
            Orderid=orderId;
            ProductId=productId;
            UnitPrice=unitprice;
            Quantity=quantity;
            Discount=discount;
        }
        public static OrderDetail fromCSVLine(String[] x){
            return new OrderDetail(x[0],x[1],x[2],x[3],x[4]);
        }
    }
}