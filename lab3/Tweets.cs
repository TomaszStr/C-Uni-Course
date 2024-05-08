using System.Text.RegularExpressions;
using System.Xml.Serialization;

public class Tweets{
    //List<Tweet>? _Data;
    public List<Tweet>? Data{
        get; set;
    }

    public void Sort(Comparer<Tweet> comparer){
        if(Data != null)
            Data.Sort(comparer);
        else
            throw new Exception("EMPTY LIST");
    }

    public Dictionary<string,List<Tweet>> ToDict(){
        if(Data == null)
            throw new Exception("EMPTY LIST");
        Dictionary<string,List<Tweet>> result = new Dictionary<string,List<Tweet>>();
        //iterate through tweets
        foreach(Tweet t in Data){
            if(!result.ContainsKey(t.UserName))//add key
                result[t.UserName] = new List<Tweet>();
            result[t.UserName].Add(t);//add tweet to list
        }

        return result;
    }

    public Dictionary<string,int> WordFrequency(){
        if(Data == null)
            throw new Exception("EMPTY LIST");
        Dictionary<string,int> result = new Dictionary<string,int>();
        foreach(Tweet t in Data){
            String text = t.Text?.ToLower() ?? "";
            //String[] strings = t.Text.Split(" "); //simple version
            String pattern = @"\s\w+\s"; //MORE ACCURATE
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

    public Dictionary<string,double> TermIDF(){
        Dictionary<string,double> result = new Dictionary<string,double>();
        // if data is null return empty dict
        if(Data == null)
            return result; 
        
        int len = Data.Count;

        foreach(Tweet t in Data){
            string pattern = @"\s\w+\s";
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

    public void ToXML(String fileName){
        XmlSerializer x = new XmlSerializer(typeof(Tweets));
        using(StreamWriter writer = File.CreateText("data.xml")){
            x.Serialize(writer,this);
        }
    }

    public static Tweets getFromXML(String filePath){
        //READ XML
        XmlSerializer x = new XmlSerializer(typeof(Tweets));
        Tweets tweets = new Tweets();
        using(StreamReader reader = new StreamReader(filePath)){
            var deserialized = x.Deserialize(reader);
            if(deserialized == null)
                throw new Exception("Deserialization unsuccessful");
            tweets = (Tweets) deserialized;
        }
        
        return tweets;
    }
}