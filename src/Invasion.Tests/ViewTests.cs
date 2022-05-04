using Invasion.Engine;
using Invasion.Models;
using Invasion.Models.Configurations;
using Invasion.Models.Enemies;
using Invasion.View;
using Invasion.View.Factories.Base;
using System;
using Xunit;

namespace Invasion.Tests
{
    public class ViewTests
    {
        [Fact]
        public void TestGameObjectViewFactory()
        {
            var enemyBaseViewFactory = new GameObjectViewFactoryBase<EnemyBase>();
            var enemyModel = new KamikadzeEnemy(null, new EnemyConfiguration(3, 1, 1));
            var entity = new Entity<EnemyBase>(enemyModel);
            var view = enemyBaseViewFactory.Create(entity);
            Assert.NotNull(view);
            view.Update();
            enemyBaseViewFactory.Destroy(entity);
            Assert.True(enemyBaseViewFactory is not null);
        }

        [Fact]
        public void TestScoreView()
        {
            DirectXGraphicsProvider directXGraphicsProvider = new DirectXGraphicsProvider(new SharpDX.Windows.RenderForm());
            ScoreSystem scoreSystem = new ScoreSystem();
            ScoreView scoreView = new ScoreView(scoreSystem, directXGraphicsProvider.GraphicTarget);
            scoreView.Dispose();
        }

        [Fact]
        public void TestHealthView()
        {
            DirectXGraphicsProvider directXGraphicsProvider = new DirectXGraphicsProvider(new SharpDX.Windows.RenderForm());
            var enemyModel = new KamikadzeEnemy(null, new EnemyConfiguration(3, 1, 1));
            HealthView healthView = new HealthView(enemyModel, directXGraphicsProvider.GraphicTarget);
            healthView.Dispose();
        }
    }
}
