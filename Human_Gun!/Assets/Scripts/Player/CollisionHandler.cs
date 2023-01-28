using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    #region HumanCount

    public int HumanCount
    {
        get { return _humanCount; }
        set { _humanCount = value; }
    }

    [SerializeField] private int _humanCount;

    #endregion

    #region CollectedHuman

    public GameObject CollectedHuman
    {
        get { return _collectedHuman; }
        set { _collectedHuman = value; }
    }

    private GameObject _collectedHuman;

    #endregion


    private void Awake() => _humanCount = -1;

    private void OnCollisionEnter(Collision collision)
    {
        var collisionObject = collision.gameObject;
        if (collisionObject.CompareTag(nameof(StringType.Tags.Human)))
        {
            _collectedHuman = collisionObject.gameObject;
            _collectedHuman.tag = nameof(StringType.Tags.Gun);
            _collectedHuman.GetComponent<BoxCollider>().isTrigger = true;
            _humanCount++;
            EventManager.Instance.OnPlayerAnimatorTrigger(nameof(StringType.HumanAnimatorTriggers.Pose03));
        }

        if (collisionObject.CompareTag(nameof(StringType.Tags.Stone)))
        {
            EventManager.Instance.OnCollisionStone();
        }
    }
}