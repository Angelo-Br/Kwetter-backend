﻿using Microsoft.Extensions.DependencyInjection;
using RabbitMQLibrary;
using System;

namespace RabbitMQLibrary
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMessageProducing(this IServiceCollection services, string queueName)
        {
            // Add to every service in the microservices the queuename which is unique to each service
            var queueNameService = new QueueName(queueName);
            services.AddSingleton(queueNameService);
            // Create a new rabbitmq connection and add it to the service collection that way every service has the connection
            var connection = new RabbitMqConnection();
            services.AddSingleton(connection);
            // Give the IMessagePublisher its MessageProducer that way it can start producing messages

            services.AddScoped<IMessageProducer, MessageProducer>();
        }

        public static void AddMessageConsuming(this IServiceCollection services)
        {
            // Create a new rabbitmq connection and add it to the service collection that way every service has the connection
            var connection = new RabbitMqConnection();
            services.AddSingleton(connection);
            // Give the IMessagePublisher its MessageProducer that way it can start producing messages
            services.AddScoped<IMessageConsumer, MessageConsumer>();
        }
    }
}
