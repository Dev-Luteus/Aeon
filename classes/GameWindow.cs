using System.Text;

namespace Aeon.classes
{
    public class GameWindow
    {
        public void Main()
        {
            // ---------------- Main Section ----------------
            Console.OutputEncoding = Encoding.UTF8;
            int mainWindowWidth = 120;
            int mainWindowHeight = 20;
            int commandWindowHeight = 20;
            int commandWindowWidth = 28;
            int hudHeight = 2;
            int inputHeight = 2;
            
            int playerCursorPositionX = 5;    // Makes sense
            int playerCursorPositionY = mainWindowHeight + hudHeight; // Set cursor below game window and HUD
            
            MainWindow(mainWindowHeight, mainWindowWidth); 
            PlayerHUD(mainWindowHeight, mainWindowWidth, hudHeight);  // Below Main
            UserInputWindow(mainWindowHeight + hudHeight, mainWindowWidth, inputHeight); // Below HUD
            CommandWindow(commandWindowHeight, commandWindowWidth);
            
            // ---------------- Text Section ----------------
            int startingRow = 1;        // Print consoleText below top border
            
            CenteredConsoleText("This is a test of the console window system", mainWindowWidth, ref startingRow);
            CenteredConsoleText("Another test line", mainWindowWidth, ref startingRow);
            CenteredConsoleText("A shorter line", mainWindowWidth, ref startingRow);
            CenteredConsoleText("Yet another line to test", mainWindowWidth, ref startingRow);
            CenteredConsoleText("Yet another line to test test test test test test test test test test", mainWindowWidth, ref startingRow);
            
            // Set playerCursor inside userInput window
            Console.SetCursorPosition(playerCursorPositionX, playerCursorPositionY);

            Console.ForegroundColor = ConsoleColor.Green;
            string userInput = ReadLimitedInput(20); // Limit : 20 char
        }
        
        /* I want to limit the amount of characters a user can type, BEFORE a message is sent.
         * This is to ensure that the User does not exceed the UserInputWindow, and doesn't overwrite its borders.
         * Since a String is actually an array of characters, a string builder can limit the size of that array.*/
        public static string ReadLimitedInput(int maxChars)
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true); // Intercept: Read but don't display
                if (keyInfo.Key == ConsoleKey.Enter) {                     // Enter = command sent
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && input.Length > 0) // Handle backspace
                {
                    input.Remove(input.Length - 1, 1);
                    Console.Write("\b \b"); // Remove character from console
                }
                else if (input.Length < maxChars && !char.IsControl(keyInfo.KeyChar)) // Limit characters and ignore control characters
                {
                    input.Append(keyInfo.KeyChar);
                    Console.Write(keyInfo.KeyChar); // Echo character to console
                }
            }
            return input.ToString();
        }

        // Main Window ------------
        static void MainWindow(int height, int width)
        {
            Console.OutputEncoding = Encoding.UTF8;
            // Top Border
            Console.SetCursorPosition(0, 0);
            Console.Write("◼");
            Console.Write(new string('─', width - 2));
            Console.Write("◼");

            // Left and Right Borders
            for (int i = 1; i < height - 1; i++)
            {
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
        
        // Center Console Text ------------
        static void CenteredConsoleText(string text, int width, ref int row)
        {
            int maxWidth = width - 2;

            // Truncate text if too long
            if (text.Length > maxWidth) { text = text.Substring(0, maxWidth); }

            // Calculate, center text
            int leftPadding = (width - text.Length) / 2;

            // Ensure within bounds
            if (leftPadding < 0) leftPadding = 0;

            // Check if current row within area
            if (row > 0 && row < Console.WindowHeight - 1)
            {
                Console.SetCursorPosition(leftPadding, row);
                Console.Write(text);
                row++;  // New line
            }
        }
    }
}
