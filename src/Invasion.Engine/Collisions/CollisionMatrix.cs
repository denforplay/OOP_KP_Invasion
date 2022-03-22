namespace Invasion.Engine.Collisions
{
    public class CollisionMatrix
    {
        private static List<CollisionRow> CollisionTable = new List<CollisionRow>
        {
            new CollisionRow {Layer1 = Layer.Player, Layer2 = Layer.Border, IsCollide = true},
            new CollisionRow {Layer1 = Layer.Enemy, Layer2 = Layer.Bullet, IsCollide = false},
            new CollisionRow {Layer1 = Layer.Player, Layer2 = Layer.Bullet, IsCollide = false},
            new CollisionRow {Layer1 = Layer.Bullet, Layer2 = Layer.Border, IsCollide = false},
            new CollisionRow {Layer1 = Layer.Weapon, Layer2 = Layer.Enemy, IsCollide = false},
            new CollisionRow {Layer1 = Layer.Modificator, Layer2 = Layer.Player, IsCollide = false},
            new CollisionRow {Layer1 = Layer.Modificator, Layer2 = Layer.Weapon, IsCollide = false},
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
