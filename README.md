# WCF Docker Image

## Supported tags and respective `Dockerfile` links

* 4.7-windowsservercore-10.0.14393.1358, latest ([windowsservercore/Dockerfile](https://github.com/Microsoft/wcf-docker/blob/master/4.7/Dockerfile))
* 4.6.2-windowsservercore-10.0.14393.1358 ([windowsservercore/Dockerfile](https://github.com/Microsoft/wcf-docker/blob/master/4.6.2/Dockerfile))

This image is built from the [microsoft/wcf-docker GitHub repo](https://github.com/microsoft/wcf-docker).

## What is WCF?
The Windows Communication Foundation (WCF) is  a framework for building service-oriented applications. Using WCF, you can send data as asynchronous messages from one service endpoint to another. A service endpoint can be part of a continuously available service hosted by IIS, or it can be a service hosted in an application.

![WCF Docker Image](https://avatars2.githubusercontent.com/u/6154722?v=3&s=200)

## How to use this image?
### Create a Dockerfile with your WCF service IIS Hosted or selfhosted
```
FROM microsoft/wcf

WORKDIR WcfService

RUN powershell -NoProfile -Command \
    Import-module IISAdministration; \
    New-IISSite -Name "WcfService" -PhysicalPath C:\WcfService -BindingInformation "*:83:"

EXPOSE 83

COPY content/ .
```
You can then build and run the Docker image:
```
$ docker build -t wcfserviceimage .
$ docker run -d -p 83:83 --name my-wcfservice wcfserviceimage
```

There is no need to specify an `ENTRYPOINT` in your Dockerfile since an entrypoint application is already specified that monitors the status of the IIS World Wide Web Publishing Service (W3SVC).

### Verify in the browser

> With the current release, you can't use `http://localhost` to browse your site from the container host. This is because of a known behavior in WinNAT, and will be resolved in future. Until that is addressed, you need to use the IP address of the container.

Once the container starts, you'll need to finds its IP address so that you can connect to your running container from a browser. You use the `docker inspect` command to do that:

`docker inspect -f "{{ .NetworkSettings.Networks.nat.IPAddress }}" my-wcfservice`

You will see an output similar to this:

```
172.28.103.001
```

You can connect the running container using the IP address and configured port, `http://172.28.103.001:83/<wcfservice.svc>` in the example shown.

For a comprehensive tutorial on running an WCF service in a container, check out [WCF service samples in container](https://github.com/Microsoft/wcf-docker-samples)

## Supported Docker Versions
This image has been tested on Docker Versions 1.12.2-cs2-ws-beta or higher.

## License
MICROSOFT SOFTWARE SUPPLEMENTAL LICENSE TERMS

CONTAINER OS IMAGE

Microsoft Corporation (or based on where you live, one of its affiliates) (referenced as "us," "we," or "Microsoft") licenses this Container OS Image supplement to you ("Supplement"). You are licensed to use this Supplement in conjunction with the underlying host operating system software ("Host Software") solely to assist running the containers feature in the Host Software. The Host Software license terms apply to your use of the Supplement. You may not use it if you do not have a license for the Host Software. You may use this Supplement with each validly licensed copy of the Host Software.

## User Feedback
If you have any issues or concerns, reach out to us through a [GitHub issue](https://github.com/Microsoft/wcf-docker/issues/new).
