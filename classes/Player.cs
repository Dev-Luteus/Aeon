namespace Aeon.classes;

public class Player
{
    public string name           { get; set; }
    public string race           { get; set; }
    public string rpgClass       { get; set; }
    public int health            { get; set; }
    public int damage            { get; set; }
    public int armor             { get; set; }
    public int potions           { get; set; }
    public int coins             { get; set; }

    // Constructor
//     public Player(string name, string race, string rpgClass, int health, int damage, int armor, int potions, int coins)
//     {
//         this.name = name;          this.race = race;        this.rpgClass = rpgClass; 
//         this.health = health;      this.damage = damage;    this.armor = armor; 
//         this.potions = potions;    this.coins = coins;
//     }
// }
//
// // " : "      -   Allows 'derived' class to reference a Constructor of the 'base' class (parent)
// // " base "   -   Reference 'base' class (parent).
//
// public class Human : Player {
//     public Human(string name, string race, string rpgClass, int health, int damage, int armor, int potions, int coins) 
//         : base(name, "Human", rpgClass, health, damage, armor, potions, coins) {
//     }
// }
// public class Orc : Player {
//     public Orc(string name, string race, string rpgClass, int health, int damage, int armor, int potions, int coins) 
//         : base(name, "Orc", rpgClass, health, damage, armor, potions, coins) {
//     }
}