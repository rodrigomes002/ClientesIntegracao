using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var options = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(options);

Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", builder.Configuration["AWS:AccessKey"]);
Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", builder.Configuration["AWS:SecretKey"]);
Environment.SetEnvironmentVariable("AWS_REGION", builder.Configuration["AWS:Region"]);
Environment.SetEnvironmentVariable("AWS_CONTENT", builder.Configuration["VAR:TableName"]);

builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();

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
    pattern: "{controller=Cliente}/{action=Index}/{id?}");

app.Run();
