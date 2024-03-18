namespace SystemPlatnosci{
partial class RachunekBankowy{
    private string _numer;
    private Decimal _stanRachunku;
    private bool _czyDozwolonyDebet;
    private List<PosiadaczRachunku> _PosiadaczeRachunku;
    private List<Transakcja> _Transakcje = new List<Transakcja>();
    public string numer{
        get => _numer;
    }
    public bool czyDozwolonyDebet{
        get => _czyDozwolonyDebet;
        set => _czyDozwolonyDebet = value;
    }
    public Decimal stanRachunku{
        get => _stanRachunku;
        set => _stanRachunku = value;
    }
    public List<PosiadaczRachunku> PosiadaczeRachunku{
        get => _PosiadaczeRachunku;
    }
    public List<Transakcja> Transakcje{
        get => _Transakcje;
    }

    public RachunekBankowy(string numer,
                    Decimal stanRachunku,
                    bool czyDozwolonyDebet,
                    List<PosiadaczRachunku> posiadaczeRachunku){
        if(posiadaczeRachunku.Count < 1)
            throw new Exception("Potrzebny chociaż jeden posiadacz rachunku");
        
        _numer = numer;
        _czyDozwolonyDebet = czyDozwolonyDebet;
        _stanRachunku = stanRachunku;
        _PosiadaczeRachunku = posiadaczeRachunku;

    }

    static public RachunekBankowy operator +(RachunekBankowy rachunekBankowy,PosiadaczRachunku posiadaczRachunku){
        if(rachunekBankowy.PosiadaczeRachunku.Contains(posiadaczRachunku))
            throw new Exception("Posiadacz juz znajduje sie na liscie");
        else
            rachunekBankowy.PosiadaczeRachunku.Add(posiadaczRachunku);
        return rachunekBankowy;
    }

    static public RachunekBankowy operator -(RachunekBankowy rachunekBankowy,PosiadaczRachunku posiadaczRachunku){
        if(rachunekBankowy.PosiadaczeRachunku.Count()<1)
            throw new Exception("Niemozliwe jest usuniecie ostatniego posiadacza");
        if(!rachunekBankowy.PosiadaczeRachunku.Contains(posiadaczRachunku))
            throw new Exception("Posiadacza nie ma na liscie");

        rachunekBankowy.PosiadaczeRachunku.Remove(posiadaczRachunku);
        return rachunekBankowy;
    }

    public static void DokonajTransakcji(RachunekBankowy ?rachunekZrodlowy,
                                        RachunekBankowy ?rachunekDocelowy,
                                        Decimal kwota,
                                        string opis){
        if(kwota < 0 
            || (rachunekZrodlowy == null && rachunekDocelowy == null)
            || (rachunekZrodlowy != null && rachunekZrodlowy.czyDozwolonyDebet == false && kwota > rachunekZrodlowy.stanRachunku))
                throw new Exception("Transakcja niemożliwa");
        
        //WPLATA
        if(rachunekZrodlowy == null && rachunekDocelowy != null){
            rachunekDocelowy.stanRachunku += kwota;
            rachunekDocelowy.Transakcje.Add(new Transakcja(null,rachunekDocelowy,kwota,opis));
        }
        else if(rachunekDocelowy == null && rachunekZrodlowy != null){
            rachunekZrodlowy.stanRachunku -= kwota;
            rachunekZrodlowy.Transakcje.Add(new Transakcja(rachunekZrodlowy,null,kwota,opis));     
        }
        //warunek zeby nie bylo warningow
        else if(rachunekDocelowy != null && rachunekZrodlowy != null){
           rachunekZrodlowy.stanRachunku -= kwota;
           rachunekDocelowy.stanRachunku += kwota;
           Transakcja t = new Transakcja(rachunekZrodlowy,rachunekDocelowy,kwota,opis);
           rachunekZrodlowy._Transakcje.Add(t);
           rachunekDocelowy._Transakcje.Add(t);
        }
    }

    public override string ToString()
    {
        string value = "RACHUNEK BANKOWY\nNumer: "+numer
        +"\nStan rachunku: "+stanRachunku.ToString()
        +"\nPosiadacze:\n";
        foreach(PosiadaczRachunku p in PosiadaczeRachunku)
            value += "-"+p.ToString()+"\n";
        value += "Transakcje:\n";
        foreach(Transakcja t in Transakcje)
            value += "-"+t.ToString()+"\n";
        return value;//base.ToString();
    }

}

}