using System.Text;

namespace Aeon.classes
{
    public class CharacterCreation
    {                           
        private Player playerChar;                // Readonly = get (no set)
        private readonly GameWindow gameWindow;   // List to define Valid and Non-Valid class input
        private readonly List<string> validClasses = new List<string> { "crusader", "graverobber", "occultist" };
        bool done = false; 
        
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
            Introduction(); // Before Draw
            
            gameWindow.DrawUI(gameWindow.mainWindowHeight, gameWindow.mainWindowWidth, 
                              gameWindow.commandWindowHeight, gameWindow.commandWindowWidth, 
                              gameWindow.hudHeight, gameWindow.inputHeight);
            
            // Task : asynchronous operation. Run code in parallel with main program.
            Task inputTask = null;
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
                    Console.ForegroundColor = ConsoleColor.Green;               // rest of inputs ( very stupid )
                    gameWindow.needsRedraw = false;                               // False after drawing (otherwise infinite)
                }

                if (gameWindow.isCorrectSize && inputTask == null) {              // Allow input
                    inputTask = Task.Run(() => {
                        
                        Console.ForegroundColor = ConsoleColor.Green;           // Initial        ( very stupid )
                        gameWindow.isTyping = true;
                        
                        AssignName();
                        AssignRace();
                        AssignRPGClass();
                        
                        gameWindow.isTyping = false;
                        Console.SetCursorPosition(gameWindow.inputWindowStartX, gameWindow.inputWindowStartY);
                    });
                }
                if (inputTask != null && inputTask.IsCompleted)
                {
                    inputTask = null;
                }
                Thread.Sleep(100); // Reduce CPU usage with a delay
            }
        }
        private void Introduction() {
            gameWindow.Story = "";
        }
        private void AssignName() {
            gameWindow.Story = "Welcome to the Aeon Dungeon character creator. |" +
               "\x1b[93mEnter Your Name:\x1b[39m"; 
            
            Console.Clear(); Console.WriteLine("\x1b[3J");
            Console.ResetColor();
            gameWindow.DrawUI(gameWindow.mainWindowHeight, gameWindow.mainWindowWidth, gameWindow.commandWindowHeight, 
                        gameWindow.commandWindowWidth, gameWindow.hudHeight, gameWindow.inputHeight);
            playerChar.name = gameWindow.ReadLimitedInput(20);
        }

        private void AssignRace() {
            gameWindow.Story = "Welcome to the Aeon Dungeon character creator. |" +
                               "\x1b[93mEnter Your Race:\x1b[39m"; 
            
            Console.Clear(); Console.WriteLine("\x1b[3J");
            Console.ResetColor();
            gameWindow.DrawUI(gameWindow.mainWindowHeight, gameWindow.mainWindowWidth, gameWindow.commandWindowHeight, 
                        gameWindow.commandWindowWidth, gameWindow.hudHeight, gameWindow.inputHeight);
            playerChar.race = gameWindow.ReadLimitedInput(20);
        }
        private void AssignRPGClass() {
            // loop for valid input 
            bool validClassEntered = false;
            while (!validClassEntered)
            {
                gameWindow.Story = "Welcome to the Aeon Dungeon character creator. |" +
                       "\x1b[93mEnter Your Class (Crusader, Graverobber, or Occultist):\x1b[39m";
                Console.Clear(); Console.WriteLine("\x1b[3J");
                Console.ResetColor();
                gameWindow.DrawUI(gameWindow.mainWindowHeight, gameWindow.mainWindowWidth, gameWindow.commandWindowHeight, 
                            gameWindow.commandWindowWidth, gameWindow.hudHeight, gameWindow.inputHeight);
                
                string className = gameWindow.ReadLimitedInput(15).ToLower();
                
                if (validClasses.Contains(className)) { // Try valid, else Catch (loop : retry)
                    try { rpgClasses.ApplyClass(playerChar, className); validClassEntered = true;
                        done = true;
                    } catch (ArgumentException) { }
                }
            }
        }
    }
}
