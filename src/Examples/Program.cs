using FluentDiagrams.NET.AWS;
using FluentDiagrams.NET.Core;

IDiagram diagram = new Diagram()
                   .Vpc(vpcName: "vpc-1", config: vpc =>
                   {
                     vpc.Ec2(instanceName: "ec2-X")
                        .Ec2(instanceName: "ec2-Z")
                        .Ec2(instanceName: "ec2-w", connectTo: "ec2-X")
                        .Ec2(instanceName: "ec2-U", connectTo: "ec2-X")
                        .Ec2(instanceName: "ec2-1", connectTo: "ec2-X")
                        .Ec2(instanceName: "ec2-2", connectTo: "ec2-X")
                        .Ec2(instanceName: "ec2-3", connectTo: "ec2-X")
                        .Ec2(instanceName: "ec2-4", connectTo: "ec2-U")
                        .Ec2(instanceName: "ec2-5", connectTo: "ec2-4")
                        .Ec2(instanceName: "ec2-6", connectTo: "ec2-1")
                        .Ec2(instanceName: "ec2-7", connectTo: "ec2-2")
                        .Ec2(instanceName: "ec2-8", connectTo: "ec2-2")
                        .Ec2(instanceName: "ec2-9", connectTo: "ec2-Z")
                        .Ec2(instanceName: "ec2-10", connectTo: "ec2-X");
                   });


diagram.Render();
