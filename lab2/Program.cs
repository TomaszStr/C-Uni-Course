// See https://aka.ms/new-console-template for more information

using SystemPlatnosci;
class Program{

    public static void Main(string[] args){
        OsobaFizyczna fiz = new OsobaFizyczna("a","owski","b","11122233344","123");
        //Console.WriteLine(fiz.ToString());
        string s = fiz.imie;
        //fiz.imie += 'a';
        //fiz.PESEL = "a";
        //Console.WriteLine(fiz.PESEL);
        OsobaPrawna praw = new OsobaPrawna("polbud","krk");
        //Console.WriteLine(praw);
        
        List<PosiadaczRachunku> posiadacze = new List<PosiadaczRachunku>();
        posiadacze.Add(fiz);
        posiadacze.Add(praw);

        RachunekBankowy rachunek = new RachunekBankowy("1",0,false,posiadacze);
        RachunekBankowy rachunek2 = new RachunekBankowy("2",100,false,posiadacze);
        Console.WriteLine(rachunek);

        Transakcja t1=new Transakcja(rachunek,null,100,"wpłata"), 
        t2=new Transakcja(null,rachunek,100,"wypłata");

        RachunekBankowy.DokonajTransakcji(null,rachunek,100,"wpłata");
        
        Console.WriteLine(rachunek);
        
        RachunekBankowy.DokonajTransakcji(rachunek,null,100,"wypłata");

        Console.WriteLine(rachunek);

        try{
        RachunekBankowy.DokonajTransakcji(rachunek,null,100,"wypłata");
        }
        catch(Exception e){Console.WriteLine(e.Message);}

        Console.WriteLine(rachunek);

        rachunek -= fiz;

        Console.WriteLine(rachunek);

        RachunekBankowy.DokonajTransakcji(rachunek2,rachunek,100,"przelew");

        Console.WriteLine(rachunek);
        Console.WriteLine(rachunek2);

        RachunekBankowy.DokonajTransakcji(rachunek2,rachunek,-100,"przelew");
    }   
}
