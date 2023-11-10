using UnityEngine;

namespace E4.Utility
{
	public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
	{
		static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance is not null) return _instance;

				_instance = FindObjectOfType<T>();
				if (_instance is not null) return _instance;

				var instance = new GameObject(typeof(T).Name);
				_instance = instance.AddComponent<T>();

				return _instance;
			}
		}

		bool IsInstance => ReferenceEquals(_instance, GetComponent<T>());

		protected virtual void Awake()
		{
			if (_instance is null)
				_instance = GetComponent<T>();
			else if (!IsInstance)
				Destroy(gameObject);
		}

		protected virtual void OnDestroy()
		{
			if (IsInstance)
				_instance = null;
		}
	}
}
