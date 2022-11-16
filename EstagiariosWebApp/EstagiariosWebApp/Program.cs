using EstagiariosWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

try
{
    var data = File.ReadLines("../CadastroCLI/CadastroCLI/bin/Debug/net6.0/estagiariosData");

    // Cada linha percorrida pelo loop armazenar esse estagiario em uma variavel temporaria e ira o adicionar para a lista de estagiarios


    foreach (var line in data)
    {
        EstagiarioModel estagiarioTemp = line;
        EstagiarioModel.estagiariosLista.Add(estagiarioTemp);
    }
}

catch (Exception error)
{ Console.WriteLine("Arquivo vazio ou com dados invalidos."); }

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


