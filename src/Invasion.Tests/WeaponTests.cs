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
        }

        public void KnifeAttackTest()
        {
            var weapon = _weaponFactory.Create<Knife>(null);
        }
    }
}
