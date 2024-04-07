namespace classes{
    class Employee{
        // private String employeeid;
        // private String lastname;
        // private String firstname;
        // private String title;
        // private String titleofcourtesy;
        // private String birthdate;
        // private String hiredate;
        // private String address;
        // private String city;
        // private String region;
        // private String postalcode;
        // private String country;
        // private String homephone;
        // private String extension;
        // private String photo;
        // private String notes;
        // private String reportsto;
        // private String photopath;

        public String EmployeeId{get; set;}
        public String Lastname{get; set;}
        public String Firstname{get; set;}
        public String Title{get; set;}
        public String TitleOfCourtesy{get; set;}
        public String Birthdate{get; set;}
        public String HireDate{get; set;}
        public String Address{get; set;}
        public String City{get; set;}
        public String Region{get; set;}
        public String Postalcode{get; set;}
        public String Country{get; set;}
        public String Homephone{get; set;}
        public String Extension{get; set;}
        public String Photo{get; set;}
        public String Notes{get; set;}
        public String Reportsto{get; set;}
        public String Photopath{get; set;}
        
        public Employee(String employeeid,
        String lastname,
        String firstname,
        String title,
        String titleofcourtesy,
        String birthdate,
        String hiredate,
        String address,
        String city,
        String region,
        String postalcode,
        String country,
        String homephone,
        String extension,
        String photo,
        String notes,
        String reportsto,
        String photopath){
            EmployeeId = employeeid;
            Lastname = lastname;
            Firstname = firstname;
            Title = title;
            TitleOfCourtesy = titleofcourtesy;
            Birthdate = birthdate;
            HireDate = hiredate;
            Address = address;
            City = city;
            Region = region;
            Postalcode = postalcode;
            Country = country;
            Homephone = homephone;
            Extension = extension;
            Photo = photo;
            Notes = notes;
            Reportsto = reportsto;
            Photopath = photopath;
        }

        public static Employee fromCSVLine(String[] x){
            return new Employee(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9], x[10], x[11], x[12], x[13], x[14], x[15], x[16],x[17]);
        }

        public override String ToString(){
            return "Employee: "
            +EmployeeId+" "
            +Firstname+" "
            +Lastname;
        } 
    }
}