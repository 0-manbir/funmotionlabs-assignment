using UnityEngine;

[CreateAssetMenu(menuName = "Parkour System/New Action")]
public class ParkourAction : ScriptableObject
{
    [SerializeField] string animationName;

    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;

    [Header("Target Matching")]
    [SerializeField] bool enableTargetMatching = true;
    [SerializeField] AvatarTarget partToMatch;
    [SerializeField] float matchStartPercentage;
    [SerializeField] float matchEndPercentage;

    public Vector3 MatchPos { get; set; }

    public bool IsActionPossible (ObstacleData hitData, Transform player)
    {
        MatchPos = hitData.heightHitInfo.point;
        float height = hitData.heightHitInfo.point.y - player.transform.position.y;
        return height >= minHeight && height <= maxHeight;
    }

    public string Name { get { return animationName; } }

    public bool EnableTargetMatching => enableTargetMatching;
    public AvatarTarget PartToMatch => partToMatch;
    public float MatchStartTime => matchStartPercentage;
    public float MatchTargetTime => matchEndPercentage;
}