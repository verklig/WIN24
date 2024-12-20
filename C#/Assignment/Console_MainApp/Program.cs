using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Repositories;
using Console_MainApp.Dialogs;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider serviceProvider = new ServiceCollection()
		.AddSingleton<IContactService, ContactService>()
		.AddSingleton<IContactRepository, ContactRepository>()
		.AddSingleton<IFileService>(new FileService("Data", "contacts.json"))
		.AddTransient<MenuDialog>()
		.BuildServiceProvider();

MenuDialog menuDialog = serviceProvider.GetRequiredService<MenuDialog>();
menuDialog.Menu();