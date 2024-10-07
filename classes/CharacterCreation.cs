using System.Net;
using System.Text;

namespace Aeon.classes
{
    public class CharacterCreation
    {
        private Player playerChar;
        private readonly GameWindow gameWindow; // Readonly = get (no set)

        // Constructor to accept the Player instance
        public CharacterCreation(Player player, GameWindow gameWindow)
        {
            playerChar = player; // Save the passed Player instance
            this.gameWindow = gameWindow;
        }
        public void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(Console.LargestWindowWidth, gameWindow.totalHeight); // Initialize
            gameWindow.CheckWindowSize();
            
            gameWindow.Story = "Welcome to the Aeon Dungeon character creator. |" +
                               "\x1b[93mEnter Your Name:\x1b[39m";
            
            gameWindow.DrawUI(gameWindow.mainWindowHeight, gameWindow.mainWindowWidth, 
                              gameWindow.commandWindowHeight, gameWindow.commandWindowWidth, 
                              gameWindow.hudHeight, gameWindow.inputHeight);
            
            // Task : asynchronous operation. Run code in parallel with main program.
            Task inputTask = null;
            bool done = false;
            while (!done) // Resizing + Input Handling
            {                                                          // not
                if (Console.WindowHeight != gameWindow.originalWindowHeight || Console.WindowWidth != gameWindow.originalWindowWidth) {
                    
                    bool wasCorrectSize = gameWindow.isCorrectSize;
                    gameWindow.CheckWindowSize();
                    
                    if (!gameWindow.isCorrectSize) {                              // If not
                        gameWindow.isTyping = false;
                        gameWindow.userInput = string.Empty;
                        
                        Console.Clear(); Console.WriteLine("\x1b[3J"); // Won't clear fully otherwise
                    } 
                    else if (!wasCorrectSize) { gameWindow.needsRedraw = true; }  // if not correct size
                }
                                                                       // if not correct size
                if (gameWindow.needsRedraw) {
                    Console.ResetColor();
                    gameWindow.DrawUI(gameWindow.mainWindowHeight, gameWindow.mainWindowWidth, gameWindow.commandWindowHeight, 
                        gameWindow.commandWindowWidth, gameWindow.hudHeight, gameWindow.inputHeight);
                    gameWindow.needsRedraw = false;                               // False after drawing (otherwise infinite)
                    Console.ForegroundColor = ConsoleColor.Green;               // rest of inputs ( very stupid )
                }

                if (gameWindow.isCorrectSize && inputTask == null) {              // Allow input
                    inputTask = Task.Run(() => {
                        
                        Console.ForegroundColor = ConsoleColor.Green;           // Initial        ( very stupid )
                        gameWindow.isTyping = true;
                        // -----
                        playerChar.name = gameWindow.ReadLimitedInput(30);
                    
                        gameWindow.Story = "Welcome to the Aeon Dungeon character creator. |" +
                               "\x1b[93mEnter Your Race:\x1b[39m";
                        gameWindow.needsRedraw = true;
                        
                        playerChar.race = gameWindow.ReadLimitedInput(30);
                        
                        // -----
                        gameWindow.isTyping = false;                              // Not infinite input
                        
                        
                        // Process userInput                           :: In User Input Window
                        Console.SetCursorPosition(gameWindow.inputWindowStartX, gameWindow.inputWindowStartY);
                        done = true;
                    });
                }
                if (inputTask != null && inputTask.IsCompleted)
                {
                    inputTask = null;
                }
                Thread.Sleep(100); // Reduce CPU usage with a delay
            }
        }
    }
}
