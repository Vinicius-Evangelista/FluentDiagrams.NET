using FluentDiagrams.NET.AWS;
using FluentDiagrams.NET.Core;

IDiagram diagram = new Diagram()
                   .SetSettings(settings: new DiagramSettings()
                   {
                     OutputPath =
                       @"C:\\Users\\vinie\\diagramaa.png",
                     Width = 3000,
                     Height = 1000,
                     ElementMargin = 50
                   })
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
                   })
                   .Vpc(vpcName: "vpc-4", config: vpc =>
                   {
                     vpc.Ec2(instanceName: "analytics")
                        .Ec2(instanceName: "cache")
                        .Ec2(instanceName: "monitoring");
                   })
                   .Vpc(vpcName: "vpc-5", config: vpc =>
                   {
                     vpc.Ec2(instanceName: "backup")
                        .Ec2(instanceName: "search")
                        .Ec2(instanceName: "queue");
                   });


diagram.Render();
