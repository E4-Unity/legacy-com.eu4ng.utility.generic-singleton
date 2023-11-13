# Generic Singleton

## 개요
다른 클래스의 `Awake` 혹은 `OnEnable` 이벤트에서 싱글톤 객체에 접근하는 경우에도 `null` 오류를 발생시키지 않는 제네릭 싱글톤 클래스이다. 최적화보다는 개발의 편의성과 싱글톤 객체 접근에 대한 보장을 목표로 설계하였다.

일반적으로는 싱글톤 객체의 Awake 이벤트에서 자기 자신을 직접 할당하므로 문제가 없지만, 다른 클래스의 `Awake` 혹은 `OnEnable` 이벤트에서 실글톤 객체에 접근하는 경우에는 `FindObjectOfType` 메서드를 사용하므로 최적화를 위해서는 추후에 의존성 주입 방식으로 전환을 권장한다.

## 클래스 다이어그램
![ClassDiagram](/Documentation/ClassDiagram.drawio.png)

## 순서도
![FlowChart](/Documentation/FlowChart.drawio.png)
