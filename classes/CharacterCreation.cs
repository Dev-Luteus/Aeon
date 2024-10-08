using System.Text;

namespace Aeon.classes
{
    public class CharacterCreation
    {                           
        private Player playerChar;                // Readonly = get (no set)
        private readonly GameWindow gameWindow;   // List to define Valid and Non-Valid class input
        private readonly List<string> validClasses = new List<string> { "crusader", "graverobber", "occultist" };
        
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
                        // -----
                        gameWindow.isTyping = true;
                        
                        // -- Name
                        playerChar.name = gameWindow.ReadLimitedInput(20);
                        
                        // -- Race
                        gameWindow.Story = "Welcome to the Aeon Dungeon character creator. |" +
                               "\x1b[93mEnter Your Race:\x1b[39m"; gameWindow.needsRedraw = true;
                        playerChar.race = gameWindow.ReadLimitedInput(15);
                        
                        // -- Class ( loop for valid input )
                        bool validClassEntered = false;
                        while (!validClassEntered)
                        {
                            gameWindow.Story = "Welcome to the Aeon Dungeon character creator. |" +
                                   "\x1b[93mEnter Your Class (Crusader, Graverobber, or Occultist):\x1b[39m";
                            gameWindow.needsRedraw = true;
                            
                            string className = gameWindow.ReadLimitedInput(15).ToLower();
                            
                            if (validClasses.Contains(className)) { // Try valid, else Catch (loop : retry)
                                try { rpgClasses.ApplyClass(playerChar, className); validClassEntered = true;
                                } catch (ArgumentException) { }
                            }
                        }
                        
                        // -----
                        gameWindow.isTyping = false;                              // Not infinite input
                        
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
        private void CharacterCreationString()
        {
            
        }
    }
}
