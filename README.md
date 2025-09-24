[한국어](./README.kr.md)

# Jenkins Service Control

This is a C# console application designed to restart the Jenkins service, clear specific cache/build-related folders, and then restart the service.

## Development Log

- [Blog Post](https://yobine.tistory.com/582)

## Features

- Checks if the specified Jenkins service is running.
- Stops the Jenkins service.
- Deletes the following folders from a specified root directory:
  - `lastStable`
  - `lastStableBuild`
  - `lastSuccessful`
  - `lastSuccessfulBuild`
- Starts the Jenkins service.
- Logs events using NLog.

## How to Use

1.  **Configure `app.config`:**
    - `service_Name`: The name of the Jenkins Windows service (e.g., "Jenkins").
    - `directory_RootName`: The root path where Jenkins jobs are stored (e.g., `C:\Program Files (x86)\Jenkins\jobs`).
    - The application also has commented-out settings for a "safe-shutdown" feature (`service_URL`, `user_Name`, `password`) which is not currently active.

2.  **Run the application:**
    - Execute `ServiceControl.exe`.

The application will perform the stop, delete, and start sequence automatically.

## Code Structure

- `ServiceControl/Program.cs`: Main entry point of the application. It orchestrates the service control and folder deletion logic.
- `ServiceControl/Core/ServiceControlCore.cs`: Handles starting and stopping the Windows service.
- `ServiceControl/Core/FolderControlCore.cs`: Handles searching for and deleting the specified folders.
- `ServiceControl/Core/ExecuteCommandInCMD.cs`: Contains logic to execute shell commands, intended for the "safe-shutdown" feature (currently inactive).
- `ServiceControl/app.config`: Configuration file for service name, paths, etc.
- `ServiceControl/cmd/`: Contains the `jenkins-cli.jar` for interacting with Jenkins via the command line.