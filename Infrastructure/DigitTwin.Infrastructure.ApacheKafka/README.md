# DigitTwin.Infrastructure.ApacheKafka - ���������� ��� ������ � Apache Kafka � .NET

���������� ������������� ������� ������ ������ � Apache Kafka �����:
- ��������� ��������� ����������/�����������
- DI-����������
- ��������� ����� appsettings.json
- �������������� ���������� ���������

## ���������

1. �������� ����� � ������:
```bash
dotnet add package KafkaLib
```

2. �������� � appsettings.json:
```json
{
  "Kafka": {
    "Producers": {
      "OrderProducer": {
        "BootstrapServers": "localhost:9092",
        "Topic": "orders",
        "Properties": {
          "message.timeout.ms": "5000"
        }
      },
      "PaymentProducer": {
        "BootstrapServers": "kafka-payments:9093",
        "Topic": "payments"
      }
    },
    "Consumers": {
      "OrderConsumer": {
        "BootstrapServers": "localhost:9092",
        "Topic": "orders",
        "GroupId": "order-processor"
      },
      "NotificationConsumer": {
        "BootstrapServers": "localhost:9092",
        "Topic": "notifications",
        "GroupId": "notification-service"
      }
    }
  }
}
```

3. ��������������� ������� � DI:
```csharp
builder.Services.AddKafka(builder.Configuration);
```

## ������������� ���������
```csharp
public class OrderService
{
    private readonly IKafkaProducer<string, OrderEvent> _producer;

    public OrderService(IKafkaProducerFactory factory)
    {
        _producer = factory.GetProducer<string, OrderEvent>("OrderProducer");
    }

    public async Task CreateOrderAsync(Order order)
    {
        var orderEvent = new OrderEvent(order);
        await _producer.ProduceAsync(order.Id, orderEvent);
    }
}
```

## ������������� ����������
```csharp
public class OrderProcessor : BackgroundService
{
    private readonly IKafkaConsumer<string, OrderEvent> _consumer;

    public OrderProcessor(IKafkaConsumerFactory factory)
    {
        _consumer = factory.CreateConsumer<string, OrderEvent>("OrderConsumer");
    }

    protected override async Task ExecuteAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                var result = _consumer.Consume(token);
                ProcessOrder(result.Message.Value);
                _consumer.Commit(result);
            }
            catch (Exception ex)
            {
                // ��������� ������
            }
        }
    }
}
```