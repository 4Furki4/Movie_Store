using System;

namespace MovieStore.Services
{
    public class ConsoleLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine($"[Console Logger]- {message}");
        }
    }
}