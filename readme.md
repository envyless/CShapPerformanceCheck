# CSharp, Unity Performance Checker

생성일: 2021년 5월 2일 오후 12:51
태그: C#, Unity

C#의 퍼포먼스 체크를 기록하는 곳

각 항목을 눌러봄으로 테스트 결과를 확인 할 수 있다.

목차 및 설명

- Class의 경우 ref 키워드 속도 차이
- Struct의 경우 ref 키워드 속도 차이
- 어셈블리 단에서 ref 키워드의 차이
- ... 추가예정

[CBV vs CBR](CSharp,%20Unity%20Performance%20Checker%2079daf67cccbe47679a78e9ff58ba5195/CBV%20vs%20CBR%2025f97ab82fb84089886e36c76b6248d8.csv)

## Thread, Task 의 FalsSharing Test

- C#에서 이전에 경험했단 멀티코어를 통한 다중처리가 싱클코어보다 느렸던 경험이 있다.
이번에는 패딩을 통해 개선해보자.