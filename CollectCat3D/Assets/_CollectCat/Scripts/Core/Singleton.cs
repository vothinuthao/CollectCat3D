using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Instance tĩnh được bảo vệ để chỉ có thể truy cập thông qua property Instance
    private static T _instance;
    
    // Flag kiểm tra xem đã bắt đầu destroy object chưa
    private static bool _isQuitting = false;
    
    // Lock object để đảm bảo thread-safe
    private static readonly object _lock = new object();
    
    public static T Instance
    {
        get
        {
            // Kiểm tra nếu đang quit game thì return null
            if (_isQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' đã bị destroy khi thoát game." +
                    " Sẽ return null.");
                return null;
            }
            
            // Double-check locking pattern
            lock (_lock)
            {
                if (_instance == null)
                {
                    // Tìm tất cả các instance trong scene
                    _instance = (T)FindObjectOfType(typeof(T));
                    
                    // Nếu có nhiều hơn 1 instance trong scene
                    var allInstances = FindObjectsOfType(typeof(T));
                    if (allInstances.Length > 1)
                    {
                        Debug.LogError($"[Singleton] Có nhiều hơn 1 instance của Singleton: {typeof(T)}");
                        return _instance;
                    }
                    
                    // Nếu không tìm thấy instance nào, tạo mới GameObject
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = $"[Singleton] {typeof(T)}";
                        
                        Debug.Log($"[Singleton] Tạo instance mới của singleton: {typeof(T)}");
                        
                        // Đảm bảo instance không bị destroy khi load scene mới
                        DontDestroyOnLoad(singleton);
                    }
                }
                
                return _instance;
            }
        }
    }
    
    protected virtual void Awake()
    {
        // Kiểm tra nếu đã có instance tồn tại
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning($"[Singleton] Trying to create second instance of singleton {GetType()}");
            Destroy(gameObject);
        }
    }
    
    protected virtual void OnApplicationQuit()
    {
        _isQuitting = true;
    }
}