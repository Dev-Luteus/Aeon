using System.Text;

namespace Aeon.classes;

public class CharacterCreation {
    private Player playerChar;

    // Constructor to accept the Player instance
    public CharacterCreation(Player player) {
        playerChar = player; // Save the passed Player instance
    }
    public void Main() {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Aeon Dungeon");
        Console.WriteLine("Enter your name: ");
        playerChar.name = Console.ReadLine(); // Modify the same instance
    }
}
