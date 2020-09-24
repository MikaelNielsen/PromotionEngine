using Microsoft.VisualStudio.TestTools.UnitTesting;
using PromotionEngine3;
using System.Collections.Generic;
using System.Diagnostics;


// Testing the PromotionEngine3 project
namespace PromotionEngineTest
{
    // unit and promotion prices in this test
    public class PriceValues
    {
        // Unit price per SKU
        public int A = 50;
        public int B = 30;
        public int C = 20;
        public int D = 15;

        // Active Promotions 
        public int AAA = 130; // promoted price of 3 As
        public int BB = 45; // promoted price of 2 Bs
        public int CD = 30; // promoted price of C and D
    }

    [TestClass]
    public class PromotionEngineTest
    {
        PriceValues Price = new PriceValues();

        // Build the common scenario for all tests
        Scenario BuildScenario()
        {
            var promotions = new List<Promotion>
             {
                 new SinglePromotion(3, 'A', Price.AAA),
                 new SinglePromotion(2, 'B', Price.BB),
                 new CombiPromotion('C', 'D', Price.CD)
             };
            var scen = new Scenario(promotions);
            return scen;
        }

        // test Scenario A
        [TestMethod]
        public void ScenarioA()
        {
            var scen = BuildScenario();
            var units = new List<UnitCount> 
            { 
                new UnitCount(1, 'A', Price.A), 
                new UnitCount(1, 'B', Price.B), 
                new UnitCount(1, 'C', Price.C) 
            };
            var price = scen.CalcBestPrice(units);
            Debug.Assert(price == Price.A + Price.B + Price.C);
        }

        // test Scenario B
        [TestMethod]
        public void ScenarioB()
        {
            var scen = BuildScenario();
            var units = new List<UnitCount> 
            { 
                new UnitCount(5, 'A', Price.A), 
                new UnitCount(5, 'B', Price.B), 
                new UnitCount(1, 'C', Price.C) 
            };
            var price = scen.CalcBestPrice(units);
            Debug.Assert(price == Price.AAA + 2 * Price.A + 2 * Price.BB + Price.B + Price.C);
        }

        // test Scenario C
        [TestMethod]
        public void ScenarioC()
        {
            var scen = BuildScenario();
            var units = new List<UnitCount>
            {
                new UnitCount(3, 'A', Price.A),
                new UnitCount(5, 'B', Price.B),
                new UnitCount(1, 'C', Price.C),
                new UnitCount(1, 'D', Price.D)
            };
            var price = scen.CalcBestPrice(units);
            Debug.Assert(price == Price.AAA + 2 * Price.BB + Price.B + Price.CD);
        }
    }
}
