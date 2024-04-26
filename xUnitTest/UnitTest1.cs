using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            MyMath myMath = new MyMath();
            int a = 10;
            int b = 20;
            int expected = 30;

            int actual = myMath.Add(a, b);
            Assert.Equal(expected, actual); 
        }
    }
}
