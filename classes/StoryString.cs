namespace Aeon.classes
{
    public class StoryString
    {
        private Player playerChar;

        // Constructor
        public StoryString(Player player)
        {
            playerChar = player; // Save the passed Player instance
        }

        // Main method to update the GameWindow.Story
        public void Main()
        {
            GameWindow.Story = "This is a test of the console window system. " +
                               "Another test line. A shorter line. Yet another line to test. " +
                               "Yet another line to test test test test test test test test test test. " +
                               "This is to ensure that the User does not exceed the UserInputWindow, " +
                               "and doesn't overwrite its borders. A long sentence to test the wrapping functionality." +
                               "peepeepoopooo I need more text to make sure that . . . the string can mess up my padding " +
                               "does that work? w h a t a b o u t t h i s ? Seems my padding is invincible. " +
                               $"Welcome, {playerChar.name}!";
        }
    }
}
