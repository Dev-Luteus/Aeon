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
                    player.health   = 100;
                    player.armor    = 30; 
                    player.damage   = 20;
                    player.potions  = 2;
                    player.coins    = 50;
                    break;
                case "graverobber":
                    player.rpgClass = "Graverobber";
                    player.health   = 100;
                    player.armor    = 20;
                    player.damage   = 30;
                    player.potions  = 2;
                    player.coins    = 50;
                    break;
                case "occultist":
                    player.rpgClass = "Occultist";
                    player.health   = 100;
                    player.armor    = 10;
                    player.damage   = 40;
                    player.potions  = 2;
                    player.coins    = 50;
                    break;
                default:
                    throw new ArgumentException("Invalid class name", nameof(className));
            }
        }
    }
}