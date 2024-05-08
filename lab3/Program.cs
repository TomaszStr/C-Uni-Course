
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Linq;

class Program{

    public static void Main(string[] args){

        string content = System.IO.File.ReadAllText("data.json");

        Tweets ?tweets = JsonSerializer.Deserialize<Tweets>(content);

        if(tweets == null || tweets.Data == null)
            throw new Exception("Read was not successful");

        Console.WriteLine(tweets.Data.Count);

        // //WRTITE XML

        tweets.ToXML("data.xml");
        
        tweets = null;

        // //READ XML
        
        tweets = Tweets.getFromXML("data.xml");

        if(tweets == null || tweets.Data == null)
            throw new Exception("Read was not successful");
            
        Console.WriteLine(tweets.Data.Count);

        //SORT (IN PLACE)

        tweets.Sort(new TweetCompareDate());

        //OLDEST AND LATEST
        Console.WriteLine("OLDEST\n"+tweets.Data[0]);
        Console.WriteLine("LATEST\n"+tweets.Data[tweets.Data.Count-1]);

        //DIRECTORY
        Dictionary<string,List<Tweet>> dict = tweets.ToDict();

        //WORD FREQUENCY
        Dictionary<string,int> freq = tweets.WordFrequency();

        //TOP 10 WORDS
        var OrderedWords = 
        from word in freq
        where word.Key.Length >= 5
        orderby word.Value descending
        select word ;

        for(int i=0; i<10;i++){
            Console.WriteLine("Word: "+OrderedWords.ToArray()[i].Key+" Times: "
            +OrderedWords.ToArray()[i].Value);
        }

        //IDF INDEX
        Dictionary<string,double> IDF = tweets.TermIDF();

        var TopIDF =
        from vals in IDF
        //where vals.Key.Length > 4 // additional condition to remove short unimportant words
        orderby vals.Value ascending
        select vals;

        //Console.WriteLine(TopIDF.ToArray()[0].Key+" "+TopIDF.ToArray()[0].Value);
        //Console.WriteLine(TopIDF.ToArray()[TopIDF.ToArray().Length-1].Key+" "+TopIDF.ToArray()[TopIDF.ToArray().Length-1].Value);

        Console.WriteLine("\nIDF TOP VALUES - MOST FREQUENT:");
        for(int i=0;i<10;i++)
            Console.WriteLine(TopIDF.ToArray()[i].Key+" "+TopIDF.ToArray()[i].Value);

        Console.WriteLine("\nIDF TOP VALUES - LEAST FREQUENT:");
        for(int i=0;i<10;i++)
            Console.WriteLine(TopIDF.ToArray()[TopIDF.ToArray().Length-1-i].Key+" "+TopIDF.ToArray()[TopIDF.ToArray().Length-1-i].Value);
    


    }
}

