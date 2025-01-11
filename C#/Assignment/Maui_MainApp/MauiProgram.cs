using Infrastructure.Interfaces;
using Infrastructure.Services;
using Infrastructure.Repositories;

namespace Maui_MainApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
			
		builder.Services.AddSingleton<IContactService, ContactService>();
		builder.Services.AddSingleton<IContactRepository, ContactRepository>();
		builder.Services.AddSingleton<IFileService>(new FileService("Data", "contacts.json"));

		return builder.Build();
	}
}
