
This archive demonstrates how a portable console application can access text resources both under Windows and Linux.
It also shows how to create a Docker image for a .NET 6 console application with a reference to a .NET Standard 2.0 library.
It uses the official .NET 6 SDK image from Microsoft for running under Linux.
The library contains localized text resources and demonstrates access to these resources for several languages.

Next to text resource handling, binary resource loading is also demonstrated.
The ResourceFactory class supports loading resources from any assembly that has embedded resources.
This is locale independent and does not use the ResourceDictionary type.
The assemblies must first be registered with the ResourceFactory class.

For testing and debugging under Windows simply open DotNetStandardConsole.sln.

Checking "Use the WSL 2 based engine" in Docker Desktop will make sure that the project runs under Linux.

This project is based on:
https://github.com/dotnet/dotnet-docker/blob/main/samples/build-in-sdk-container.md

Convenient docker commands to experiment with this setup: (run in root of archive)

# Create image
docker build -t counter-image -f DockerfileSln .

docker images

# Create container
docker create --name core-counter counter-image

docker ps -a

# Start, stop and remove container

docker start core-counter

docker attach --sig-proxy=false core-counter

docker stop core-counter

docker rm core-counter

# Create, start, stop and remove container in one go

docker run -it --rm counter-image 3

