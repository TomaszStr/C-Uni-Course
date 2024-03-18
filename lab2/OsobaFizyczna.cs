namespace SystemPlatnosci{
partial class OsobaFizyczna: PosiadaczRachunku {

    private string _imie;
    private string _nazwisko;
    private string _drugieImie;
    private string ?_PESEL;
    private string ?_numerPaszportu;

    public string imie{
        get => _imie;
        //set => _imie = value;
    }
    public string nazwisko{
        get => _nazwisko;
    }
    public string drugieImie{
        get => _drugieImie;
    }
    public string ?PESEL{
        get => _PESEL;
        //zabezpieczenie
        set{
            if(value == null || value.Length != 11)
                throw new Exception("Nr PESEL nieprawidlowa dlugosc");
            else
                _PESEL = value;
        }
    }
    public string ?numerPaszportu{
        get => _numerPaszportu;
    }

    public OsobaFizyczna(string imie,
                        string nazwisko,
                        string drugieImie,
                        string PESEL,
                        string numerPaszportu){
        if(PESEL == null && numerPaszportu == null)
            throw new Exception("PESEL albo numer paszportu muszą być nie null");
        //DODATEK
        if(PESEL == null || PESEL.Length != 11)
            throw new Exception("Nr PESEL nieprawidlowa dlugosc");

        _imie = imie;
        _nazwisko = nazwisko;
        _drugieImie = drugieImie;
        _PESEL = PESEL;
        _numerPaszportu = numerPaszportu;
    }
    override public string ToString(){
        return "OSOBA FIZYCZNA "+_imie+" "+_nazwisko;
    }
}

}