namespace Aeon.classes
{   /* When creating an instance of StoryString, new = StoryString(player gamewindow),
     * the constructor assigns the passed objects (Player and GameWindow) to the private fields:
     * playerChar and gameWindow*/
    public class StoryString
    {
        private Player playerChar;              // Field : Holds Player Object
        private readonly GameWindow gameWindow; // Field Readonly : Holds GameWindow Object, Can only be assigned Once

        // Constructor : Initialize Objects. This runs automatically, when you make a new = StoryString()
        public StoryString(Player player, GameWindow gameWindow)
        {
            playerChar = player; // Save the passed Player instance
            this.gameWindow = gameWindow;
        }

        // Main method to update the GameWindow.Story
        public void Main()
        {
            // Use '|' as a new line marker (instead of '\n')
            gameWindow.Story = $"Welcome, {playerChar.name}! " +
                               $"You're a proud member of the {playerChar.race} race " +
                               $"and also a {playerChar.rpgClass}.|" +
                               "Paramore is a great band";
        }
    }
}
