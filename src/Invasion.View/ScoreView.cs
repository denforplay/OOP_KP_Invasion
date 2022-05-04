using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Engine.Graphics.GraphicTargets;
using Invasion.Engine.Interfaces;
using Invasion.Models;
using SharpDX;
using SharpDX.Direct2D1;

namespace Invasion.View
{
    public class ScoreView : IView
    {
        private TextRenderer _scoreText;
        private ScoreSystem _scoreSystem;

        public ScoreView(ScoreSystem scoreSystem, IGraphicTarget renderTarget)
        {
            _scoreSystem = scoreSystem;
            _scoreText = new TextRenderer(renderTarget.Target, new RectangleF(Screen.Width / 2f - 200, 50, 400, 100), 50);
            _scoreSystem.OnScoreChanged += ChangeScore;
            _scoreSystem.Restart();
        }

        public void Dispose()
        {
            _scoreText.Dispose();
        }

        public void Update()
        {
            _scoreText.Update();
        }

        private void ChangeScore(int score)
        {
            _scoreText.SetText($"Score: {score} ");
        }
    }
}