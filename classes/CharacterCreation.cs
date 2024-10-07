using System.Net;
using System.Text;

namespace Aeon.classes
{
    public class CharacterCreation
    {
        private Player playerChar;
        
        // Constructor to accept the Player instance
        public CharacterCreation(Player player)
        {
            playerChar = player; // Save the passed Player instance
        }

        public void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            GameWindow.Story = "Welcome to the Aeon Dungeon character creator. |" +
                               "\x1b[92mEnter Your Name:\x1b[39m";
            
            GameWindow.DrawUI(GameWindow.mainWindowHeight, GameWindow.mainWindowWidth, 
                              GameWindow.commandWindowHeight, GameWindow.commandWindowWidth, 
                              GameWindow.hudHeight, GameWindow.inputHeight);

            playerChar.name = GameWindow.ReadLimitedInput(30);
        }
    }
}
