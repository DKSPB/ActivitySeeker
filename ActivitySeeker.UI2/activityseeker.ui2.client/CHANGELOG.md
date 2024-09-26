This file explains how Visual Studio created the project.

The following tools were used to generate this project:
- create-vite

The following steps were used to generate this project:
- Create vue project with create-vite: `npm init --yes vue@latest activityseeker.ui2.client -- --eslint `.
- Update `vite.config.js` to set up proxying and certs.
- Update `HelloWorld` component to fetch and display weather information.
- Create project file (`activityseeker.ui2.client.esproj`).
- Create `launch.json` to enable debugging.
- Create `nuget.config` to specify location of the JavaScript Project System SDK (which is used in the first line in `activityseeker.ui2.client.esproj`).
- Add project to solution.
- Add project to the startup projects list.
- Write this file.
