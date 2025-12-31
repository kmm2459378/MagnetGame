

public class Camera_Precise : CameraFollowBehavior
{
    public override float SmoothX => 0.2f;
    public override float SmoothY => 0.5f;
    public override float LookAheadDistance => 1.5f;
    public override float LookAheadSmooth => 0.3f;
}
    