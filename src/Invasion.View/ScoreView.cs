using System;
using Invasion.Core.Interfaces;
using Invasion.Engine;
using Invasion.Engine.Components;
using Invasion.Models;
using SharpDX;
using SharpDX.Direct2D1;

namespace Invasion.View
{
    public class ScoreView : IView
    {
        private TextRenderer _scoreText;
        private ScoreSystem _scoreSystem;

        public ScoreView(ScoreSystem scoreSystem, WindowRenderTarget renderTarget)
        {
            _scoreSystem = scoreSystem;
            _scoreText = new TextRenderer(renderTarget, new RectangleF(Screen.Width / 2f - 200, 50, 400, 100), 50);
            _scoreSystem.OnScoreChanged += ChangeScore;
            _scoreSystem.Restart();
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