using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CloudNotes.Domain.Models;
using CloudNotes.Domain.Settings;
using CloudNotes.Domain.Extensions;
using CloudNotes.Domain.Interfaces.Events;
using Amazon;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.S3.Model;


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
            SNSMessage message = new SNSMessage(eventData.SerializeObject());
            message.EmailContent = $"{eventData.EventType.ToString()} - Note {eventData.NoteId}";

            SNSMessageAttributeCollection messageAttributes = new SNSMessageAttributeCollection();
            messageAttributes.Add("Publisher", "CloudNotes Web");

            PublishRequest publishRequest = new();
            publishRequest.TopicArn = _settings.TopicArn;
            publishRequest.Message = message.ToString();
            publishRequest.MessageStructure = "json";
            publishRequest.Subject = eventData.GetSubject();
            publishRequest.MessageAttributes = messageAttributes;
            

            _logger.LogInformation("Publish an event to SNS");

            PublishResponse publishResponse = await _snsClient.PublishAsync(publishRequest);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Couldn't publish an event to SNS");
            throw;
        }
    }
}
