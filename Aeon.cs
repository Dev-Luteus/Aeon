using System.Text;
using Aeon.classes;

namespace Aeon {
    class Program
    {
        public static Player               playerChar         = new Player();
        public static GameWindow           mainWindow         = new GameWindow(playerChar);
        public static LoadScene            loadScene          = new LoadScene();
        public static CharacterCreation    characterCreation  = new CharacterCreation(playerChar);
        public static StoryString          story              = new StoryString(playerChar);
        static void Main() 
        {
            //Start();
            loadScene.Main();
            characterCreation.Main();
            story.Main();               // Call Story, to feed into mainWindow
            mainWindow.Main();
        }
        static void Start()
        {
            //Console.WriteLine("Welcome, " + playerChar.name);
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