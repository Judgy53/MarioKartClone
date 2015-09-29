using UnityEngine;

/// <summary>
/// Base class for singleton monobehaviour
/// </summary>

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	static private T m_instance = null;
	static public T Instance
	{
		get
		{
			if (m_instance == null)
			{
				T target = GameObject.FindObjectOfType(typeof(T)) as T;
				if(target != null)
				{
					m_instance = target;
				}
			}
			return m_instance;
		}
	}
}
