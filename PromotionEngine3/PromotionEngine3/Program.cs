using System;
using System.Collections.Generic;

namespace PromotionEngine3
{
    public class Promotion
    {
        public int Value = 0; // all promotions have a value

        // find a valid promotion in unit list, remove items, and return promotion value
        public virtual int ExtractValue(List<UnitCount> units)
        {
            return 0;
        }
    }

    // promotion involving 1 SKU
    public class SinglePromotion : Promotion
    {
        int Count = 0; // how many units required for promotion
        char Sku = '-'; // name of SKU

        public SinglePromotion(int count, char sku, int value)
        {
            Count = count;
            Sku = sku;
            Value = value;
        }
    }

    // promotion involving 2 SKUs
    public class CombiPromotion : Promotion
    {
        char Sku1 = '0'; // name of SKU
        char Sku2 = '0'; // name of SKU

        public CombiPromotion(char sku1, char sku2, int value)
        {
            Sku1 = sku1;
            Sku2 = sku2;
            Value = value;
        }
    }

    public class UnitCount
    {
        public int Count = 0;
        public char Sku = '-';
        public int Value = 0;
        public UnitCount(int count, char sku, int value)
        {
            Count = count;
            Sku = sku;
            Value = value;
        }
    }

    // A checkout process scenario with a list of promotions
    public class Scenario
    {
        public List<Promotion> Promotions = new List<Promotion>();
        public Scenario(List<Promotion> promotions)
        {
            Promotions.AddRange(promotions);
        }

        public int CalcBestPrice(List<UnitCount> units)
        {
            return 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Promotion Engine");
        }
    }
}
