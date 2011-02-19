using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GildedRose.Console
{
    public class Inventory
    {

        private IList<Item> Items;

        public Inventory(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void updateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                updateQualityOnItem(Items[i]);
            }
        }

        private void updateQualityOnItem(Item item)
        {
            if (isSulfuras(item))
            {
                subtractSellIn(item);

                return;
            }

            if (isAgedBrie(item))
            {
                updateQualityOfAgedBried(item);

                return;
            }

            if (isBackstagePasses(item))
            {
                updateQualityOfBackstagePasses(item);

                return;
            }

            if (isConjured(item))
            {
                subractQuality(item, 1);
            }

            subractQuality(item, 1);                      

            subtractSellIn(item);

            if (item.SellIn < 0)
            {
                subractQuality(item, 1);
            }
        }

        private void updateQualityOfBackstagePasses(Item item)
        {
            addQuality(item, 1);

            if (item.SellIn < 11)
            {
                addQuality(item, 1);
            }

            if (item.SellIn < 6)
            {
                addQuality(item, 1);
            }

            subtractSellIn(item);

            if (item.SellIn < 0)
            {
                zeroQuality(item);
            }
        }
        private void updateQualityOfAgedBried(Item item)
        {
            addQuality(item, 1);

            subtractSellIn(item);

            if (item.SellIn < 0)
            {
                addQuality(item, 1);
            }
        }

        private bool isConjured(Item item)
        {
            return item.Name == "Conjured Mana Cake";
        }

        private bool isSulfuras(Item item)
        {
            return item.Name == "Sulfuras, Hand of Ragnaros";
        }

        private static bool isBackstagePasses(Item item)
        {
            return item.Name == "Backstage passes to a TAFKAL80ETC concert";
        }

        private static bool isAgedBrie(Item item)
        {
            return item.Name == "Aged Brie";
        }

        private void subtractSellIn(Item item)
        {
            item.SellIn = item.SellIn - 1;
        }

        private void zeroQuality(Item item)
        {
            item.Quality = 0;
        }

        private void subractQuality(Item item, int amount)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - amount;
            }
        }

        private void addQuality(Item item, int amount)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + amount;
            }
        }
    }
}
