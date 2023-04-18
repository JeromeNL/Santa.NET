using KwikKwekSnack.Console.Utils;
using KwikKwekSnack.Data;
using System.Timers;

namespace KwikKwekSnack.Console;

public static class Program
{
    private static int _intervalTimer = 5000;

    public static void Main(string[] args)
    {
        SetupTimer();
    }

    private static void SetupTimer()
    {
        var timer = new System.Timers.Timer();
        timer.Interval = _intervalTimer;
        timer.Elapsed += OnTimedEvent;
        timer.Enabled = true;

        System.Console.WriteLine("Press \'q\' to quit");
        Thread.Sleep(2 * 1000);// give user time to read

        OnTimedEvent(null, null!);// no initial delay
        while (System.Console.Read() != 'q'){
        }
    }


    private static void OnTimedEvent(object? sender, ElapsedEventArgs e)
    {
        var curTime = DateTime.Now.ToString("HH:mm:ss");
        using var ctx = new KwikKwekSnackContext();
        var combinedList = OrderUtil.RequestOrdersToCombinedList(ctx);
        OrderUtil.ReplaceOrdersAtTick(ctx);

        // Print
        System.Console.Clear();
        OrderUtil.PrintOrderStatus(combinedList);
        System.Console.WriteLine("Last updated at: " + curTime);
    }
}