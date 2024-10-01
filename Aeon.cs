using System.Text;
using Aeon.classes;

namespace Aeon {
    class Program
    {
        public static Player playerChar = new Player();
        public static CharacterCreation playerCharCreation = new CharacterCreation(); // Changed to instance call
        static void Main()
        {
            //Start();
            playerCharCreation.Main();
        }
        static void Start()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Aeon Dungeon");
            Console.WriteLine("Enter your name: ");
            playerChar.name = Console.ReadLine();
            Console.WriteLine("Welcome, " + playerChar.name);
        }
    }
}


// ├── Overworld
// │    ├── Region
// │    │    ├── SubRegion
// │    │    │   ├── Room
// │    │    │   ├── Room
// │    │    │   ├── Room
// │    │    ├── SubRegion
// │    │    │   ├── Room
// │    │    │   ├── Room