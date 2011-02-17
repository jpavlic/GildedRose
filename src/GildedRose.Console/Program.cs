using System.Collections.Generic;
using System;

namespace GildedRose.Console
{
    class Program
    {
        
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            IList<Item> Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          };

            Inventory inventory = new Inventory(Items);
            for (int i = 0; i < 20; i++)
            {
                inventory.updateQuality();
            }

            System.Console.ReadKey();

        }

    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }

        public override string ToString()
        {
            return String.Format("Name = {0} SellIn = {1} Quality = {2}", Name, SellIn, Quality);
        }
    }

}
