
public class Camera_Speed : CameraFollowBehavior
{
    public override float SmoothX => 0.05f;
    public override float SmoothY => 0.15f;
    public override float LookAheadDistance => 3f;
    public override float LookAheadSmooth => 0.15f;
}
