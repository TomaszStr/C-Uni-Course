
class ThreadStarter{

    public int n;
    public long count;

    public EventWaitHandle ewhChild;
    public EventWaitHandle ewhParent;
    public ThreadStarter(int v){
        n = v;
        count=0;
        ewhChild = new EventWaitHandle(false, EventResetMode.AutoReset);
        ewhParent = new EventWaitHandle(false, EventResetMode.AutoReset);
    }

    public void start(){
        Console.WriteLine("STARTING THREADS");
        for(int i=0; i<n; i++){
            Thread t = new Thread(new ThreadStart(new MyThread(this).start));
            t.Start();
        }
        // while(count < n){}
        // Console.WriteLine("ALL THREADS STARTED");
        //Poczekaj aż zostanie uruchomione dokładnie maksymalnaLiczbaWatkow
        while (Interlocked.Read(ref count) != n)
        {
            Thread.Sleep(100);
        }
        Console.WriteLine("ALL THREADS STARTED");
        //sygnalizuj do każdego wątku potomnego, czekaj aż wątek potomny odpowie sygnałem ewhRodzic
        while (Interlocked.Read(ref count) > 0)
        {
            WaitHandle.SignalAndWait(ewhChild, ewhParent);
        }
        Console.WriteLine("ALL THREADS STOPPED");
    }
}

class MyThread{
    ThreadStarter parent;
    public bool EndMe=false;
    public MyThread(ThreadStarter threadStarter){
        parent = threadStarter;
    }
    public void start(){
        Interlocked.Increment(ref parent.count);
        parent.ewhChild.WaitOne();
        //Zmniejsz zmienną liczącą liczbę aktywnych wątków o 1
        Interlocked.Decrement(ref parent.count);
        //Odeślij do głównego wątku sygnał, odblokuje to operację
        //WaitHandle.SignalAndWait(ewhDziecko, ewhRodzic); 
        parent.ewhParent.Set();
        //Console.WriteLine("Koniec wątku numer "+ numer);
    }
}