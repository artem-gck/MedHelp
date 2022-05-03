using MedHelp.Access;
using MedHelp.Access.Context;
using MedHelp.Access.SqlServer;
using MedHelp.Services;
using MedHelp.Services.Logic;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MedHelpContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IAuthService, AuthService>(); 
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<IPatientService, PatientService>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddTransient<IAuthAccess, AuthAccess>();
builder.Services.AddTransient<IDoctorAccess, DoctorAccess>();
builder.Services.AddTransient<IPatientAccess, PatientAccess>();
builder.Services.AddTransient<ICommentAccess, CommentAccess>();

builder.Services.AddCors();

builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseStatusCodePages();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(options => options.AllowAnyOrigin()
                              .AllowAnyMethod()
                              .AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();