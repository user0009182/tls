// See https://aka.ms/new-console-template for more information
using client;

Console.WriteLine("Hello, World!");

//set the current directory to the parent of the certificates folder
//search upwards until it is found
var dir = Directory.GetCurrentDirectory();
while (!Directory.Exists(Path.Combine(dir, "certificates")))
{
    var parentDir = Directory.GetParent(dir);
    if (parentDir == null)
    {
        Console.WriteLine("Cannot find certificates directory");
        return;
    }
    dir = parentDir.FullName;
}
Directory.SetCurrentDirectory(dir);

var client = new TlsClient();
client.Connect("localhost", 10101, @"certificates\clientcert.pem");