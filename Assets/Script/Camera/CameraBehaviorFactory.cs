using UnityEngine;

public static class CameraBehaviorFactory
{
    public static CameraFollowBehavior Create(CameraPresent present)
    {
        return present switch
        { 
          CameraPresent.Normal => new Camera_Normal(),
          CameraPresent.Speed => new Camera_Speed(),
          CameraPresent.Precise => new Camera_Precise(),
          CameraPresent.Slow => new Camera_Slow(),
          _=> new Camera_Normal(),
        };
    }
}
