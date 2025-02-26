using NumberSums.Classes;

namespace NumberSums
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BoardFactory factory = new();
            factory.NColumns = 7;
            factory.NRows = 7;
            factory.Density = 0.3f;
            Board board = factory.Generate();
            Console.WriteLine(board.ToString());
        }
    }
}
