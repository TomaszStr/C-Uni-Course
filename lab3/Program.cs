
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Linq;

class Program{

    public static void Main(string[] args){
        string content = System.IO.File.ReadAllText("data.json");
        Tweets ?tweets = JsonSerializer.Deserialize<Tweets>(content);
        if(tweets == null || tweets.Data == null)
            throw new Exception("Reading file was not successful");

        //WRTITE XML
        XmlSerializer x = new XmlSerializer(typeof(Tweets));
        using (StreamWriter writer = File.CreateText("data.xml")){
                x.Serialize(writer,tweets);
            }
        
        tweets = null;

        //READ XML
        using(StreamReader reader = new StreamReader("data.xml")){
                    tweets = (Tweets)x.Deserialize(reader);
        }

        Console.WriteLine(tweets.Data.Count);

        //SORT

        tweets.Sort(new CompareDate());

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
        select word;

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

        Console.WriteLine(TopIDF.ToArray()[0].Key+" "+TopIDF.ToArray()[0].Value);
        Console.WriteLine(TopIDF.ToArray()[TopIDF.ToArray().Length-1].Key+" "+TopIDF.ToArray()[TopIDF.ToArray().Length-1].Value);

        Console.WriteLine("\nIDF TOP VALUES - MOST FREQUENT:");
        for(int i=0;i<10;i++)
            Console.WriteLine(TopIDF.ToArray()[i].Key+" "+TopIDF.ToArray()[i].Value);

        Console.WriteLine("\nIDF TOP VALUES - LEAST FREQUENT:");
        for(int i=0;i<10;i++)
            Console.WriteLine(TopIDF.ToArray()[TopIDF.ToArray().Length-1-i].Key+" "+TopIDF.ToArray()[TopIDF.ToArray().Length-1-i].Value);
    


    }




    //MADE FOR favorite-tweets.jsonl


    public static void OLDMain(string[] args){
        
        List<Tweet>? tweets = new List<Tweet>(); 
    
        tweets = FromJson(true,"data.json");

        //tweets must be passed as a reference so that it will be permanently modified
        XML(true,"tweets-list.xml",ref tweets);

        tweets.Clear();

        XML(false,"tweets-list.xml",ref tweets);

        Console.WriteLine(tweets.Count);
        
        tweets.Sort(new CompareDate());
        tweets.Sort(new CompareUsername());

        Console.WriteLine(GetOldest(tweets));
        Console.WriteLine(GetLatest(tweets));
        
        Console.WriteLine(tweets[0]);
        Console.WriteLine(tweets[tweets.Count-1]);

        Dictionary<string,List<Tweet>> dict = ToDict(tweets);

        Console.WriteLine(dict.Count);
        int sum = 0;
        foreach(String key in dict.Keys){
            sum+= dict[key].Count;
        }
        Console.WriteLine(sum);
        
        Dictionary<string,int> words = WordFrequency(tweets);
        //LINQ
        var OrderedWords = 
        from word in words
        where word.Key.Length >= 5
        orderby word.Value descending
        select word;

        for(int i=0; i<10;i++){
            Console.WriteLine(OrderedWords.ToArray()[i].Key+" times: "
            +OrderedWords.ToArray()[i].Value);
        }


        //TF IDF:
        //TF(t) = (Number of times term t appears in a document) / (Total number of terms in the document).
        //IDF(t) = log_e(Total number of documents / Number of documents with term t in it).


        Dictionary<String,double> IDFValues = TermIDF(tweets);

        var TopIDF =
        from vals in IDFValues
        //where vals.Key.Length > 4 // additional condition to remove short unimportant words
        orderby vals.Value ascending
        select vals;

        for(int i=0;i<10;i++){
            Console.WriteLine(TopIDF.ToArray()[i].Key);
        }
    }

    static public List<Tweet>? FromJson(bool InMemory,String FilePath){
        List<Tweet> ?tweets = new List<Tweet>(); // create empty list
        if(InMemory){//whole JSON stored as a string
        //WE HAVE TO MODIFY THE JSON
        //IT NEEDS TO BE A LIST 
        //IF IT IS TO BE PARSED WHOLE WITH .Deserialize
        string JsonTweets = File.ReadAllText(FilePath);
        //turn the string into a json list
        JsonTweets = "["+JsonTweets+"]";
        //add commas instead of \n
        JsonTweets = JsonTweets.Replace("\n",",");
        //remove the last comma
        JsonTweets = JsonTweets.Remove(JsonTweets.Length-2,1);
        
        tweets = JsonSerializer.Deserialize<List<Tweet>>(JsonTweets);
        }
        else{ // LINE BY LINE
            String ?line;
            Tweet ?tweet;
            System.IO.StreamReader reader = new StreamReader("favorite-tweets.jsonl");
            while((line = reader.ReadLine()) != null)
            {
                tweet = JsonSerializer.Deserialize<Tweet>(line);
                if(tweet != null)
                    tweets.Add(tweet);
            }
        }

        return tweets;
    }

    //toXML -> true=WRITE, false=READ
    static public void XML(bool toXml,String FilePath, ref List<Tweet> ?tweets){
    System.Xml.Serialization.XmlSerializer x = new XmlSerializer(typeof(List<Tweet>));  
        if(toXml){//WRITE
            if(tweets == null)
                throw new Exception("Can't write a null to a file");
            using (StreamWriter writer = File.CreateText(FilePath)){
                x.Serialize(writer,tweets);
            }
        }
        else{//READ
            using(StreamReader reader = new StreamReader(FilePath)){
                    tweets = (List<Tweet>)x.Deserialize(reader);
                    
            }
        }
    }

    static public void Sort(ref List<Tweet> ?tweets,Comparer<Tweet> comparer){
        if(tweets != null)
            tweets.Sort(comparer);
    }

    static public Tweet GetLatest(List<Tweet> tweets){
        tweets.Sort(new CompareDate());
        return tweets[tweets.Count-1];
    }

    static public Tweet GetOldest(List<Tweet> tweets){
        tweets.Sort(new CompareDate());
        return tweets[0];
    }

    static public Dictionary<string,List<Tweet>> ToDict(List<Tweet> tweets){
        Dictionary<string,List<Tweet>> result = new Dictionary<string,List<Tweet>>();

        //iterate through tweets
        foreach(Tweet t in tweets){
            if(!result.ContainsKey(t.UserName))//add key
                result[t.UserName] = new List<Tweet>();
            result[t.UserName].Add(t);//add tweet to list
        }

        return result;
    }

    static public Dictionary<string,int> WordFrequency(List<Tweet> tweets){
        Dictionary<string,int> result = new Dictionary<string,int>();
        foreach(Tweet t in tweets){
            String text = t.Text;
            
            //String[] strings = t.Text.Split(" ");//simple version
            
            //MORE ACCURATE
            String pattern = @"\b\w+\b";
            MatchCollection matches = Regex.Matches(text,pattern);
            String[] strings = matches.Select(x => x.Value).ToArray();
            
            for (int i = 0; i < strings.Length; i++)
            {
                if(!result.ContainsKey(strings[i]))//add key
                    result[strings[i]] = 0;
                result[strings[i]]++;
            }
        }
        return result;
    }

    //IDF(t) = log_e(Total number of documents / Number of documents with term t in it).
    //FOR THIS TASK: IDF = log_e(number of tweets/number of tweets containing the term)
    static public Dictionary<string,double> TermIDF(List<Tweet> tweets){
        Dictionary<string,double> result = new Dictionary<string,double>();
        int len = tweets.Count;

        foreach(Tweet t in tweets){
            string pattern = @"\b\w+\b";
            MatchCollection matches = Regex.Matches(t.Text,pattern);
            List<String> unique = matches.Select(x => x.Value).ToList();
            unique = unique.Distinct().ToList();

            foreach(string u in unique){
                if(!result.ContainsKey(u))
                    result[u]=0;
                result[u]++;
            }
        }

        foreach(string key in result.Keys){
            result[key] = Math.Log(len/result[key]);
        }
        
        return result;

    }
}

