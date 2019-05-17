using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace UnitTest
{
    [TestClass]
    public class CsvEntryUnitTest
    {
        [TestMethod]
        public void TestAddDimension()
        {
            var key = "key";
            var value = "10.0";

            var entry = new CsvEntry();
            entry.AddValue(key, value, true);

            Assert.AreEqual(0, entry.MeasureValues.Count);
            Assert.AreEqual(1, entry.DimensionValues.Count);
            Assert.IsTrue(entry.DimensionValues.ContainsKey(key));
            Assert.AreEqual(value, entry.DimensionValues[key]);
        }

        [TestMethod]
        public void TestAddMeasure()
        {
            var key = "key";
            var value = "10.0";

            var entry = new CsvEntry();
            entry.AddValue(key, value, false);

            Assert.AreEqual(0, entry.DimensionValues.Count);
            Assert.AreEqual(1, entry.MeasureValues.Count);
            Assert.IsTrue(entry.MeasureValues.ContainsKey(key));
            Assert.AreEqual(value, entry.MeasureValues[key]);
        }

        [TestMethod]
        public void TestToString()
        {
            var key1 = "key1";
            var value1 = "10.0";
            var key2 = "key2";
            var value2 = "value2";

            var entry = new CsvEntry();

            entry.AddValue(key1, value1, true);
            entry.AddValue(key2, value2, false);

            var expectedString = "10.0,value2";

            Assert.AreEqual(expectedString, entry.ToString());
        }
    }
}
