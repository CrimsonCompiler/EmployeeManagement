namespace EmployeeManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Dependecny Injection 
            builder.Services.AddControllers();

            var app = builder.Build();

            // 2. Http Request Routing middleware active
            app.UseRouting();

            // 3. Mapping the controllers endpoint
            app.MapControllers();

            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}
