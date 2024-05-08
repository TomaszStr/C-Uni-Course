namespace classes{
    class EmployeeTerritory{
        private String _employeeId=String.Empty;
        private String _territoryId=String.Empty;
        public String EmployeeId{
            get => _employeeId;
            set =>_employeeId = value;
        }
        public String TerritoryId{
            get => _territoryId;
            set => _territoryId = value;
        }

        public EmployeeTerritory(String empId,String terId){
            EmployeeId = empId;
            TerritoryId = terId;
        }

        public override String ToString()
        {
            return "Employee-Territory: "+EmployeeId+" "+TerritoryId;
        }

    }
}