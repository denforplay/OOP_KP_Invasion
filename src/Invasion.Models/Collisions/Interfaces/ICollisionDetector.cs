namespace Invasion.Models.Collisions.Interfaces
{
    public interface ICollisionDetector
    {
        (object, object) DetectCollision();
    }
}
