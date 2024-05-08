using System.Globalization;

public class Tweet{

    // String ?_Text;
    // String ?_UserName;
    // String ?_LinkToTweet;
    // String ?_FirstLinkUrl;
    // String ?_CreatedAt;
    // String ?_TweetEmbedCode;

    public String Text{
        get; set;
    } = String.Empty;
    public String UserName{
        get; set;
    } = String.Empty;
    public String? LinkToTweet{
        get; set;
    }
    public String? FirstLinkUrl{
        get; set;
    }
    public String? CreatedAt{
        get; set;
    }
    public String? TweetEmbedCode{
        get; set;
    }

    public override String ToString()
    {
        return "USERNAME: "+UserName
        +"\nTWEET:" + Text
        +"\nDATE: " + CreatedAt;
    }

}
public class TweetCompareUsername : Comparer<Tweet>
{
    public override int Compare(Tweet? x, Tweet? y)
    {
        //check nulls
        if(x==null && y==null)
            return 0;
        else if(x == null)
            return -1;
        else if(y == null)
            return 1;
        
        return string.Compare(x.UserName,y.UserName);
    }
}

public class TweetCompareDate : Comparer<Tweet>
{
    public override int Compare(Tweet? x, Tweet? y)
    {
        //check nulls
        if(x==null && y==null)
            return 0;
        else if(x == null)
            return -1;
        else if(y == null)
            return 1;

        if(x.CreatedAt==null && y.CreatedAt==null)
            return 0;
        else if(x.CreatedAt == null)
            return -1;
        else if(y.CreatedAt == null)
            return 1;
        
        string format = "MMMM dd, yyyy 'at' hh:mmtt";
        DateTime xDate = DateTime.ParseExact(x.CreatedAt,format,CultureInfo.InvariantCulture),
        yDate= DateTime.ParseExact(y.CreatedAt,format,CultureInfo.InvariantCulture);
        //DateTime.TryParseExact(x.CreatedAt,format:" ",CultureInfo.InvariantCulture,DateTimeStyles.None,out xDate);
        return DateTime.Compare(xDate,yDate);
    }
}
