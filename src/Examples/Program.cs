using FluentDiagrams.NET.AWS;
using FluentDiagrams.NET.Core;

IDiagram diagram = new Diagram()
                      .Ec2(instanceName: "ec2-11")
                      .Ec2(instanceName: "ec2-12")
                      .Ec2(instanceName: "ec2-13")
                      .Ec2(instanceName: "ec2-14")
                      .Ec2(instanceName: "ec2-15",
                           connectTo: "ec2-11")
                      .Ec2(instanceName: "ec2-16",
                           connectTo: "ec2-15")
                      .Ec2(instanceName: "ec2-17",
                           connectTo: "ec2-11")
                      .Ec2(instanceName: "ec2-18",
                           connectTo: "ec2-11")
                      .Ec2(instanceName: "ec2-19",
                           connectTo: "ec2-11")
                      .Ec2(instanceName: "ec2-20",
                           connectTo: "ec2-11")
                      .Vpc(vpcName: "vpc-1", config: vpc =>
                      {
                        vpc.Ec2(instanceName: "ec2-X")
                           .Ec2(instanceName: "ec2-Z")
                           .Ec2(instanceName: "ec2-w",
                                connectTo: "ec2-X")
                           .Ec2(instanceName: "ec2-U",
                                connectTo: "ec2-X")
                           .Ec2(instanceName: "ec2-1",
                                connectTo: "ec2-X")
                           .Ec2(instanceName: "ec2-3",
                                connectTo: "ec2-X")
                           .Ec2(instanceName: "ec2-4",
                                connectTo: "ec2-U")
                           .Ec2(instanceName: "ec2-5",
                                connectTo: "ec2-4")
                           .Ec2(instanceName: "ec2-6",
                                connectTo: "ec2-1")
                           .Ec2(instanceName: "ec2-2",
                                connectTo: "ec2-12")
                           .Ec2(instanceName: "ec2-9",
                                connectTo: "ec2-12")
                           .Ec2(instanceName: "ec2-10",
                                connectTo: "ec2-X");
                      });

diagram.Render();
