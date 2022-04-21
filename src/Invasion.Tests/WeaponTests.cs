using Invasion.Models.Factories.WeaponsFactories;
using Invasion.Models.Weapons;
using Invasion.Models.Weapons.Melee;
using Xunit;

namespace Invasion.Tests
{
    public class WeaponTests
    {
        private WeaponFactory _weaponFactory;

        public WeaponTests()
        {
            _weaponFactory = new WeaponFactory(null, null, null);
        }

        [Fact]
        public void TestCreateKnife()
        {
            Assert.True(true);
        }
    }
}
