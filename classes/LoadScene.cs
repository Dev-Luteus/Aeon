using System;
using System.Linq;
using System.Text;
using System.Threading; // Don't forget to include this for Thread.Sleep
using System.Diagnostics; // Add this for Stopwatch

namespace Aeon.classes
{
    public class LoadScene
    {
        public void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            MainMenuText();  // Display the Main Menu text first
            LoadingSymbolText();
        }

        static void MainMenuText()
        {
            string mainMenuText = @$"
   ▄██▄      █████▄  ▒█████▒   ██     ██  
 ▄██▀▀██▄   ██▒   ▀ ▒██▒  ██▒  ███▄   ██ 
▒██    ██▒ ▓█████   ▒██░  ██▒ ▓██▒▀█▄ ██▒
░██▀▀▀▀██▒ ▓██░   ▄ ▒██   ██░ ▓██▒  ▀███▒
 ██▓  ▓██▒ ░▒█████▀ ░ ████▓▒░ ▒██░   ▓██░
 ▓▀▒  ▓▒█░ ░░ ▒░ ░░ ▒░▒░▒░ ░ ▒░   ▒ ▒ 
 ▒▒░  ▒▒ ░ ░ ░  ░  ░ ▒ ▒░ ░  ░░   ░ ▒░
  ░   ▒      ░   ░ ░ ░ ▒     ░   ░ ░ 
 ░    ░  ░   ░  ░    ░ ░           ░";
            
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Split to calculate the dimensions
            string[] textLines = mainMenuText.Split('\n');

            int textHeight = textLines.Length;
            int textWidth = textLines.Max(line => line.Length);

            int verticalPosition = Math.Max(0, ((windowHeight / 2) - 5) - (textHeight / 2));
            int horizontalPosition = (windowWidth / 2) - (textWidth / 2);

            Console.SetCursorPosition(0, verticalPosition);

            foreach (string line in textLines)
            {
                // Set cursor to the horizontal center for each line
                Console.SetCursorPosition(horizontalPosition, Console.CursorTop);
                Console.WriteLine(line);
            }
        }

        static void LoadingSymbolText()
        {
            string loadSymbolFrame1 = @"   
    ▀  ▀
 ▀        x
x          x
x          x
 x        x
    x  x";

            string loadSymbolFrame2 = @"   
    x  x
 x        ▀
x          ▀
x          ▀
 x        x
    x  x";

            string loadSymbolFrame3 = @"   
    x  x
 x        x
x          x
x          x
 x        ▀
    ▀  ▀";

            string loadSymbolFrame4 = @"   
    x  x
 x        x
▀          x
▀          x
 ▀        x
    x  x";

            string[] frames = { loadSymbolFrame1, loadSymbolFrame2, loadSymbolFrame3, loadSymbolFrame4 };
            int delay = 200;

            int loadingStartPos = Console.CursorTop + 1;
            loadingStartPos = Math.Min(loadingStartPos, Console.WindowHeight - frames[0].Split('\n').Length);

            int frameWidth = frames.Max(frame => frame.Split('\n').Max(line => line.Length));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (stopwatch.Elapsed.TotalSeconds < 3)
            {
                foreach (string frame in frames)
                {
                    if (stopwatch.Elapsed.TotalSeconds >= 3) break;

                    string[] frameLines = frame.Split('\n');
                    ClearLoadingArea(frameLines.Length);

                    int horizontalPosition = (Console.WindowWidth - frameWidth) / 2;

                    for (int i = 0; i < frameLines.Length; i++)
                    {
                        Console.SetCursorPosition(horizontalPosition, loadingStartPos + i);
                        Console.Write(frameLines[i]);
                    }

                    Thread.Sleep(delay);
                }
            }

            stopwatch.Stop(); Console.WriteLine();
            ClearLoadingArea(frames[0].Split('\n').Length); // Clear the loading symbol after finishing
        }

        static void ClearLoadingArea(int height)
        {
            int currentLineCursor = Console.CursorTop;
            int frameWidth = 20; // Approximate width of the loading symbol
            int horizontalPosition = (Console.WindowWidth - frameWidth) / 2;

            for (int i = 0; i < height; i++)
            {
                if (currentLineCursor + i < Console.WindowHeight)
                {
                    Console.SetCursorPosition(horizontalPosition, currentLineCursor + i);
                    Console.Write(new string(' ', frameWidth));
                }
            }
            Console.SetCursorPosition(horizontalPosition, currentLineCursor);
        }
    }
}