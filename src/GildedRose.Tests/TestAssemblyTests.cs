using NUnit.Framework;
using System.Collections.Generic;

using GildedRose.Console;

namespace GildedRose.Tests
{
    [TestFixture]
    public class TestAssemblyTests
    {
        IList<Item> Items;

        Inventory inventory;

        [SetUp]
        public void Setup()
        {
            Items = new List<Item>
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

            inventory = new Inventory(Items);
        }

        private void updateInventory(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                inventory.updateQuality();
            }
        }

        private Item findItemByName(string name)
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name == name)
                {
                    return Items[i];
                }
            }

            return null;
        }

        [Test]
        public void QualityOfAnItemIsNeverNegative()
        {
            updateInventory(100);

            for (var i = 0; i < Items.Count; i++)
            {
                Assert.GreaterOrEqual(Items[i].Quality, 0, "Failed item: " + Items[i]);
            }
        }

        [Test]
        public void QualityIsNeverGreaterThanFifty()
        {
            updateInventory(100);

            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Assert.GreaterOrEqual(50, Items[i].Quality, "Failed item: " + Items[i]);
                }                
            }
        }

        [Test]
        public void OnceSellByDateIsPassedQualityDegradesTwiceAsFast()
        {            
            for (var i = 0; i < 100; i++)
            {
                IList<int> quality = new List<int>();

                for (var j = 0; j < Items.Count; j++)
                {                    
                    quality.Add(Items[j].Quality);
                }

                inventory.updateQuality();

                for (var j = 0; j < Items.Count; j++)
                {
                    if (Items[j].SellIn < 0)
                    {
                        if ((Items[j].Name != "Aged Brie") && (Items[j].Name != "Backstage passes to a TAFKAL80ETC concert") && (Items[j].Name != "Sulfuras, Hand of Ragnaros"))
                        {
                            int toCompare = quality[j] - 2;
                            if (toCompare < 0)
                            {
                                toCompare = 0;
                            }
                            Assert.AreEqual(toCompare, Items[j].Quality, "Failed item: " + Items[j]);
                        }
                    }
                }
            }
        }

        [Test]
        public void SulfrasNeverDecreasesInQuality()
        {
            Item sulfras = findItemByName("Sulfuras, Hand of Ragnaros");

            int quality = sulfras.Quality;

            updateInventory(100);

            Assert.AreEqual(sulfras.Quality, quality);
        }


        [Test]
        public void BackstagePassQualityIncreasesByTwoWhen10DaysOrLess()
        {
            Item backstagePass = findItemByName("Backstage passes to a TAFKAL80ETC concert");

            int quality = backstagePass.Quality;

            updateInventory(10);

            Assert.AreEqual(35, backstagePass.Quality);
        }

        [Test]
        public void BackstagePassQualityIncreasesByThreeWhen5DaysOrLess()
        {
            Item backstagePass = findItemByName("Backstage passes to a TAFKAL80ETC concert");

            int quality = backstagePass.Quality;

            updateInventory(14);

            Assert.AreEqual(47, backstagePass.Quality);
        }

        [Test]
        public void BackstagePassQualityIsZeroAfterTheConcert()
        {
            Item backstagePass = findItemByName("Backstage passes to a TAFKAL80ETC concert");

            int quality = backstagePass.Quality;

            updateInventory(16);

            Assert.AreEqual(0, backstagePass.Quality);
        }

        [Test]
        public void AgedBrieIncreasesInQualityWithAge()
        {
            Item brie = findItemByName("Aged Brie");

            for (int i = 0; i < 10; i++)
            {
                int quality = brie.Quality;
                inventory.updateQuality();

                Assert.Greater(brie.Quality, quality);
            }

        }

        [Test]
        public void ConjuredItemsDegradeInQualityTwiceAsFast()
        {
            Item conjured = findItemByName("Conjured Mana Cake");

            int quality = conjured.Quality;
            inventory.updateQuality();

            Assert.AreEqual(quality - 2, conjured.Quality);

            quality = conjured.Quality;
            inventory.updateQuality();

            Assert.AreEqual(quality - 2, conjured.Quality);
        }
    }
}