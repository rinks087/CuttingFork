namespace CSFTWebAPI.Services
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class MiiBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public MiiBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        // Resolve services needed for background processing
                        var dbContext = scope.ServiceProvider.GetRequiredService<YourDbContext>();
                        var soapClient = scope.ServiceProvider.GetRequiredService<MiiSoapClient>();

                        // Fetch pending items from the database
                        var pendingItems = dbContext.QueueItems.Where(q => q.Status == "Pending").ToList();

                        foreach (var item in pendingItems)
                        {
                            try
                            {
                                // Call the SOAP service
                                var response = await soapClient.CallSoapServiceAsync(item.Payload);

                                // Update item status to Processed
                                item.Status = "Processed";
                                item.Response = response;
                            }
                            catch (Exception ex)
                            {
                                // Log and mark as failed
                                item.Status = "Failed";
                                item.ErrorMessage = ex.Message;
                            }

                            // Save changes to the database
                            dbContext.Update(item);
                        }

                        await dbContext.SaveChangesAsync(stoppingToken);
                    }

                    // Wait for 5 seconds before the next execution
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
                catch (Exception ex)
                {
                    // Log errors (use a logging framework like Serilog here)
                    Console.WriteLine($"Error in background service: {ex.Message}");
                }
            }
        }
    }

}
