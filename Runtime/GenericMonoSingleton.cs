using UnityEngine;

namespace E4.Utility
{
	public class GenericMonoSingleton<T> : MonoBehaviour where T : GenericMonoSingleton<T>
	{
		/* Static */
		static T instance;

		public static T Instance
		{
			get
			{
				// 이미 할당된 상태라면 그대로 반환
				if (instance is not null) return instance;

				// 씬에 이미 배치되어있는 컴포넌트 찾기
				T sceneComponent = FindObjectOfType<T>();

				// 정적 변수 할당 및 반환
				Instance = sceneComponent;
				return instance;
			}
			private set
			{
				instance = value;
				if(instance is not null) instance.TryInit();
			}
		}

		/* Field */
		bool isInitialized;

		/* Property */
		bool IsInstance => ReferenceEquals(instance, GetComponent<T>());

		/* MonoBehaviour */
		protected virtual void Awake()
		{
			if (instance is null)
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
			if (isInitialized) return;
			isInitialized = true;

			InitializeComponent();
		}

		protected virtual void InitializeComponent(){}
	}
}
