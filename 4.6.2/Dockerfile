# Pull microsoft/iis image which contains microsoft/windowsservercore and .NET v4.6.2

FROM microsoft/iis

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# install WCF services required components.
RUN Add-WindowsFeature NET-WCF-HTTP-Activation45; \
    Add-WindowsFeature NET-WCF-TCP-Activation45; \
    Add-WindowsFeature NET-WCF-Pipe-Activation45; \
    Add-WindowsFeature Web-WebSockets
