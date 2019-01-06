using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameobjectExtensions {

	public static T AddOrGetComponent<T> (this GameObject gameObject) where T : Component {
        var comp = gameObject.GetComponent<T>();
        if (comp == null)
            return gameObject.AddComponent<T>();
        else
            return comp;
	}
}
