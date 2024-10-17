추가 내용
- play scene: map scene 복사뜨고 dummy player, mesh collider 추가
- PlayerRoom scene: 플레이어 집 안 구현
- house prefab: 집 내부 틀 프리팹

---
- Dialogue_Manager prefab: 대사를 관리
  - GameManager; 싱글톤 & DontDestroyOnLoad
  - SetDialogue(int startId, int endId)로 대사 얼마나 읽을건지 세팅 및 첫줄 실행
  - ShowNextDialogue()로 대사 하나씩 실행
  - 대사를 모두 읽으면 onDialogueEnd 이벤트가 발생 -> 구독해서 활용
  - 자식 Canvas를 알아서 활성화 및 비활성화함

- DialogueDataType.cs
  - json 파일 datatype
  - Dialogue: int id, string speaker, string text
  - DialogueList: List<Dialogue> dialogues <= 사실상 이거를 사용

- DialogueJson.json
  - 대사 저장하는 파일
  - 위 datatype 참고

- Cavas_Letter prefab: 편지 UI

<테스팅용>
- SampleScene
- DialogueTest.cs






