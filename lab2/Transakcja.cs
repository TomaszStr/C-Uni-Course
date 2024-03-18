namespace SystemPlatnosci{
partial class Transakcja{
    private RachunekBankowy ?_rachunekZrodlowy;
    private RachunekBankowy ?_rachunekDocelowy;
    private Decimal _kwota;
    private string _opis;

    public RachunekBankowy ?rachunekZrodlowy{
        get => _rachunekZrodlowy;
    }
    public RachunekBankowy ?rachunekDocelowy{
        get => _rachunekDocelowy;
    }
    public Decimal kwota{
        get => _kwota;
    }
    public string opis{
        get=>opis;
    }

    public override string ToString()
    {
        string t = "Transakcja:\nRachunek zrodlowy: ";
        t+= rachunekZrodlowy!=null? rachunekZrodlowy.numer : "-";
        t+=" Rachunek docelowy: ";
        t+=rachunekDocelowy!=null? rachunekDocelowy.numer: "-";
        t+=" Kwota: "+_kwota+" Opis: "+_opis;
        return t;
    }
    public Transakcja(RachunekBankowy ?rachunekZrodlowy,
                RachunekBankowy ?rachunekDocelowy,
                Decimal kwota,
                string opis){
        if(rachunekDocelowy == null && rachunekZrodlowy == null)
            throw new Exception("Rachunki zrodlowe i docelowe nie moga byc null'ami");
        _rachunekZrodlowy = rachunekZrodlowy;
        _rachunekDocelowy = rachunekDocelowy;
        _kwota = kwota;
        _opis = opis;
    }

}

}