namespace Invasion.Engine.Collisions
{
    /// <summary>
    /// Represents entity to configure layer collisions
    /// </summary>
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
            new CollisionRow {Layer1 = Layer.Weapon, Layer2 = Layer.Enemy, IsCollide = false},
            new CollisionRow {Layer1 = Layer.Weapon, Layer2 = Layer.Player, IsCollide = false},
            new CollisionRow {Layer1 = Layer.Enemy, Layer2 = Layer.Player, IsCollide = false},
        };

        /// <summary>
        /// Check if two layers are collided
        /// </summary>
        /// <param name="layer1">One layer</param>
        /// <param name="layer2">Second layer</param>
        /// <returns>True if layers should be collided, other returns false</returns>
        public static bool IsCollided(Layer layer1, Layer layer2) => CollisionTable.Find(x =>
            x.Layer1 == layer1 && x.Layer2 == layer2)?.IsCollide ?? false;

        /// <summary>
        /// Collision row, contains data to work with collisions
        /// </summary>
        private class CollisionRow
        {
            public Layer Layer1 { get; init; }
            public Layer Layer2 { get; init; }
            public bool IsCollide { get; init; }
        }
    }
}
