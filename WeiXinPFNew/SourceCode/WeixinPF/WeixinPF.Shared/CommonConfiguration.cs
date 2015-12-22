﻿using NServiceBus;

public static class CommonConfiguration
{
    public static void ApplyCommonConfiguration(this BusConfiguration configuration)
    {
        configuration.UseTransport<MsmqTransport>();
        configuration.UsePersistence<InMemoryPersistence>();
        configuration.Conventions()
            .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.StartsWith("Store") && t.Namespace.EndsWith("Commands"))
            .DefiningEventsAs(t => t.Namespace != null && t.Namespace.StartsWith("Store") && t.Namespace.EndsWith("Events"))
            .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.StartsWith("Store") && t.Namespace.EndsWith("RequestResponse"))
            .DefiningEncryptedPropertiesAs(p => p.Name.StartsWith("Encrypted"));
        configuration.RijndaelEncryptionService();
        configuration.EnableInstallers();
    }
}