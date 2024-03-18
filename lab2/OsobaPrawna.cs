namespace SystemPlatnosci{
partial class OsobaPrawna: PosiadaczRachunku{

    private string _Nazwa;
    private string _Siedziba;

    public string Nazwa{
        get => _Nazwa;
    }
    public string Siedziba{
        get => _Siedziba;
    }
    
    public OsobaPrawna(string Nazwa,
                        string Siedziba){
        if(Nazwa == null || Siedziba == null)
            throw new Exception("Nazwa oraz Siedziba muszą być nie-null");

       _Nazwa = Nazwa;
       _Siedziba = Siedziba;
    }
    override public string ToString(){
        return "OSOBA PRAWNA "+_Nazwa+" "+_Siedziba;
    }

}

}