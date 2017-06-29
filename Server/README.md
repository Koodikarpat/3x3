# 3x3 Server

MainClass is defined in Program.cs, this is where everything begins.

Parser.cs, Message.cs, and Connection.cs are fairly mature, however if you make changes to the protocol, make sure to update all 3 files they are higly interdependent and break sneakily.

Game.cs and Server.cs are very incomplete.

## Usage

This code depends on JSON.NET you'll have to reference its assemblies.
