using Invasion.Engine;
using Invasion.Engine.Graphics;
using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Factories;
using Invasion.Models.Factories.ModificatorFactories;
using Invasion.Models.Modificators;
using Xunit;

namespace Invasion.Tests
{
    public class ModificatorsTests
    {
        private IModelFactory<ModificatorBase> _modificatorsFactory;
        private IGraphicProvider _graphicProvider;

        public ModificatorsTests()
        {
            _graphicProvider = new DirectXGraphicsProvider(new SharpDX.Windows.RenderForm());
            Compose();
        }

        [Fact]
        public void SpeedBonus_ApplyTest()
        {
            Player player = new Player(null, new PlayerConfiguration(1, 10));
            _modificatorsFactory = new HigherReloadWeaponBonusFactory(_graphicProvider, null);
            var bonus = _modificatorsFactory.Create();
            bonus.Apply(player);
        }

        public void Compose()
        {
            _graphicProvider.LoadBitmap(@"Sources\background.bmp");
            _graphicProvider.LoadBitmap(@"Sources\character.png");
            _graphicProvider.LoadBitmap(@"Sources\topdownwall.png");
            _graphicProvider.LoadBitmap(@"Sources\pistol.png");
            _graphicProvider.LoadBitmap(@"Sources\knife.png");
            _graphicProvider.LoadBitmap(@"Sources\defaultBullet.png");
            _graphicProvider.LoadBitmap(@"Sources\shootingEnemy.png");
            _graphicProvider.LoadBitmap(@"Sources\speedBonus.png");
            _graphicProvider.LoadBitmap(@"Sources\slowTrap.png");
            _graphicProvider.LoadBitmap(@"Sources\kamikadzeEnemy.png");
            _graphicProvider.LoadBitmap(@"Sources\beatingEnemy.png");
        }
    }
}
