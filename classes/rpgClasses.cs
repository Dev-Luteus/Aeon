namespace Aeon.classes
{
    public class rpgClasses
    {
        public static void ApplyClass(Player player, string className)
        {
            switch (className.ToLower())
            {
                case "crusader":
                    player.rpgClass = "Crusader";
                    player.health = 100;
                    player.armor = 20;
                    player.damage = 15;
                    player.potions = 2;
                    player.coins = 50;
                    break;
                case "graverobber":
                    player.rpgClass = "Graverobber";
                    player.health = 80;
                    player.armor = 10;
                    player.damage = 20;
                    player.potions = 3;
                    player.coins = 100;
                    break;
                case "occultist":
                    player.rpgClass = "Occultist";
                    player.health = 70;
                    player.armor = 5;
                    player.damage = 25;
                    player.potions = 4;
                    player.coins = 75;
                    break;
                default:
                    throw new ArgumentException("Invalid class name", nameof(className));
            }
        }
    }
}