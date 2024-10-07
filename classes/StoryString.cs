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
            // Use '|' as a new line marker (instead of '\n')
            GameWindow.Story = $"Welcome, {playerChar.name}!|" +
                               $"You are a {playerChar.race}|" +
                               "To your utter fucking demise.";
        }
    }
}
