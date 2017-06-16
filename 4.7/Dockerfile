FROM microsoft/dotnet-framework:4.7

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# install WCF services required components.
RUN Add-WindowsFeature Web-Server; \
Add-WindowsFeature NET-WCF-TCP-Activation45; \
Add-WindowsFeature NET-WCF-HTTP-Activation45; \
Add-WindowsFeature Web-WebSockets

ADD ServiceMonitor.exe /ServiceMonitor.exe

EXPOSE 80

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]
