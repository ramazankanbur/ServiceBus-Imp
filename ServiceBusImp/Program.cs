
using ServiceBusImp;

Console.WriteLine("Hello, World!");

var messageBuss = new ServiceBusSample1("Endpoint=sb://promenaplayground.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=tDnqKzZiBpPqVAubKI1yy6V0VEXPfVY0PgMA44wd1Gc=", "rkqueue");

await messageBuss.SendServiceBusMessage();

Console.ReadKey();
