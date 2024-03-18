// See https://aka.ms/new-console-template for more information


using System.Collections;

class Program{

    static string ostatni;
    static public void Exc_2(){
            string str = Console.ReadLine();
            string ostatni = str;

            StreamWriter sw = new StreamWriter("NazwaPliku.txt", append:true);

            while(!str.Equals("koniec!")){
                sw.WriteLine(str);
                if(str.CompareTo(ostatni) > 0)
                    ostatni = str;
                str = Console.ReadLine();
            }
            sw.Close();
        }
    
    static public void  Exc_3(string[] args){
        int pozycja, linia=0;

        StreamReader sr = new StreamReader(args[0]);
        while (!sr.EndOfStream)
        {
            linia++;
            String napis = sr.ReadLine();
            pozycja = napis.IndexOf(args[1]);
            if(pozycja != -1){
                Console.WriteLine("linijka: {0}, pozycja: {1}",linia,pozycja);
                return;
            }
        }
        Console.WriteLine("NIE ZNALEZIONO");
        sr.Close();
    }

    static public void  Exc_4(string[] args){
        n = int.Parse(args[1]),
        low = int.Parse(args[2]),
        top = int.Parse(args[3]),
        seed = int.Parse(args[4]),
        typ = int.Parse(args[5]);

        string name = args[0];
        Random random = new Random(seed);

        StreamWriter wr = new StreamWriter(path:name, append:false);

        if(typ == 0)
        for(int i=0;i<n;i++){
            //int r = random.Next(low,top);
            wr.WriteLine((int)random.Next(low,top));
        }
        else
        for(int i=0;i<n;i++){
            //double r = random.Next(low,top);
            wr.WriteLine(random.Next(low,top));
        }

        wr.Close();
        //dotnet run losowe.txt 10 0 10 1 0

    }

    static public void  Exc_5(){
        StreamReader reader = new StreamReader("losowe.txt");

        int lines=0, chars=0, cur;
        double max=double.MinValue, min = double.MaxValue, mean = 0;

        String l;
        
        while(!reader.EndOfStream){
            l = reader.ReadLine();
            chars += l.Length;
            lines++;
            cur = int.Parse(l);
            if(cur > max)
                max = cur;
            if(cur < min)
                min = cur;
            mean += cur;
        }
        mean /= lines;
        reader.Close();
        Console.WriteLine("Linii: {0}, Znaków: {1}, max: {2}, min: {3}, srednia: {4}",lines,chars,max,min,mean);
    }



    public static void Main(string[] args){
        Exc_2();
        //Exc_3(args);
        //Exc_4();
        //Exc_5();
    }
}
