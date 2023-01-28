using UnityEngine;

public class StoneController : Stone
{
    private void OnParticleCollision(GameObject other)
    {
        var collisionObject = other.gameObject;
        if (collisionObject.CompareTag(nameof(StringType.Tags.Bullet)))
        {
            base.ShakeScale();
            base.LowerTheStoneNumber();
        }
    }
}