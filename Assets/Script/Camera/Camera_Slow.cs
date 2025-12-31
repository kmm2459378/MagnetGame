

public class Camera_Slow : CameraFollowBehavior
{
    public override float SmoothX => 0.25f;
    public override float SmoothY => 0.35f;
    public override float LookAheadDistance => 1f;
    public override float LookAheadSmooth => 0.3f;
}




