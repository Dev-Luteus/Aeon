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
            GameWindow.Story = "Aeon Dungeon - Character Creation: " +
                               "Enter Your Name: ";
            
            GameWindow.DrawUI(GameWindow.mainWindowHeight, GameWindow.mainWindowWidth, 
                              GameWindow.commandWindowHeight, GameWindow.commandWindowWidth, 
                              GameWindow.hudHeight, GameWindow.inputHeight);
            
            playerChar.name = Console.ReadLine();
            
            // // Set cursor position for character creation prompts
            // Console.SetCursorPosition(3, 1);
            // Console.WriteLine("Aeon Dungeon - Character Creation");
            // Console.SetCursorPosition(3, 3);
            // Console.Write("Enter your name: ");
        }
    }
}
