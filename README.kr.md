[English](./README.md)

# Jenkins 서비스 컨트롤

이 프로젝트는 Jenkins 서비스를 재시작하고 특정 캐시/빌드 관련 폴더를 삭제한 후 서비스를 다시 시작하는 C# 콘솔 애플리케이션입니다.

## 주요 기능

- 지정된 Jenkins 서비스가 실행 중인지 확인합니다.
- Jenkins 서비스를 중지합니다.
- 지정된 루트 디렉토리에서 다음 폴더들을 삭제합니다:
  - `lastStable`
  - `lastStableBuild`
  - `lastSuccessful`
  - `lastSuccessfulBuild`
- Jenkins 서비스를 다시 시작합니다.
- NLog를 사용하여 이벤트 로그를 기록합니다.

## 사용 방법

1.  **`app.config` 파일 설정:**
    - `service_Name`: Jenkins 윈도우 서비스의 이름 (예: "Jenkins").
    - `directory_RootName`: Jenkins 작업(job)이 저장된 루트 경로 (예: `C:\Program Files (x86)\Jenkins\jobs`).
    - 주석 처리된 "안전 종료(safe-shutdown)" 기능 관련 설정(`service_URL`, `user_Name`, `password`)이 있으나 현재는 사용되지 않습니다.

2.  **애플리케이션 실행:**
    - `ServiceControl.exe`를 실행합니다.

애플리케이션이 자동으로 서비스 중지, 폴더 삭제, 서비스 시작 순서로 작업을 수행합니다.

## 코드 구조

- `ServiceControl/Program.cs`: 애플리케이션의 메인 진입점. 서비스 제어 및 폴더 삭제 로직을 조정합니다.
- `ServiceControl/Core/ServiceControlCore.cs`: 윈도우 서비스의 시작 및 중지를 처리합니다.
- `ServiceControl/Core/FolderControlCore.cs`: 지정된 폴더를 검색하고 삭제하는 기능을 처리합니다.
- `ServiceControl/Core/ExecuteCommandInCMD.cs`: "안전 종료(safe-shutdown)" 기능을 위해 셸 명령을 실행하는 로직을 포함합니다 (현재 비활성화 상태).
- `ServiceControl/app.config`: 서비스 이름, 경로 등을 위한 설정 파일입니다.
- `ServiceControl/cmd/`: 커맨드 라인을 통해 Jenkins와 상호작용하기 위한 `jenkins-cli.jar` 파일이 포함되어 있습니다.
