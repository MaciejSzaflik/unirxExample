using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CameraCache {

    private static Camera camera;
    public static Camera main
    {
        get
        {
            if(camera == null)
            {
                camera = Camera.main;
            }
            return camera;
        }
    }
     
}
