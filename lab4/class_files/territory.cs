namespace classes{

    class Territory{
        private String _territoryId="";
        private String _territoryDescription="";
        private String _regionId="";
        public String TerritoryId{
            get{return _territoryId;}
            set{_territoryId = value;}
        }
        public String TerritoryDescription{
            get{return _territoryDescription;}
            set{_territoryDescription = value;}
        }
        public String RegionId{
            get{return _regionId;}
            set{_regionId = value;}
        }

        public Territory(String terId,String terdesc,String regId){
            TerritoryId = terId;
            TerritoryDescription = terdesc;
            RegionId = regId;
        }

        public override String ToString()
        {
            return "Territory: "+TerritoryDescription+" "+TerritoryId+" "+RegionId;
        }

    }
}