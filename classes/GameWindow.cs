using System.Text;

namespace Aeon.classes {
    public class GameWindow {
        static Player playerChar = new Player();
        
        // Dimensions
        public static int mainWindowWidth = 120;        public static int mainWindowHeight = 24;
        public static int commandWindowHeight = 16;     public static int commandWindowWidth = 28;
        public static int hudHeight = 2;                public static int inputHeight = 2;
        
        public static int totalWidth                    = mainWindowWidth + commandWindowWidth;
        public static int totalHeight                   = mainWindowHeight + hudHeight + inputHeight;
        public static int originalWindowWidth           = Console.LargestWindowWidth;
        public static int originalWindowHeight          = totalHeight;
        
        // Cursor position (user)
        public static int inputWindowStartX = 5;        public static int inputWindowStartY;
        
        // Input/Redraw logic variables
        public static string userInput       = string.Empty;
        public static bool   isCorrectSize   = false;
        public static bool   isTyping        = false;
        public static bool   needsRedraw     = false;
        public static string Story;
        public GameWindow(Player player) {
            playerChar = player; // Save the passed Player instance
        }
        
        public void Main() {   
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(Console.LargestWindowWidth, totalHeight); // Initialize
            DrawUI(mainWindowHeight, mainWindowWidth, commandWindowHeight,
                   commandWindowWidth, hudHeight, inputHeight);
            CheckWindowSize(); // Check size on startup (not perfect)
            
            /* Known Issues:
             * - 1: If user starts console in a smaller window, they can still type.
             * -- 1:1: This is supposedly harmless */

            // Task : asynchronous operation. Run code in parallel with main program.
            Task inputTask = null;
            
            while (true) // Resizing + Input Handling
            {                                                          // not
                if (Console.WindowHeight != originalWindowHeight || Console.WindowWidth != originalWindowWidth) {
                    
                    bool wasCorrectSize = isCorrectSize;
                    CheckWindowSize();
                    
                    if (!isCorrectSize) {                              // If not
                        isTyping = false;
                        userInput = string.Empty;
                        
                        Console.Clear(); Console.WriteLine("\x1b[3J"); // Won't clear fully otherwise
                    } 
                    else if (!wasCorrectSize) { needsRedraw = true; }  // if not correct size
                }
                                                                       // if not correct size
                if (needsRedraw) {
                    
                    DrawUI(mainWindowHeight, mainWindowWidth, commandWindowHeight, commandWindowWidth, hudHeight, inputHeight);
                    needsRedraw = false;                               // False after drawing (otherwise infinite)
                }

                if (isCorrectSize && inputTask == null) {              // Allow input
                    inputTask = Task.Run(() => {
                        
                        isTyping = true;
                        Console.ForegroundColor = ConsoleColor.Green; // Green user input text
                        userInput = ReadLimitedInput(30); Console.ResetColor();
                        isTyping = false;                              // Not infinite input
                        
                        // Process userInput                           :: In User Input Window
                        Console.SetCursorPosition(inputWindowStartX, inputWindowStartY); 
                    });
                }

                if (inputTask != null && inputTask.IsCompleted)
                {
                    inputTask = null;
                }

                Thread.Sleep(100); // Reduce CPU usage with a delay
            }
        }
        
        // Monitor Screen Size Changes
        public static void CheckWindowSize() {
            isCorrectSize = (Console.WindowHeight >= totalHeight && Console.WindowWidth >= totalWidth);
        }
        
        /* I want to limit the amount of characters a user can type, BEFORE a message is sent.
         * This is to ensure that the User does not exceed the UserInputWindow, and doesn't overwrite its borders.
         * Since a String is actually an array of characters, a string builder can limit the size of that array.*/
        public static string ReadLimitedInput(int maxChars)
        {
            StringBuilder input = new StringBuilder();
            Console.SetCursorPosition(inputWindowStartX, inputWindowStartY);
            
            while (isTyping && isCorrectSize)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                    if (keyInfo.Key == ConsoleKey.Enter) 
                    {
                        // Process input       : (e.g., add to a list of messages)
                        // For now             : clear input
                        for (int i = 0; i < input.Length; i++) { Console.Write("\b \b"); }
                        
                        string result = input.ToString();
                        input.Clear();
                        return result;
                    }
                    else if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input.Remove(input.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                    else if (input.Length < maxChars && !char.IsControl(keyInfo.KeyChar))
                    {
                        input.Append(keyInfo.KeyChar);
                        Console.Write(keyInfo.KeyChar);
                    }
                }
                else
                {
                    Thread.Sleep(50);
                }
            }
            return input.ToString();
        }
        
        public static void DrawUI(int mainWindowHeight, int mainWindowWidth, int commandWindowHeight, int commandWindowWidth,
                      int hudHeight, int inputHeight) {
            
            Console.Clear(); Console.WriteLine("\x1b[3J"); // Clear old UI (x1b clears whole)
            
            // Functions
            MainWindow(mainWindowHeight, mainWindowWidth);
            PlayerHUD(mainWindowHeight, mainWindowWidth, hudHeight);  // Below Main
            UserInputWindow(mainWindowHeight + hudHeight, mainWindowWidth, inputHeight); // Below HUD
            CommandWindow(commandWindowHeight, commandWindowWidth);

            // Initialize variables for MainWindowDialogue and Commands
            int startingRow = 1;
            int commandRow = 1; 
            
            MainWindowDialogue(mainWindowHeight, mainWindowWidth, ref startingRow);
            Commands(commandWindowHeight, commandWindowWidth, mainWindowWidth, ref commandRow);
            
            inputWindowStartY = mainWindowHeight + hudHeight;
            Console.SetCursorPosition(inputWindowStartX, inputWindowStartY);
        }
        
        // Main Window Story Text Function ------------
        public static void MainWindowDialogue(int mainWindowHeight, int mainWindowWidth, ref int startingRow)
        {
            MainWindowDialogueManager(Story, mainWindowWidth, ref startingRow);
        }
        public static void ClearGameWindow() {  }
        
        // Main Window Story Text Manager Function ------------
        public static void MainWindowDialogueManager(string story, int maxWidth, ref int startingRow)
        {
            int maxLineWidth = maxWidth - 4;
            
            // Split the story string based on the '|' marker (make a new line inside other classes)
            string[] lines = story.Split('|');

            foreach (string line in lines)
            {
                string[] words = line.Split(' '); // Split into words
                StringBuilder currentLine = new StringBuilder();

                foreach (var word in words)
                {
                    if (currentLine.Length + word.Length + 1 > maxLineWidth)
                    {
                        // Print the current line and reset
                        MainWindowTextRegular(currentLine.ToString(), maxWidth, ref startingRow);
                        currentLine.Clear();
                    }

                    if (currentLine.Length > 0)
                    {
                        currentLine.Append(" "); // Space before word, if not the first word
                    }

                    currentLine.Append(word); // Append the word to the current line
                }

                if (currentLine.Length > 0)
                {
                    // Print the remaining text in the current line
                    MainWindowTextRegular(currentLine.ToString(), maxWidth, ref startingRow);
                }
            }
        }


        // All the Commands in our CommandWindow()
        public static void Commands(int commandWindowHeight, int commandWindowWidth, int mainWindowWidth, ref int commandRow)
        {
            int commandWindowXPosition = mainWindowWidth + 1; // Adjust
            CommandWindowText("Command 1: Start Game", commandWindowXPosition, ref commandRow);
            CommandWindowText("Command 2: Load Game", commandWindowXPosition, ref commandRow);
            CommandWindowText("Command 3: Settings", commandWindowXPosition, ref commandRow);
            CommandWindowText("Command 4: Exit", commandWindowXPosition, ref commandRow);
        }
        
        // Main Window ------------
        public static void MainWindow(int height, int width)
        {
            Console.SetCursorPosition(0, 0);
            Console.OutputEncoding = Encoding.UTF8;
            
            // Top Border
            Console.Write("◼");
            Console.Write(new string('─', width - 2));
            Console.Write("◼");

            // Left and Right Borders
            for (int i = 1; i < height - 1; i++) {
                Console.SetCursorPosition(0, i);
                Console.Write("│");
                Console.SetCursorPosition(width - 1, i);
                Console.Write("│");
            }

            // Bottom Border
            Console.SetCursorPosition(0, height - 1);
            Console.Write("◼");
            Console.Write(new string('─', width - 2));
            Console.Write("◼");
        }

        static void PlayerHUD(int startY, int width, int hudHeight)
        {
            for (int i = 0; i < hudHeight; i++)
            {
                Console.SetCursorPosition(0, startY + i);
                Console.Write("│");
                //Console.Write(playerHealth) etc?
                Console.SetCursorPosition(width - 1, startY + i);
                Console.Write("│");
            }

            // Bottom Border of PlayerHUD
            Console.SetCursorPosition(0, startY + hudHeight - 1);
            Console.Write("◼");
            Console.Write(new string('─', width - 2));
            Console.Write("◼");
        }

        // Input Window ------------
        public static void UserInputWindow(int startY, int width, int inputHeight)
        {
            for (int i = 0; i < inputHeight; i++)
            {
                Console.SetCursorPosition(0, startY + i);
                Console.Write("│ {:");
                Console.SetCursorPosition(width - 4, startY + i);
                Console.Write(":} │");
            }

            // Bottom Border
            Console.SetCursorPosition(0, startY + inputHeight - 1);
            Console.Write("◼");
            Console.Write(new string('─', width - 2));
            Console.Write("◼");
        }

        // Command List Window ------------
        public static void CommandWindow(int height, int width)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int commandWindowXPosition = 121; // Set X position for command window

            // Top Border
            Console.SetCursorPosition(commandWindowXPosition, 0); // Set cursor position for command window
            Console.Write("◼");
            Console.Write(new string('─', width - 2));
            Console.Write("◼");

            // Left and Right Borders
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(commandWindowXPosition, i);
                Console.Write("│ ➤ ");
                Console.SetCursorPosition(commandWindowXPosition + width - 1, i);
                Console.Write("│");
            }

            // Bottom Border
            Console.SetCursorPosition(commandWindowXPosition, height - 1);
            Console.Write("◼");
            Console.Write(new string('─', width - 2));
            Console.Write("◼");
        }

        // Display Text Function ------------
        // Center Console Text ------------
        public static void MainWindowTextRegular(string text, int width, ref int row)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int maxWidth = width - 2;
            
            // Truncate text if too long
            if (text.Length > maxWidth) { text = text.Substring(0, maxWidth); }

            // Center text:                                            // int leftPadding = (width - text.Length) / 2;
            int leftPadding = 3; if (leftPadding < 0) leftPadding = 0; // Ensure within bounds

            // Check if current row within area
            if (row > 0 && row < Console.WindowHeight - 1)
            {
                Console.SetCursorPosition(leftPadding, row);
                Console.Write(text);
                row++;  // New line
            }
        }

        // Command Window Text ------------
        public static void CommandWindowText(string text, int commandWindowXPosition, ref int row)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int maxWidth = 28 - 7;  // 28 == Window width  :  7 == '│ ➤ ' + border

            // Truncate if too long
            if (text.Length > maxWidth) { text = text.Substring(0, maxWidth); }
            
            Console.SetCursorPosition(commandWindowXPosition + 5, row); // Start after arrow (5 spaces)
            Console.Write(text); // Display
            row++;               // New line
        }
    }
}