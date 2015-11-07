using UnityEngine;

/// <summary>
///A singleton that's made specifically for gameobjects.
///If another object spawns in the scene with the same class
///it'll be destroyed. base.Start () needs to be called in the
///child's Start () if it's used.
/// </summary>
public class GameObjectSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public void Start()
    {
        RemoveDuplicates();
    }

    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
            }
            DontDestroyOnLoad(_instance.gameObject);
            return _instance;
        }
        set
        {
            _instance = value;
            DontDestroyOnLoad(_instance.gameObject);
        }
    }

    void RemoveDuplicates()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
    }

    private static bool applicationIsQuitting = false;
	/// <summary>
	/// When Unity quits, it destroys objects in a random order.
	/// In principle, a Singleton is only destroyed when application quits.
	/// If any script calls Instance after it have been destroyed, 
	///   it will create a buggy ghost object that will stay on the Editor scene
	///   even after stopping playing the Application. Really bad!
	/// So, this was made to be sure we're not creating that buggy ghost object.
	/// </summary>
	public void OnDestroy ()
	{
		_instance = null;	
	}
}