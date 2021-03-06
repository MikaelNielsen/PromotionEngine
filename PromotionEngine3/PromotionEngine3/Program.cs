﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        // find a unit with the correct sku and remove max number of units. Return value
        public override int ExtractValue(List<UnitCount> units)
        {
            foreach (var unit in units)
            {
                if (unit.Sku != Sku)
                    continue;

                var multi = unit.Count / Count;
                if (multi == 0)
                    continue;

                unit.Count -= multi * Count;
                units.RemoveAll(u => u.Count == 0); // can't have zero items
                return multi * Value;
            }
            return 0;
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

        // find sku1 and sku2 in units, remove max count, return value
        public override int ExtractValue(List<UnitCount> units)
        {
            var u1 = units.Find(u => u.Sku == Sku1);
            if (u1 == null)
                return 0;

            var u2 = units.Find(u => u.Sku == Sku2);
            if (u2 == null)
                return 0;

            var multi = Math.Min(u1.Count, u2.Count);
            Debug.Assert(multi > 0);
            u1.Count -= multi;
            u2.Count -= multi;
            units.RemoveAll(u => u.Count == 0); // can't have zero items
            return multi * Value;
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
        // promotions available in this scenario
        public List<Promotion> Promotions = new List<Promotion>();
        
        // create a scenario with promotions
        public Scenario(List<Promotion> promotions)
        {
            Promotions.AddRange(promotions);
        }

        // calculate price for units using promotions

        public int CalcBestPrice(List<UnitCount> orgUnits)
        {

            var units = new List<UnitCount>(); // this algoritm destroys the units counted - operate on copy
            units.AddRange(orgUnits);
            var cost = 0;
            for (; ; )
            {
                var value = FindPromotion(units);
                if (value == 0)
                    break;
                cost += value;
            }

            foreach (var unit in units)
                cost += unit.Count * unit.Value;

            return cost;
        }

        // ask each promotion to apply ExtractValue() to units
        private int FindPromotion(List<UnitCount> units)
        {
            var value = 0;
            foreach (var prom in Promotions)
                value += prom.ExtractValue(units);
            return value;
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
