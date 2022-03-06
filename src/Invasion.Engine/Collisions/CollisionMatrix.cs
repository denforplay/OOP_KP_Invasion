using System.Collections.Generic;

namespace Invasion.Engine.Collisions
{
    public class CollisionMatrix
    {
        private static List<CollisionRow> CollisionTable = new List<CollisionRow>
        {
            new CollisionRow {Layer1 = Layer.Player, Layer2 = Layer.Border, IsCollide = true}
        };

        public static bool IsCollided(Layer layer1, Layer layer2) => CollisionTable.Find(x =>
            x.Layer1 == layer1 && x.Layer2 == layer2)?.IsCollide ?? false;

        private class CollisionRow
        {
            public Layer Layer1 { get; init; }
            public Layer Layer2 { get; init; }
            public bool IsCollide { get; init; }
        }
    }
}
