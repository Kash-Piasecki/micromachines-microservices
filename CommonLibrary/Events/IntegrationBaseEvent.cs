using System;

namespace CommonLibrary.Events
{
    public class IntegrationBaseEvent
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }

        // public IntegrationBaseEvent(Guid id, DateTime creationDate)
        // {
        //     Id = id;
        //     CreationDate = creationDate;
        // }

        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}