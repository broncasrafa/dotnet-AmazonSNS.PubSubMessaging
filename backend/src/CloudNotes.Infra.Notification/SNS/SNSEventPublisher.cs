using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CloudNotes.Domain.Interfaces;
using CloudNotes.Domain.Models;
using CloudNotes.Domain.Settings;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.S3.Model;
using CloudNotes.Domain.Extensions;


namespace CloudNotes.Infra.Notification.SNS;

internal class SNSEventPublisher : IEventPublisher
{
    private readonly ILogger _logger;
    private readonly SNSSettings _settings;
    private readonly IAmazonSimpleNotificationService _snsClient;

    public SNSEventPublisher(IOptions<SNSSettings> snsSettings, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<SNSEventPublisher>();
        _settings = snsSettings.Value;
        _snsClient = new AmazonSimpleNotificationServiceClient(
            awsAccessKeyId: _settings.AWSAccessKey,
            awsSecretAccessKey: _settings.AWSSecretKey,
            region: RegionEndpoint.GetBySystemName(_settings.AWSRegion));
    }

    public async Task PublishEventAsync(Event eventData)
    {
        try
        {
            PublishRequest publishRequest = new();
            publishRequest.TopicArn = _settings.TopicArn;
            publishRequest.Message = eventData.SerializeObject();

            PublishResponse publishResponse = await _snsClient.PublishAsync(publishRequest);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Couldn't publish an event to SNS");
            throw;
        }
    }
}
