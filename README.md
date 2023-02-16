# Jenkins-Restart 
 
( * 2020년 9월에 개발된 Tool의 개발 로그입니다.)

회사에서 사용하고 있는 Jenkins 가 간혹 이상 증상을 일으키는데, 그 중 하나는 Windows OS 를 사용하는 Slave-node 들이 모두 off-line 으로 변경 된다.

사용 중 인 Jenkins 버전 (2.13) 버그여서, Jenkins 업데이트를 하게 되면 없어지는 현상이지만, 해당 Jenkins 에 등록되어 배포 중 인 프로젝트들도 많고 (20+), 설치 된 플러그인들 중, 현 버전의 Jenkins 에서는 잘 사용 중이지만, 상위 버전에서는 지원이 끊겨, 업데이트를 시도 했다가, 여러 문제가 발생하여 다시 현재 버전의 Jenkins 로 롤백하였고, 해당 버그를 안고 사용 중 이다.

이 후 2.13 버전의 Jenkins 서비스엔 새로운 프로젝트 등록은 하지 않고 있고, 기존 프로젝트들만 필요 시 따로 분리하는 작업을 진행 중이긴 하지만 원치 않는 부서들이 더 많아 해당 오류 발생 시 연락이 바로 왔을 때 조치를 취해 줘야 합니다.

Windows OS 를 사용하는 Slave-Node 들이 모두 off-line 으로 변경되는 오류 발생 시 오류제거를 위해 만들어진 프로그램이다.

What is does
1. It searches for the Jenkins Service
2. if it is running, it stops the Jenkins Service (The safe-restart version is coming)
3. it searches for targeted directories under the %jenkins home%/jobs/ folder and deletes them
4. it starts jenkins Service . 

5. I did make a method to stop the service in the safe mode.. but I haven't added cmd options yets.


... ( https://yobine.tistory.com/582 )
