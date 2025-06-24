using FluentDiagrams.NET.AWS;
using FluentDiagrams.NET.Core;

IDiagram diagram = new Diagram()
                   .SetSettings(settings: new DiagramSettings()
                   {
                     OutputPath =
                       @"C:\\Users\\vinie\\diagramaa.png",
                     Width = 2000,
                     Height = 1000,
                     ElementMargin = 50
                   })

                   .Ec2(instanceName: "datadog-agent")
                   .Vpc(vpcName: "vpc-2", config: vpc =>
                   {
                     vpc.Ec2(instanceName: "api")
                        .Ec2(instanceName: "worker")
                        .Ec2(instanceName: "front");
                   })
                   .Vpc(vpcName: "vpc-3", config: vpc =>
                   {
                     vpc.Ec2(instanceName: "web-server")
                        .Ec2(instanceName: "database")
                        .Ec2(instanceName: "kafka");
                   });

diagram.Render();
