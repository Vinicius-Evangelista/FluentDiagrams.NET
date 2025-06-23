using FluentDiagrams.NET.AWS;
using FluentDiagrams.NET.Core;

IDiagram diagram = new Diagram()
                   .SetSettings(settings: new DiagramSettings()
                   {
                     OutputPath =
                       @"C:\\Users\\vinie\\diagramaa.png",
                     Width = 2000,
                     Height = 2000,
                     ElementMargin = 50
                   })
                   .Vpc(vpcName: "vpc-1", config: vpc =>
                   {
                     vpc.Ec2(instanceName: "ec2-app-server")
                        .Ec2(instanceName: "ec2-db-server", connectTo: "ec2-app-server")
                        .Ec2(instanceName: "ec2-cache-server", connectTo: "ec2-app-server")
                        .Ec2(instanceName: "ec2-worker-1", connectTo: "ec2-app-server")
                        .Ec2(instanceName: "ec2-worker-2", connectTo: "ec2-app-server")
                        .Ec2(instanceName: "ec2-monitoring", connectTo: "ec2-app-server")
                        .Ec2(instanceName: "ec2-backup", connectTo: "ec2-app-server")
                        .Ec2(instanceName: "ec2-logging", connectTo: "ec2-app-server")
                        .Ec2(instanceName: "ec2-api-gateway", connectTo: "ec2-app-server")
                        .Ec2(instanceName: "ec2-load-balancer", connectTo: "ec2-app-server")
                        .Ec2(instanceName: "ec2-admin", connectTo: "ec2-app-server")
                        .Ec2(instanceName: "ec2-test", connectTo: "ec2-app-server");
                   });

diagram.Render();
