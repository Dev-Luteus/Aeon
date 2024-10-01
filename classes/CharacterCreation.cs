using System.Text;
namespace Aeon.classes
{
    public class CharacterCreation
    {
        public static Player playerChar = new Player();
        public void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            int width = 100;
            int height = 20; 
            int startingRow = 1; // Print below top border
            int cursorPosition = 5; 
            
            // WindowSize + Buffer has to be larger than generated Window.
            Console.SetWindowSize(width+5, height+5);
            Console.SetBufferSize(width+5, height+5);
            
            CreationWindow(height, width); 
            UserInputWindow(height, width);
            
            CenteredConsoleText("This is a test of the console window system", width, ref startingRow);
            CenteredConsoleText("Another test line", width, ref startingRow);
            CenteredConsoleText("A shorter line", width, ref startingRow);
            CenteredConsoleText("Yet another line to test", width, ref startingRow);
            CenteredConsoleText("Yet another line to test test test test test test test test test test", width, ref startingRow);
            
            Console.SetCursorPosition(cursorPosition, height); // Set cursor below game window
            Console.ReadLine(); // Prevent window from closing
        }
        
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