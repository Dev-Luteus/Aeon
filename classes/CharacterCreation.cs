namespace Aeon.classes
{
    public class CharacterCreation
    {
        public static Player playerChar = new Player();

        public void Main()
        {
            int width = 140; // Width of the console window
            int height = 20; // Height of the console window
            int startingRow = 1; // Start printing from the row below the top border
            
            // Set console window and buffer size
            Console.SetWindowSize(width+2, height+2);
            Console.SetBufferSize(width+2, height+2);
            CreationWindow(height, width); 
            
            // Print centered text lines
            CenteredConsoleText("This is a test of the console window system", width, ref startingRow);
            CenteredConsoleText("Another test line", width, ref startingRow);
            CenteredConsoleText("A shorter line", width, ref startingRow);
            CenteredConsoleText("Yet another line to test", width, ref startingRow);
            CenteredConsoleText("Yet another line to test test test test test test test test test test", width, ref startingRow);
            
            Console.SetCursorPosition(0, height); // Set cursor below game window
            Console.ReadKey(); // Prevent window from closing
        }

        // Draw the game window with borders
        static void CreationWindow(int height, int width)
        {
            // Top Border
            Console.SetCursorPosition(0, 0);
            Console.Write("+");
            Console.Write(new string('-', width - 2));
            Console.Write("+");

            // Left and Right Borders
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("|");
                Console.SetCursorPosition(width - 1, i);
                Console.Write("|");
            }

            // Bottom Border
            Console.SetCursorPosition(0, height - 1);
            Console.Write("+");
            Console.Write(new string('-', width - 2));
            Console.Write("+");
        }

        // Center text horizontally and move to the next line
        static void CenteredConsoleText(string text, int width, ref int row)
        {
            int maxWidth = width - 2; // Account for borders
            
            // Truncate if the text is too long
            if (text.Length > maxWidth)
            {
                text = text.Substring(0, maxWidth);
            }

            // Calculate left padding to center the text
            int leftPadding = (width - text.Length) / 2;

            // Ensure leftPadding is within bounds
            if (leftPadding < 0) leftPadding = 0;
            
            // Check if the current row is within the printable area
            if (row > 0 && row < Console.WindowHeight - 1)
            {
                Console.SetCursorPosition(leftPadding, row); // Set cursor position
                Console.Write(text); // Print the text
                row++;  // Move to the next row
            }
        }
    }
}