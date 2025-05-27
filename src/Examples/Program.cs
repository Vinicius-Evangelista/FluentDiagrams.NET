// See https://aka.ms/new-console-template for more information

using FluentDiagrams.NET.AWS;
using FluentDiagrams.NET.Core;

IDiagram diagram = new Diagram()
                   .Ec2(instanceName: "ec2-1", connectTo: "ec2-2")
                   .Ec2(instanceName: "ec2-2", connectTo: "ec2-3")
                   .Ec2(instanceName: "ec2-3", connectTo: "ec2-4")
                   .Ec2(instanceName: "ec2-5", connectTo: "ec2-6")
                   .Ec2(instanceName: "ec2-7", connectTo: "ec2-8")
                   .Ec2(instanceName: "ec2-9", connectTo: "ec2-10")
                   .Ec2(instanceName: "ec2-11", connectTo: "ec2-12")
                   .Ec2(instanceName: "ec2-13", connectTo: "ec2-14")
                   .Ec2(instanceName: "ec2-15", connectTo: "ec2-16")
                   .Ec2(instanceName: "ec2-17", connectTo: "ec2-18")
                   .Ec2(instanceName: "ec2-19", connectTo: "ec2-20")
                   .Ec2(instanceName: "ec2-21", connectTo: "ec2-22")
                   .Ec2(instanceName: "ec2-23", connectTo: "ec2-24")
                   .Ec2(instanceName: "ec2-25", connectTo: "ec2-26")
                   .Ec2(instanceName: "ec2-27", connectTo: "ec2-28")
                   .Ec2(instanceName: "ec2-29", connectTo: "ec2-30");

diagram.Render();
