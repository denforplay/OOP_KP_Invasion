using Invasion.Models;
using Xunit;

namespace Invasion.Tests.Models
{
    public class BorderTests
    {
        [Fact]
        public void CreateBorderTest()
        {
            Border border = new Border(null);
            Assert.NotNull(border);
        }
    }
}
