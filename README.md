
# BlazorHeartbeat
[![Build - CI](https://github.com/xaviersolau/BlazorHeartbeat/workflows/Build%20-%20CI/badge.svg)](https://github.com/xaviersolau/BlazorHeartbeat/actions?query=workflow%3A%22Build+-+CI%22)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![NuGet Beta](https://img.shields.io/nuget/vpre/SoloX.BlazorHeartbeat.svg)](https://www.nuget.org/packages/SoloX.BlazorHeartbeat)

BlazorHeartbeat is a Blazor component that maintains the SignalR connection in a Server side Blazor application.
It is particularly useful when your application is hosted behind an Application Gateway with a connection timeout.

Don't hesitate to post issue, pull request on the project or to fork and improve the project.

## License and credits

BlazorHeartbeat project is written by Xavier Solau. It's licensed under the MIT license.

 * * *

## Installation

You can checkout this Github repository or you can use the NuGet package:

**Install using the command line from the Package Manager:**
```bash
Install-Package SoloX.BlazorHeartbeat -version 1.0.0-alpha.1
```

**Install using the .Net CLI:**
```bash
dotnet add package SoloX.BlazorHeartbeat --version 1.0.0-alpha.1
```

**Install editing your project file (csproj):**
```xml
<PackageReference Include="SoloX.BlazorHeartbeat" Version="1.0.0-alpha.1" />
```

## How to use it

Note that you can find code examples in this repository in this location: `src/examples`.

Basically, all you need to do is to add the `Heartbeat` element at the end of the `App.rasor`
file in your Blazor Server-Side application.
You can optionally specify the PingDelay.

```html
<Heartbeat PingDelay="60000" />
```

Don't forget to add the using directive in the `_Imports.razor` file:

```razor
@using SoloX.BlazorHeartbeat
```
