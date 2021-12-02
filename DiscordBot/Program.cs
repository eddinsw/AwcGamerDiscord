using System.Threading.Tasks;

namespace DiscordBot
{
    internal class Program
    {
        public static Task Main(string[] args)
            => Startup.RunAsync(args);
    }
}