namespace classes{

    class Region{
        private String _regionDescription=String.Empty;
        private String _regionId=String.Empty;
        public String RegionDescription{
            get{return _regionDescription;}
            set{_regionDescription = value;}
        }
        public String RegionId{
            get{return _regionId;}
            set{_regionId = value;}
        }

        public Region(String regId,String terdesc){
            RegionDescription = terdesc;
            RegionId = regId;
        }

        public override string ToString()
        {
            return "Region: "+RegionDescription+" "+RegionId;
        }
    }
}