# Faber Controller

Web controller for a Hyperledger Aries Faber CloudAgent demo

## Table of Contents

- [Running Locally](#running-locally)
    - [Prerequisites](#prerequisites)
    - [Start the Application](#start-the-application)
- [Notes](#notes)


### Running Locally (Steps to start)
Note: Before carrying out the following process, start up the von-network from Docker on localhost:9000, as the agent requires the von-network, and without starting it, it won't work.
Commands:
cd von-network
./manage build
./manage start --logs
visit: localhost:9000

Step 1: Clone the aries-cloudagent-python project in your system from this link:https://github.com/hyperledger/aries-cloudagent-python

Step 2: Starting the issuer agent Open git bash and navigate to the folder where the agent project is cloned.

Step 3: Then write the command FABER Agent: cd /aries-cloudagent-python/demo.

Step 4: Then, to run the agent, write ./run_demo faber, and In case of error, docker rm -f faber. Now we have successfully run the agent.

Step 5: Now we will run the controller project. Clone the controller project in your system from the GitHub link:https://github.com/hyperledger/aries-acapy-controllers

Step 6: Navigate to the folder in another bash tab using the command cd /aries-acapy-controllers/AliceFaberAcmeDemo/controllers/faber-controller.(Prerequisite: Dotnet needs to be installed)

Step 7: Write dotnet run -p FaberController to run the controller

Step 8: Start the project on localhost:5000

#### Prerequisites

Faber Controller requires `.NET Core 3.1`. Installation instructions for vairous platforms can be viewed [here](https://dotnet.microsoft.com/download).

#### Start the Application

`.NET Core 3.1` comes with a CLI for running .NET applications. To run the controller, you simply need to issue the following command from the faber-controller root directory in a terminal:

For example on Linux:

```
$ dotnet run -p FaberController
```
You may see an output like:

```
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: https://localhost:5001
info: Microsoft.Hosting.Lifetime[0]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
```

You can now open your browser tab to either `localhost:5000` or `localhost:5001` to see the application.

### Notes

_Note: Faber Controller has already been configured to connect to it's agent on localhost:8021. If the controller is not connected to it's agent you will see a red status indicator on the top right-hand side of the navbar. If the agent is successfully connected, you will see a green status indicator._
