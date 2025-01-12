using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Repositories;
using Console_MainApp.Dialogs;
using Microsoft.Extensions.DependencyInjection;

string rootDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../../"));  
string dataDirectory = Path.Combine(rootDirectory, "Data");

ServiceProvider serviceProvider = new ServiceCollection()
		.AddSingleton<IContactService, ContactService>()
		.AddSingleton<IContactRepository, ContactRepository>()
		.AddSingleton<IFileService>(new FileService(dataDirectory, "contacts.json"))
		.AddTransient<MenuDialog>()
		.BuildServiceProvider();

MenuDialog menuDialog = serviceProvider.GetRequiredService<MenuDialog>();
menuDialog.Menu();