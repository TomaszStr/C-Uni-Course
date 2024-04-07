
class Program{
    public static void Main(String[] args){
        //Excercise1(args);

        // // //var files = Directory.GetFiles("files","*",SearchOption.TopDirectoryOnly);
        // var files = Directory.GetFileSystemEntries("files","*.txt",SearchOption.AllDirectories);
        // foreach(string s in files)
        //     Console.WriteLine(s);

        //Excercise3(args);
        Excercise4(args);
    }

    public static void Excercise1(String[] args){

        Console.WriteLine("Insert n - number of producers");
        String ?s = Console.ReadLine();
        int n = int.Parse(s);

        Console.WriteLine("Insert m - number of consumers");
        s= Console.ReadLine();
        int m = int.Parse(s);

        List<Producer> producers = new List<Producer>();
        List<Consumer> consumers = new List<Consumer>();

        Queue<Product> memory = new Queue<Product>();
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

        //Console.WriteLine("Threads stopped");
    }

    public static void Excercise2(String[] args){
        DirectoryMonitor monitor = new DirectoryMonitor("files");

        new Thread(new ThreadStart(monitor.listenKey)).Start();
        new Thread(new ThreadStart(monitor.start)).Start();
    }

    public static void Excercise3(String[] args){
        FileSearcher searcher = new FileSearcher("files","*.txt");

        new Thread(new ThreadStart(searcher.search)).Start();
        new Thread(new ThreadStart(searcher.start)).Start();
    }

    public static void Excercise4(String[] args){
        ThreadStarter starter = new ThreadStarter(10000);
        new Thread(new ThreadStart(starter.start)).Start();
    }
}
