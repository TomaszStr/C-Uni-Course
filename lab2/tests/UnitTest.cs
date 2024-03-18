namespace testy;

using SystemPlatnosci;

public class UnitTest1
{
    [Fact]
    public void TestFizyczna()
    {
        OsobaFizyczna fiz = new OsobaFizyczna("Jan","Kowalski","Adam","11122233344",null);
        Assert.Equal("Jan",fiz.imie);
        Assert.Equal("Kowalski",fiz.nazwisko);
        Assert.Equal("Adam",fiz.drugieImie);
        Assert.Equal("11122233344",fiz.PESEL);
    }

    [Fact]
    public void TestPeselException()
    {
        Assert.Throws<Exception>(() =>
        {
            OsobaFizyczna fiz = new OsobaFizyczna("Jan","Kowalski","Adam","11",null);
        });
    }

    [Fact]
    public void TestPrawna()
    {
        OsobaPrawna prawna = new OsobaPrawna("BudPol","Krakow");
        Assert.Equal("BudPol",prawna.Nazwa);
        Assert.Equal("Krakow",prawna.Siedziba);
    }

    [Fact]
    public void TestPrawnaException()
    {
        Assert.Throws<Exception>(
            ()=>{new OsobaPrawna(null,null);}
        );
    }

    [Fact]
    public void TestRachunek()
    {
        OsobaFizyczna fiz = new OsobaFizyczna("Jan","Kowalski","Adam","11122233344",null);
        OsobaPrawna prawna = new OsobaPrawna("BudPol","Krakow");
        List<PosiadaczRachunku> posiadacze = new List<PosiadaczRachunku>();
        posiadacze.Add(fiz);
        posiadacze.Add(prawna);
        RachunekBankowy rachunek = new RachunekBankowy("1",0,false,posiadacze);
        RachunekBankowy rachunek2 = new RachunekBankowy("2",100,false,posiadacze);
        
        Assert.Equal(100,rachunek2.stanRachunku);
        Assert.Equal(2,rachunek.PosiadaczeRachunku.Count);
        Assert.Equal(2,rachunek2.PosiadaczeRachunku.Count);

        RachunekBankowy.DokonajTransakcji(null,rachunek,100,"wpłata");
        Assert.Equal(100,rachunek.stanRachunku);
        RachunekBankowy.DokonajTransakcji(rachunek,null,100,"wypłata");
        Assert.Equal(0,rachunek.stanRachunku);

        rachunek -= fiz;
        Assert.DoesNotContain(fiz,rachunek.PosiadaczeRachunku);
        rachunek += fiz;
        Assert.Contains(fiz,rachunek.PosiadaczeRachunku);

        RachunekBankowy.DokonajTransakcji(rachunek2,rachunek,100,"przelew");

        Assert.Equal(100,rachunek.stanRachunku);
        Assert.Equal(0,rachunek2.stanRachunku);
    }

}