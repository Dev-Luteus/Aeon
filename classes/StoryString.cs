namespace Aeon.classes
{
    public class StoryString
    {
        private Player playerChar;
        private readonly GameWindow gameWindow;

        // Constructor
        public StoryString(Player player, GameWindow gameWindow)
        {
            playerChar = player; // Save the passed Player instance
            this.gameWindow = gameWindow;
        }

        // Main method to update the GameWindow.Story
        public void Main()
        {
            // Use '|' as a new line marker (instead of '\n')
            gameWindow.Story = $"Welcome, {playerChar.name}!|" +
                               $"You are a {playerChar.race}|" +
                               "To your utter fucking demise.";
        }
    }
}
