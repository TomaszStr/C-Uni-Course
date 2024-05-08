
class Program{
    public static void Main(String[] args){
        //Excercise1(args);

        //Excercise2(args);

        //Excercise3(args);

        Excercise4(args);
    }

    public static void Excercise1(String[] args){

        Console.WriteLine("Insert n - number of producers");
        string s = Console.ReadLine() ?? string.Empty;
        int n = int.Parse(s);

        Console.WriteLine("Insert m - number of consumers");
        s = Console.ReadLine() ?? string.Empty;
        int m = int.Parse(s);

        // Threads
        List<Producer> producers = new List<Producer>();
        List<Consumer> consumers = new List<Consumer>();

        // Where all products are stored.
        Queue<Product> memory = new Queue<Product>();

        // For memory synchronization
        Mutex mutex = new Mutex();

        for(int i=0;i<n;i++){
            Producer p = new Producer(i,1000+i*100,memory,mutex);
            Thread t = new Thread(new ThreadStart(p.start));
            producers.Add(p);
            t.Start();
        }

        for(int i=0;i<m;i++){
            Consumer c = new Consumer(i,1100+i*100,memory,mutex);
            Thread t = new Thread(new ThreadStart(c.start));
            consumers.Add(c);
            t.Start();
        }
        
        Console.WriteLine("Press q to stop program");
        while(Console.ReadKey(true).KeyChar != 'q'){

        }

        foreach(Producer p in producers)
            p.stop();
        
        foreach(Consumer c in consumers)
            c.stop();

        Console.WriteLine("Threads stopped");
    }

    public static void Excercise2(String[] args){
        DirectoryMonitor monitor = new DirectoryMonitor("files");

        new Thread(new ThreadStart(monitor.listenKey)).Start();
        new Thread(new ThreadStart(monitor.start)).Start();
    }

    public static void Excercise3(String[] args){
        FileSearcher searcher = new FileSearcher("files","*.txt");
        
        // Begin filesearcher -> wait for search result
        new Thread(new ThreadStart(searcher.start)).Start();
        // Begin search
        new Thread(new ThreadStart(searcher.search)).Start();
    }

    public static void Excercise4(String[] args){
        int number = int.Parse(args[0]);
        ThreadStarter starter = new ThreadStarter(number);
        new Thread(new ThreadStart(starter.start)).Start();
    }
}
