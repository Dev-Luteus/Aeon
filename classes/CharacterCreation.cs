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
            Console.SetWindowSize(Console.LargestWindowWidth, GameWindow.totalHeight); // Initialize
            GameWindow.CheckWindowSize();
            
            GameWindow.Story = "Welcome to the Aeon Dungeon character creator. |" +
                               "\x1b[93mEnter Your Name:\x1b[39m";
            
            GameWindow.DrawUI(GameWindow.mainWindowHeight, GameWindow.mainWindowWidth, 
                              GameWindow.commandWindowHeight, GameWindow.commandWindowWidth, 
                              GameWindow.hudHeight, GameWindow.inputHeight);
            
            // Task : asynchronous operation. Run code in parallel with main program.
            Task inputTask = null;
            bool done = false;
            while (!done) // Resizing + Input Handling
            {                                                          // not
                if (Console.WindowHeight != GameWindow.originalWindowHeight || Console.WindowWidth != GameWindow.originalWindowWidth) {
                    
                    bool wasCorrectSize = GameWindow.isCorrectSize;
                    GameWindow.CheckWindowSize();
                    
                    if (!GameWindow.isCorrectSize) {                              // If not
                        GameWindow.isTyping = false;
                        GameWindow.userInput = string.Empty;
                        
                        Console.Clear(); Console.WriteLine("\x1b[3J"); // Won't clear fully otherwise
                    } 
                    else if (!wasCorrectSize) { GameWindow.needsRedraw = true; }  // if not correct size
                }
                                                                       // if not correct size
                if (GameWindow.needsRedraw) {
                    Console.ResetColor();
                    GameWindow.DrawUI(GameWindow.mainWindowHeight, GameWindow.mainWindowWidth, GameWindow.commandWindowHeight, 
                        GameWindow.commandWindowWidth, GameWindow.hudHeight, GameWindow.inputHeight);
                    GameWindow.needsRedraw = false;                               // False after drawing (otherwise infinite)
                }

                if (GameWindow.isCorrectSize && inputTask == null) {              // Allow input
                    inputTask = Task.Run(() => {
                        
                        GameWindow.isTyping = true;
                        Console.ForegroundColor = ConsoleColor.Green; // Green user input text
                        playerChar.name = GameWindow.ReadLimitedInput(30); Console.ResetColor();
                        
                        // -----
                        GameWindow.Story = "Welcome to the Aeon Dungeon character creator. |" +
                               "\x1b[93mEnter Your Race:\x1b[39m";
                        GameWindow.needsRedraw = true;
                        
                        Console.ForegroundColor = ConsoleColor.Green; // Green user input text
                        playerChar.race = GameWindow.ReadLimitedInput(30); Console.ResetColor();
                        
                        // -----
                        
                        
                        GameWindow.isTyping = false;                              // Not infinite input
                        
                        
                        // Process userInput                           :: In User Input Window
                        Console.SetCursorPosition(GameWindow.inputWindowStartX, GameWindow.inputWindowStartY);
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
