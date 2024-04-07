using System.Globalization;
using classes;

class Program{

    public static void Main(string[] args){
        
        //data in CSV files is in US format
        CultureInfo.CurrentCulture = new CultureInfo("en-US");

        //read data
        List<Employee> employees = Reader<Employee>.ReadList("resources/employees.csv",",",true,
        Employee.fromCSVLine);

        List<Territory> territories = Reader<Territory>.ReadList("resources/territories.csv",",",true,
        x=>new Territory(x[0], x[1], x[2]));
        
        List<Region> regions = Reader<Region>.ReadList("resources/regions.csv",",",true,
        x=>new Region(x[0], x[1]));
        
        List<EmployeeTerritory> employeeTerritories = Reader<EmployeeTerritory>.ReadList("resources/employee_territories.csv",",",true,
        x=>new EmployeeTerritory(x[0], x[1]));

        List<Order> orders = Reader<Order>.ReadList("resources/orders.csv",",",true,Order.FromCSVLine);
        
        List<OrderDetail> orderDetails = Reader<OrderDetail>.ReadList("resources/orders_details.csv",",",true,
        x=>new OrderDetail(x[0], x[1], x[2], x[3], x[4]));

        // //Excercise 2
        // var exc2 = getSurnames(employees);
        // foreach(String str in exc2)
        //     Console.WriteLine(str);
        
        // //Excercise 3
        // var exc3 = getEmployeesLocation(employees,employeeTerritories,territories,regions);

        // foreach(String str in exc3)
        //     Console.WriteLine(str);

        // //Excercise 4
        // var exc4 = getRegionEmployees(employees,employeeTerritories,territories,regions);

        // foreach(Region reg in exc4.Keys){
        //     Console.WriteLine(reg.ToString());
        //     foreach(Employee emp in exc4[reg])
        //         Console.WriteLine(emp.ToString());
        // }

        // //Excercise 5
        // var exc5 = getRegionEmployeesCount(employees,employeeTerritories,territories,regions);
        // foreach(Region reg in exc5.Keys)
        //     Console.WriteLine(reg.ToString()+" number of employees: "+exc5[reg]);

        //Excercise 6
        var exc6 = getEmployeesOrderStatistics(employees,orders,orderDetails);
        foreach(Employee emp in exc6.Keys)
            Console.WriteLine("{0}\nStatistics:\nMax:{1}\nAverage:{2}\nCount:{3}\n",
            emp.ToString(),exc6[emp].Max,exc6[emp].Avg,exc6[emp].Count);

    }
    

    //EXCERCISE 2
    public static List<String> getSurnames(List<Employee> employees){
        var surnames = from e in employees
        select e.Lastname;
        return surnames.ToList(); 
    }

    //EXCERCISE 3
    public static List<String> getEmployeesLocation(List<Employee> employees,List<EmployeeTerritory> emplTer,
                                List<Territory> territories, List<Region> regions){
        List<String> result= new List<String>();
        var emplLocatation = from e in employees
        join et in emplTer on e.EmployeeId equals et.EmployeeId
        join t in territories on et.TerritoryId equals t.TerritoryId
        join r in regions on t.RegionId equals r.RegionId
        select new{Lastname=e.Lastname,Territory=t.TerritoryDescription,Region=r.RegionDescription};

        //Save query result as string in list
        foreach(var v in emplLocatation){
            result.Add("Employee: "+v.Lastname
            +" Territory: "+v.Territory
            +" Region: "+v.Region);
        }

         return result;
    }

    //EXCERCISE 4

    public static Dictionary<Region,List<Employee>> getRegionEmployees(List<Employee> employees,List<EmployeeTerritory> emplTer,
                                List<Territory> territories, List<Region> regions){
        Dictionary<Region,List<Employee>> result= new Dictionary<Region, List<Employee>>();

        var query = from er in 
        (from r in regions
        join t in territories on r.RegionId equals t.RegionId
        join et in emplTer on t.TerritoryId equals et.TerritoryId
        join e in employees on et.EmployeeId equals e.EmployeeId
        select new {Region=r,Employee=e})
        group er.Employee by er.Region into grouped
        select new{Region = grouped.Key,List=grouped.ToList()};

        //CAST TO DICTIONARY
        result = query.ToDictionary(x=>x.Region,x=>x.List);

        return result;
    }

    //EXCERCISE 5
    public static Dictionary<Region,int> getRegionEmployeesCount(List<Employee> employees,List<EmployeeTerritory> emplTer,    
                            List<Territory> territories, List<Region> regions){
        Dictionary<Region,int> result;

        var query = from er in 
        (from r in regions
        join t in territories on r.RegionId equals t.RegionId
        join et in emplTer on t.TerritoryId equals et.TerritoryId
        join e in employees on et.EmployeeId equals e.EmployeeId
        select new {Region=r,Employee=e})
        group er by er.Region into grouped
        select new {Region=grouped.Key,EmpList=grouped.Count()};

        result = query.ToDictionary(x=>x.Region,x=>x.EmpList);
        return result;
    }

    //EXCERCISE 6
    public static Dictionary<Employee,(double Max,double Avg,int Count)> getEmployeesOrderStatistics(List<Employee> employees,List<Order> orders,List<OrderDetail> orderDetails){
        Dictionary<Employee,(double Max,double Avg,int Count)> result;

        var query = from empStats in(
            from e in employees
            join o in orders on e.EmployeeId equals o.EmployeeId
            join od in orderDetails on o.OrderId equals od.Orderid
            select new{Employee=e,Order=o,OrderDetail=od})
            group empStats by empStats.Employee into grouped
            select new{grouped.Key,
                       Max=grouped.Max(grouped=>double.Parse(grouped.OrderDetail.UnitPrice)),
                       Average=grouped.Average(grouped=>double.Parse(grouped.OrderDetail.UnitPrice)),
                       Count=grouped.Count(grouped=>grouped.Order!=null)};

        result=query.ToDictionary(x=>x.Key,x=>(x.Max,x.Average,x.Count));//(Dictionary<Employee,(double Max,double Avg,int Count)>)query;
        return result;
    }
}
