using System.Text;

namespace Aeon.classes
{
    public class CharacterCreation
    {
        public static Player playerChar = new Player();

        public void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            int mainWindowWidth = 110;
            int mainWindowHeight = 20;
            int commandWindowHeight = 20;
            int commandWindowWidth = 30;
            int totalWindowWidth = mainWindowWidth + commandWindowWidth + 5;
            int totalWindowHeight = mainWindowHeight + commandWindowHeight + 5;

            int startingRow = 1; // Print below top border
            int cursorPositionX = 5; // Makes sense
            int cursorPositionY = 3; // Is completely incomprehensible. Why? Wtf, help
            int commandWindowXPosition = mainWindowWidth + 5; // X position for command window

            Console.SetWindowSize(totalWindowWidth, totalWindowHeight);
            Console.SetBufferSize(totalWindowWidth, totalWindowHeight);
            
            CreationWindow(mainWindowHeight, mainWindowWidth); 
            UserInputWindow(mainWindowHeight, mainWindowWidth);
            CreationCommandWindow(commandWindowHeight, commandWindowWidth);
            
            CenteredConsoleText("This is a test of the console window system", mainWindowWidth, ref startingRow);
            CenteredConsoleText("Another test line", mainWindowWidth, ref startingRow);
            CenteredConsoleText("A shorter line", mainWindowWidth, ref startingRow);
            CenteredConsoleText("Yet another line to test", mainWindowWidth, ref startingRow);
            CenteredConsoleText("Yet another line to test test test test test test test test test test", mainWindowWidth, ref startingRow);
            
            Console.SetCursorPosition(cursorPositionX, cursorPositionY); // Set cursor below game window
            Console.ReadLine(); // Prevent window from closing
        }
        // Main Window ------------
        static void CreationWindow(int height, int width)
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
        // Input Window ------------
        static void UserInputWindow(int height, int width)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int inputHeight = 2; 

            // Left, right
            for (int i = 0; i < inputHeight; i++)
            {
                Console.SetCursorPosition(0, height + i);
                Console.Write("│ {:");
                Console.SetCursorPosition(width - 4, height + i);
                Console.Write(":} │");
            }

            // Bottom Border
            Console.SetCursorPosition(0, height + inputHeight - 1);
            Console.Write("◼");
            Console.Write(new string('─', width - 2));
            Console.Write("◼");
        }
        // Clamp to - Input Window ------------

        // Command List Window ------------
        static void CreationCommandWindow(int height, int width)
        {
            Console.OutputEncoding = Encoding.UTF8;
            int commandWindowXPosition = 111; // Set X position for command window
            
            // Top Border
            Console.SetCursorPosition(commandWindowXPosition, 0); // Set cursor position for command window
            Console.Write("◼");
            Console.Write(new string('─', width - 2));
            Console.Write("◼");

            // Left and Right Borders
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(commandWindowXPosition, i);
                Console.Write("│");
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
