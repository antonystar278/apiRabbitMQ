namespace CreateOperationWorker.Options
{
    public class RabbitMqConfiguration
    {
        public string Hostname { get; set; }
        public string InProcesOperationQueue { get; set; }
        public string ComplitedOperationQueue { get; set; }
    }
}
