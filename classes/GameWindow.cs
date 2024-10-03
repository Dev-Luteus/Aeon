using System.Text;

namespace Aeon.classes {
    public class GameWindow {
        // Dimensions
        static int mainWindowWidth = 120;        static int mainWindowHeight = 24;
        static int commandWindowHeight = 16;     static int commandWindowWidth = 28;
        static int hudHeight = 2;                static int inputHeight = 2;
        
        static int totalWidth                    = mainWindowWidth + commandWindowWidth;
        static int totalHeight                   = mainWindowHeight + hudHeight + inputHeight;
        static int originalWindowWidth           = Console.LargestWindowWidth;
        static int originalWindowHeight          = totalHeight;
        
        // Cursor position (user)
        static int inputWindowStartX = 5;         static int inputWindowStartY;
        
        // Input/Redraw logic variables
        static string userInput = string.Empty;
        static bool isCorrectSize = false;
        static bool isTyping = false;
        static bool needsRedraw = false;

        public void Main() {   
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetWindowSize(Console.LargestWindowWidth, totalHeight); // Initialize
            DrawUI(mainWindowHeight, mainWindowWidth, commandWindowHeight,
                   commandWindowWidth, hudHeight, inputHeight);
            
            CheckWindowSize();

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
                        userInput = ReadLimitedInput(30);
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
        
        private void CheckWindowSize() {
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
                    if (keyInfo.Key == ConsoleKey.Enter)                        // Press Enter : Main --> { 1 }
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
        
        private void DrawUI(int mainWindowHeight, int mainWindowWidth, int commandWindowHeight, int commandWindowWidth,
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
            
            // int playerCursorPositionX = 5; 
            // int playerCursorPositionY = mainWindowHeight + hudHeight; 
            // Console.SetCursorPosition(playerCursorPositionX, playerCursorPositionY);
            
            inputWindowStartY = mainWindowHeight + hudHeight;
            Console.SetCursorPosition(inputWindowStartX, inputWindowStartY);
        }
        
        // Main Window Story Text Function ------------
        public static void MainWindowDialogue(int mainWindowHeight, int mainWindowWidth, ref int startingRow)
        {
            string story = "This is a test of the console window system. " +
                           "Another test line. A shorter line. Yet another line to test. " +
                           "Yet another line to test test test test test test test test test test. " +
                           "This is to ensure that the User does not exceed the UserInputWindow, " +
                           "and doesn't overwrite its borders. A long sentence to test the wrapping functionality." +
                           "peepeepoopooo I need more text to make sure that . . . the string can mess up my padding " +
                           "does that work? w h a t a b o u t t h i s ? Seems my padding is invincible. ";
            
            // Display
            MainWindowDialogueManager(story, mainWindowWidth, ref startingRow);
        }

        // Main Window Story Text Manager Function ------------
        public static void MainWindowDialogueManager(string story, int maxWidth, ref int startingRow)
        {
            int maxLineWidth = maxWidth - 4;
            string[] words = story.Split(' '); // Split into words
            StringBuilder currentLine = new StringBuilder();

            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + 1 > maxLineWidth) 
                {
                    // Print line, reset
                    MainWindowTextRegular(currentLine.ToString(), maxWidth, ref startingRow);
                    currentLine.Clear();
                }
                if (currentLine.Length > 0)
                {
                    currentLine.Append(" "); // Space before word, if not first word
                } 
                currentLine.Append(word);    // Append word to current line
            } 
            if (currentLine.Length > 0)      // Print remaining in currentLine
            {
                MainWindowTextRegular(currentLine.ToString(), maxWidth, ref startingRow);
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
        static void MainWindow(int height, int width)
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
        static void UserInputWindow(int startY, int width, int inputHeight)
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
        static void CommandWindow(int height, int width)
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
        static void MainWindowTextRegular(string text, int width, ref int row)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int maxWidth = width - 2;
            
            // Truncate text if too long
            if (text.Length > maxWidth) { text = text.Substring(0, maxWidth); }

            // Calculate, center text //int leftPadding = (width - text.Length) / 2;
            
            int leftPadding = 3;
            if (leftPadding < 0) leftPadding = 0; // Ensure within bounds

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