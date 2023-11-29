using UnityEngine;

namespace E4.Utility
{
	public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
	{
		/* Static */
		static T _instance;

		public static T Instance
		{
			get
			{
				// 이미 할당된 상태라면 그대로 반환
				if (_instance is not null) return _instance;

				// 씬에 이미 배치되어있는 컴포넌트 찾기
				T instance = FindObjectOfType<T>();

				// 씬에 존재하지 않는 경우 직접 생성
				if (instance is null)
				{
					var newGameObject = new GameObject(typeof(T).Name);
					instance = newGameObject.AddComponent<T>();
				}

				// 정적 변수 할당 및 반환
				Instance = instance;
				return _instance;
			}
			private set
			{
				_instance = value;
				if(_instance is not null) _instance.TryInit();
			}
		}

		/* Field */
		bool _isInitialized;

		/* Property */
		bool IsInstance => ReferenceEquals(_instance, GetComponent<T>());

		/* MonoBehaviour */
		protected virtual void Awake()
		{
			if (_instance is null)
			{
				Instance = GetComponent<T>();
			}
			else if (!IsInstance)
			{
				Destroy(gameObject);
			}
		}

		protected virtual void OnDestroy()
		{
			if (IsInstance)
			{
				Instance = null;
			}
		}

		/* Method */
		void TryInit()
		{
			// 초기화는 한 번만 진행
			if (_isInitialized) return;
			_isInitialized = true;

			Init();
		}

		protected virtual void Init(){}
	}
}
